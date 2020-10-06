using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiWykazuPodatnikowVatData
{
    public class ApiWykazuPodatnikowVatData
    {
        # region private static readonly log4net.ILog _log4net
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private static readonly log4net.ILog _log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region private static readonly string _restClientUrl
        /// <summary>
        /// Paramentr url https://wl-test.mf.gov.pl lub https://wl-api.mf.gov.pl
        /// Wartość parametru RestClientUrl z pliku api.wykazu.podatnikow.vat.data.appsettings.debug.json
        /// Paramentr url https://wl-test.mf.gov.pl or https://wl-api.mf.gov.pl
        /// The value of the RestClientUrl parameter from the api.wykazu.podatnikow.vat.data.appsettings.debug.json file
        /// </summary>
        private static readonly string _restClientUrl = NetAppCommon.Configuration.GetValue<string>("api.wykazu.podatnikow.vat.data.appsettings.debug.json", "RestClientUrl");
        #endregion

        #region private static readonly string _connectionStrings
        /// <summary>
        /// Połączenie do bazy danych pobrane z pliku konfigracyjnego aplikacji api.wykazu.podatnikow.vat.data.appsettings.debug.json.
        /// Database connection taken from the api.wykazu.podatnikow.vat.data.appsettings.debug.json application configuration file.
        /// </summary>
        private static readonly string _connectionStrings = NetAppCommon.DatabaseMssql.GetConnectionString("ApiWykazuPodatnikowVatDataDbContext", "api.wykazu.podatnikow.vat.data.appsettings.debug.json");
        #endregion

        #region private static readonly int _cacheLifeTime
        /// <summary>
        /// Czas życia pamięci podręcznej dla zapytań do serwisu
        /// Cache lifetime for site queries
        /// </summary>
        private static readonly int _cacheLifetimeForSiteQueries = NetAppCommon.Configuration.GetValue<int>("api.wykazu.podatnikow.vat.data.appsettings.debug.json", "CacheLifeTimeForApiServiceQueries");
        #endregion

        #region private static async Task<Entity> FindByNipAndModificationDateAsync(string nip)
        /// <summary>
        /// Znajdź podmiot według numeru NIP i ostatniej daty modyfikacji,
        /// Find entity by tax identification NIP number and last modified date
        /// </summary>
        /// <param name="nip">
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$],
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <returns>
        /// Podmiot jako obiekt Entity lub null,
        /// Entity as an Entity or null object
        /// </returns>
        private static async Task<Entity> FindByNipAndModificationDateAsync(string nip)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            return context.Entity.Where(w => !string.IsNullOrWhiteSpace(nip) && !string.IsNullOrWhiteSpace(w.Nip) && w.Nip == nip && w.DateOfModification >= DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).Include(W => W.EntityAccountNumber).FirstOrDefault();
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<Entity> FindByNipAsync(string nip)
        /// <summary>
        /// Znajdź podmiot według numeru NIP
        /// Find entity by tax identification NIP number
        /// </summary>
        /// <param name="nip">
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$],
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <returns>
        /// Podmiot jako obiekt Entity lub null,
        /// Entity as an Entity or null object
        /// </returns>
        private static async Task<Entity> FindByNipAsync(string nip)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            return context.Entity.Where(w => !string.IsNullOrWhiteSpace(nip) && !string.IsNullOrWhiteSpace(w.Nip) && w.Nip == nip).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).FirstOrDefault();
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByNipsAsync(string nips)
        /// <summary>
        /// Znajdź podmioty wedłóg listy numerów NIP
        /// Find entities according to the list of NIP numbers
        /// </summary>
        /// <param name="nips">
        /// Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        /// A list of up to 30 NIP, separated by commas
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByNipsAsync(string nips)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> nipList = new List<string>(nips.Split(',')).ToList();
                            if (null != nipList && nipList.Count > 0)
                            {
                                return context.Entity.Where(w => nipList.Contains(w.Nip)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return entityList;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByNipsAndModificationDateAsync(string nips)
        /// <summary>
        /// Znajdź podmioty według listy numerów NIP jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla parametru CacheLifeTimeForApiServiceQueries
        /// Find entities by NIP number list if the modification date is greater than or equal to the date calculated for the CacheLifeTimeForApiServiceQueries parameter
        /// </summary>
        /// <param name="nips">
        /// Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        /// A list of up to 30 NIP, separated by commas
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByNipsAndModificationDateAsync(string nips)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> nipList = new List<string>(nips.Split(',')).ToList();
                            if (null != nipList && nipList.Count > 0)
                            {
                                if (context.Entity.Where(w => (from f in context.Entity where !string.IsNullOrWhiteSpace(w.Nip) && nipList.Contains(w.Nip) select f.Id).Contains(w.Id) && w.DateOfModification < DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).Any())
                                {
                                    return entityList;
                                }
                                return context.Entity.Where(w => nipList.Contains(w.Nip)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return entityList;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<Entity> FindByRegonAndModificationDateAsync(string regon)
        /// <summary>
        /// Znajdź podmiot według numeru REGON jeśli data modyfikacji jest większa lub równa od daty obliczonej dla parametru CacheLifeTimeForApiServiceQueries
        /// Find the entity by REGON number if the modification date is greater than or equal to the date calculated for the CacheLifeTimeForApiServiceQueries parameter
        /// </summary>
        /// <param name="regon">
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </param>
        /// <returns>
        /// Podmiot jako obiekt Entity lub null,
        /// Entity as an Entity or null object
        /// </returns>
        private static async Task<Entity> FindByRegonAndModificationDateAsync(string regon)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            return context.Entity.Where(w => !string.IsNullOrWhiteSpace(regon) && !string.IsNullOrWhiteSpace(w.Regon) && w.Regon == regon && w.DateOfModification >= DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).FirstOrDefault();

                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByRegonsAsync(string regons)
        /// <summary>
        /// Znajdź podmioty wedłóg listy numerów REGON
        /// Find entities according to the list of REGON numbers
        /// </summary>
        /// <param name="regons">
        /// Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        /// A list of up to 30 REGON, separated by commas
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByRegonsAsync(string regons)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> regonList = new List<string>(regons.Split(',')).ToList();
                            if (null != regonList && regonList.Count > 0)
                            {
                                return context.Entity.Where(w => regonList.Contains(w.Regon)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return entityList;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByRegonsAndModificationDateAsync(string regons)
        /// <summary>
        /// Znajdź podmioty według listy numerów REGON jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla parametru CacheLifeTimeForApiServiceQueries
        /// Find entities by REGON number list if the modification date is greater than or equal to the date calculated for the CacheLifeTimeForApiServiceQueries parameter
        /// </summary>
        /// <param name="regons">
        /// Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        /// A list of up to 30 REGON, separated by commas
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByRegonsAndModificationDateAsync(string regons)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> regonList = new List<string>(regons.Split(',')).ToList();
                            if (null != regonList && regonList.Count > 0)
                            {
                                if (context.Entity.Where(w => (from f in context.Entity where !string.IsNullOrWhiteSpace(w.Regon) && regonList.Contains(w.Regon) select f.Id).Contains(w.Id) && w.DateOfModification < DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).Any())
                                {
                                    return entityList;
                                }
                                return context.Entity.Where(w => regonList.Contains(w.Regon)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return entityList;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<Entity> FindByBankAccountAndModificationDateAsync(string bankAccount)
        /// <summary>
        /// Znajdź podmioty według numeru rachunku bankowego NRB jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla parametru CacheLifeTimeForApiServiceQueries
        /// Find entities by NRB bank account number if the modification date is greater than or equal to the date calculated for the CacheLifeTimeForApiServiceQueries parameter
        /// </summary>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <returns>
        /// Lista Podmiotów jako lista obiektów Entity lub null
        /// Entity List as a list of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByBankAccountAndModificationDateAsync(string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            if (context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(bankAccount) && !string.IsNullOrWhiteSpace(f.AccountNumber) && f.AccountNumber.Contains(bankAccount) select f.EntityId).Contains(w.Id) && w.DateOfModification < DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).Any())
                            {
                                return entityList;
                            }
                            else
                            {
                                return context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(bankAccount) && !string.IsNullOrWhiteSpace(f.AccountNumber) && f.AccountNumber.Contains(bankAccount) select f.EntityId).Contains(w.Id)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return entityList;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByBankAccountAsync(string bankAccount)
        /// <summary>
        /// Znajdź podmioty według numeru rachunku bankowego NRB
        /// Find entities by NRB bank account number
        /// </summary>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <returns>
        /// Lista Podmiotów jako lista obiektów Entity lub null
        /// Entity List as a list of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByBankAccountAsync(string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            return context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(bankAccount) && !string.IsNullOrWhiteSpace(f.AccountNumber) && f.AccountNumber.Contains(bankAccount) select f.EntityId).Contains(w.Id)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByBankAccountsAsync(string bankAccounts)
        /// <summary>
        /// Znajdź podmioty wedłóg listy numerów rachunków NRB
        /// Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        /// Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByBankAccountsAsync(string bankAccounts)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> bankAccountsList = new List<string>(bankAccounts.Split(',')).ToList();
                            if (null != bankAccountsList && bankAccountsList.Count > 0)
                            {
                                return context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(f.AccountNumber) && bankAccountsList.Contains(f.AccountNumber) select f.EntityId).Contains(w.Id)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<Entity>> FindByBankAccountsAndModificationDateAsync(string bankAccounts)
        /// <summary>
        /// Znajdź podmioty wedłóg listy numerów rachunków NRB
        /// Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        /// Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        private static async Task<List<Entity>> FindByBankAccountsAndModificationDateAsync(string bankAccounts)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> bankAccountsList = new List<string>(bankAccounts.Split(',')).ToList();
                            if (null != bankAccountsList && bankAccountsList.Count > 0)
                            {
                                if (context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(f.AccountNumber) && bankAccountsList.Contains(f.AccountNumber) select f.EntityId).Contains(w.Id) && w.DateOfModification < DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).Any())
                                {
                                    return null;
                                }
                                return context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(f.AccountNumber) && bankAccountsList.Contains(f.AccountNumber) select f.EntityId).Contains(w.Id)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).ToList();
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<EntityAccountNumber>> AddOrModifyEntityAccountNumberAsync(List<string> accountNumbersList, Entity entity)
        /// <summary>
        /// Dodaj lub zmodyfikuj numer konta bankowego dla podmiotu
        /// Add or modify the bank account number for the entity
        /// </summary>
        /// <param name="accountNumbersList">
        /// Parametr Lista rachunków bankowych jako List <string>
        /// Parameter List of bank accounts as List <string>
        /// </param>
        /// <param name="entity">
        /// Obiekt podmiotu jako Entity
        /// The subject object as Entity
        /// </param>
        /// <returns>
        /// Lista numerów kont bankowwych dla podmiotu jako List <EntityAccountNumber>
        /// List of bank account numbers for the entity as List <EntityAccountNumber>
        /// </returns>
        private static async Task<List<EntityAccountNumber>> AddOrModifyEntityAccountNumberAsync(List<string> accountNumbersList, Entity entity)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != accountNumbersList && accountNumbersList.Count > 0)
                    {
                        using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                        {
                            if (await context.Database.CanConnectAsync())
                            {
                                context.EntityAccountNumber.RemoveRange(
                                context.EntityAccountNumber.Where(
                                    w => w.EntityId == entity.Id
                                    && !accountNumbersList.Contains(w.AccountNumber)
                                    )
                                );
                                int isEntityAccountNumberRemoveRange = await context.SaveChangesAsync();
                                _log4net.Debug($"Remove EntityAccountNumber if is not found in list and Save Changes Async to database: { isEntityAccountNumberRemoveRange }");
                                foreach (string accountNumber in accountNumbersList)
                                {
                                    try
                                    {
                                        await Task.Run(async () =>
                                        {
                                            EntityAccountNumber entityAccountNumber = null;
                                            if (null != accountNumber && !string.IsNullOrWhiteSpace(accountNumber))
                                            {
                                                entityAccountNumber = await context.EntityAccountNumber.Where(w => w.EntityId == entity.Id && w.AccountNumber.Contains(accountNumber)).FirstOrDefaultAsync();
                                                if (null != entityAccountNumber)
                                                {
                                                    entityAccountNumber.DateOfModification = DateTime.Now;
                                                    context.Entry(entityAccountNumber).State = EntityState.Modified;
                                                    int isEntityAccountNumberModified = await context.SaveChangesAsync();
                                                    _log4net.Debug($"Modify EntityAccountNumber if is found in list and Save Changes Async to database: { isEntityAccountNumberModified }");
                                                }
                                                else
                                                {
                                                    entityAccountNumber = new EntityAccountNumber()
                                                    {
                                                        UniqueIdentifierOfTheLoggedInUser = "test",
                                                        Entity = entity,
                                                        AccountNumber = accountNumber,
                                                    };
                                                    context.Entry(entityAccountNumber).State = EntityState.Added;
                                                    int isEntityAccountNumberAdded = await context.SaveChangesAsync();
                                                    _log4net.Debug($"Add EntityAccountNumber and Save Changes Async to database: { isEntityAccountNumberAdded } id { entityAccountNumber.Id }");
                                                }
                                            }
                                        });
                                    }
                                    catch (Exception e)
                                    {
                                        _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                    }
                                }
                                return context.EntityAccountNumber.Where(w => w.EntityId == entity.Id && accountNumbersList.Contains(w.AccountNumber)).ToList();
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<List<EntityPerson>> AddOrModifyEntityPersonAsync(List<EntityPerson> entityPerson, Entity entity, string property)
        /// <summary>
        /// Dodaj lub zmodyfikuj numer konta bankowego dla podmiotu
        /// Add or modify the bank account number for the entity
        /// </summary>
        /// <param name="accountNumbersList">
        /// Lista maksymalnie 30 numerow rachunkow bankowych rozdzielonych przecinkami jako string
        /// A list of up to 30 bank account numbers separated by commas as a string
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="entity">
        /// Obiekt podmiotu jako Entity
        /// The subject object as Entity
        /// </param>
        /// <returns>
        /// Lista numerów kont bankowych dla podmiotu jako Lista obiektów encji EntityAccountNumber
        /// List of bank account numbers for the entity as EntityAccountNumber Entity Object List
        /// </returns>
        private static async Task<List<EntityPerson>> AddOrModifyEntityPersonAsync(List<EntityPerson> entityPerson, Entity entity, string property)
        {
            try
            {
                return await Task.Run<List<EntityPerson>>(async () =>
                {
                    if (null != entityPerson && entityPerson.Count > 0 && new List<string>() { "EntityRepresentativeId", "EntityAuthorizedClerkId", "EntityPartnerId" }.Contains(property))
                    {
                        PropertyInfo propertyInfo = entityPerson.FirstOrDefault().GetType().GetProperty(property);
                        using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                        {
                            if (await context.Database.CanConnectAsync())
                            {
                                try
                                {
                                    context.EntityPerson.RemoveRange(
                                        context.EntityPerson.Where(
                                            w => EF.Property<Guid>(w, property) == entity.Id
                                            //&& (
                                            //    !(from f in entityPerson select f.Nip).ToList().Contains(w.Nip) ||
                                            //    !(from f in entityPerson select f.CompanyName).ToList().Contains(w.CompanyName) ||
                                            //    !(
                                            //        (from f in entityPerson select f.FirstName).ToList().Contains(w.FirstName) &&
                                            //        (from f in entityPerson select f.LastName).ToList().Contains(w.LastName)
                                            //    )
                                            //)
                                        )
                                    );
                                    int isEntityPersonRemoveRange = await context.SaveChangesAsync();
                                    _log4net.Debug($"Remove EntityPerson if is not found in list and Save Changes Async to database: { isEntityPersonRemoveRange }");
                                }
                                catch (Exception e)
                                {
                                    _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                }
                                try
                                {
                                    entityPerson.ForEach(x =>
                                    {
                                        switch (property)
                                        {
                                            case "EntityRepresentativeId":
                                                x.EntityRepresentativeId = entity.Id;
                                                break;
                                            case "EntityAuthorizedClerkId":
                                                x.EntityAuthorizedClerkId = entity.Id;
                                                break;
                                            case "EntityPartnerId":
                                                x.EntityPartnerId = entity.Id;
                                                break;
                                        }
                                        x.UniqueIdentifierOfTheLoggedInUser = "test";
                                        x.DateOfModification = DateTime.Now;
                                    });
                                    context.EntityPerson.AddRange(entityPerson.Where(w => null == w.Id || "00000000-0000-0000-0000-000000000000" == w.Id.ToString()));
                                    int isEntityPersonAddRange = await context.SaveChangesAsync();
                                    _log4net.Debug($"Add EntityPerson if is not found in list and Save Changes Async to database: { isEntityPersonAddRange }");
                                }
                                catch (Exception e)
                                {
                                    _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                }
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region private static async Task<Entity> AddOrModifyEntity(Entity entity)
        /// <summary>
        /// Dodaj lub zaktualizuj rekord w bazie danych
        /// Add or update a record in the database
        /// </summary>
        /// <param name="entity">
        /// Obiekt podmiotu jako Entity
        /// The subject object as Entity
        /// </param>
        /// <returns>
        /// Zaktualizowany lub wstawiony obiekt Entity lub przekazany obiekt Entity lub null
        /// An updated or inserted Entity or a passed Entity, or null
        /// </returns>
        private static async Task<Entity> AddOrModifyEntity(Entity entity)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != entity && null != entity.Nip && (!string.IsNullOrWhiteSpace(entity.Nip) || !string.IsNullOrWhiteSpace(entity.Pesel) || !string.IsNullOrWhiteSpace(entity.Regon)))
                    {
                        using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                        {
                            if (await context.Database.CanConnectAsync())
                            {
                                int isEntitySaveChangesAsync = 0;
                                try
                                {
                                    Entity entityWhere = context.Entity.Where(w => (!string.IsNullOrWhiteSpace(w.Nip) && !string.IsNullOrWhiteSpace(entity.Nip) && w.Nip == entity.Nip)).FirstOrDefault();
                                    if (null != entityWhere)
                                    {
                                        entity.Id = entityWhere.Id;
                                        context.Entry(entityWhere).State = EntityState.Detached;
                                    }
                                    entity.DateOfModification = DateTime.Now;
                                    entity.UniqueIdentifierOfTheLoggedInUser = "test";
                                    context.Entry(entity).State = null != entity.Id && "00000000-0000-0000-0000-000000000000" != entity.Id.ToString() ? EntityState.Modified : EntityState.Added;
                                    isEntitySaveChangesAsync = await context.SaveChangesAsync();
                                    _log4net.Debug($"Save Entity Changes Async to database: { isEntitySaveChangesAsync } id: { entity.Id }");
                                }
                                catch (Exception e)
                                {
                                    _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                }
                                if (isEntitySaveChangesAsync == 1)
                                {
                                    try
                                    {
                                        List<string> accountNumbersList = (List<string>)entity.AccountNumbers;
                                        List<EntityAccountNumber> entityAccountNumbersList = await AddOrModifyEntityAccountNumberAsync(accountNumbersList, entity);
                                    }
                                    catch (Exception e)
                                    {
                                        _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                    }
                                    try
                                    {
                                        List<EntityPerson> entityPersonListRepresentatives = (List<EntityPerson>)entity.Representatives;
                                        List<EntityPerson> entityPersonListRepresentative = await AddOrModifyEntityPersonAsync(entityPersonListRepresentatives, entity, "EntityRepresentativeId");
                                    }
                                    catch (Exception e)
                                    {
                                        _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                    }
                                    try
                                    {
                                        List<EntityPerson> entityPersonListAuthorizedClerks = (List<EntityPerson>)entity.AuthorizedClerks;
                                        List<EntityPerson> entityPersonListAuthorizedClerk = await AddOrModifyEntityPersonAsync(entityPersonListAuthorizedClerks, entity, "EntityAuthorizedClerkId");
                                    }
                                    catch (Exception e)
                                    {
                                        _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                    }
                                    try
                                    {
                                        List<EntityPerson> entityPersonListPartners = (List<EntityPerson>)entity.Partners;
                                        List<EntityPerson> entityPersonListPartner = await AddOrModifyEntityPersonAsync(entityPersonListPartners, entity, "EntityPartnerId");
                                    }
                                    catch (Exception e)
                                    {
                                        _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                                    }
                                    return context.Entity.Where(w => (!string.IsNullOrWhiteSpace(w.Nip) && !string.IsNullOrWhiteSpace(entity.Nip) && w.Nip == entity.Nip) || (!string.IsNullOrWhiteSpace(w.Regon) && !string.IsNullOrWhiteSpace(entity.Regon) && w.Regon == entity.Regon) || (!string.IsNullOrWhiteSpace(w.Pesel) && !string.IsNullOrWhiteSpace(entity.Pesel) && w.Pesel == entity.Pesel)).Include(w => w.EntityAccountNumber).Include(w => w.AuthorizedClerk).Include(w => w.Partner).Include(w => w.Representative).FirstOrDefault() ?? entity;
                                }
                            }
                        }
                    }
                    return entity;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return entity;
        }
        #endregion

        #region public static async Task<Entity> ApiFindByNipAsync(string nip)
        /// <summary>
        /// Wyszukaj i pobierz podmiot w serwisie mf.gov.pl według numeru NIP
        /// Search and get an entity on the mf.gov.pl website by tax identification number NIP
        /// GET https://wl-test.mf.gov.pl/api/search/nip/{nip}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/nip/{nip}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="nip">
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <returns>
        /// Podmiot jako obiekt Entity lub null
        /// Entity as an Entity or null object
        /// </returns>
        public static async Task<Entity> ApiFindByNipAsync(string nip)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != nip && !string.IsNullOrWhiteSpace(nip))
                    {
                        Entity entityFindByNipAndModificationDateAsync = await FindByNipAndModificationDateAsync(nip);
                        if (null != entityFindByNipAndModificationDateAsync)
                        {
                            return entityFindByNipAndModificationDateAsync;
                        }
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/search/nip/{nip}").AddUrlSegment("nip", nip).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        //RestRequest request = new RestRequest(@"https://localhost:5001/api/APIRejestWLExampleDataEntityResponse");
                        IRestResponse<EntityResponse> response = await client.ExecuteAsync<EntityResponse>(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result.Subject)
                        {
                            Entity entity = response.Data.Result.Subject;
                            if (null != entity)
                            {
                                return await AddOrModifyEntity(entity);
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<Entity> ApiFindByRegonAsync(string regon)
        /// <summary>
        /// Wyszukaj i pobierz podmiot w serwisie mf.gov.pl według numeru REGON
        /// Search and get an entity on the mf.gov.pl website by REGON number
        /// GET https://wl-test.mf.gov.pl/api/search/regon/{regon}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/regon/{regon}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="regon">
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </param>
        /// <returns>
        /// Podmiot jako obiekt Entity lub null
        /// Entity as an Entity or null object
        /// </returns>
        public static async Task<Entity> ApiFindByRegonAsync(string regon)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != regon && !string.IsNullOrWhiteSpace(regon))
                    {
                        Entity entityFindByRegonAndModificationDateAsync = await FindByRegonAndModificationDateAsync(regon);
                        if (null != entityFindByRegonAndModificationDateAsync)
                        {
                            return entityFindByRegonAndModificationDateAsync;
                        }
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/search/regon/{regon}").AddUrlSegment("regon", regon).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        //RestRequest request = new RestRequest(@"https://localhost:5001/api/APIRejestWLExampleDataEntityResponse");
                        IRestResponse<EntityResponse> response = await client.ExecuteAsync<EntityResponse>(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result.Subject)
                        {
                            Entity entity = response.Data.Result.Subject;
                            if (null != entity)
                            {
                                return await AddOrModifyEntity(entity);
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<Entity> ApiFindByBankAccountAsync(string bankAccount)
        /// <summary>
        /// Wyszukaj i pobierz podmioty w serwisie mf.gov.pl według numeru rachunku bankowego NRB
        /// Search and get entities on the mf.gov.pl website according to the NRB bank account number
        /// GET https://wl-test.mf.gov.pl/api/search/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <returns>
        /// Lista Podmiotów jako lista obiektów Entity lub null
        /// Entity List as a list of Entity objects or null
        /// </returns>
        public static async Task<List<Entity>> ApiFindByBankAccountAsync(string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != bankAccount && !string.IsNullOrWhiteSpace(bankAccount))
                    {
                        List<Entity> findByBankAccountAndModificationDateList = await FindByBankAccountAndModificationDateAsync(bankAccount);
                        if (null != findByBankAccountAndModificationDateList)
                        {
                            return findByBankAccountAndModificationDateList;
                        }
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/search/bank-account/{bankAccount}").AddUrlSegment("bankAccount", bankAccount).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        //RestRequest request = new RestRequest(@"https://localhost:5001/api/APIRejestWLExampleDataEntityResponse");
                        IRestResponse<EntityListResponse> response = await client.ExecuteAsync<EntityListResponse>(request);
                        //_log4net.Debug($"{ response.StatusCode } { response.Content }");
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result.Subjects)
                        {
                            List<Entity> entityList = (List<Entity>)response.Data.Result.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Nip && !string.IsNullOrWhiteSpace(entity.Nip))
                                    {
                                        Entity entityFindByNipAndModificationDateAsync = await FindByNipAndModificationDateAsync(entity.Nip);
                                        if (null == entityFindByNipAndModificationDateAsync)
                                        {
                                            await AddOrModifyEntity(entity);
                                        }
                                    }
                                    else if (null != entity && null != entity.Regon && !string.IsNullOrWhiteSpace(entity.Regon))
                                    {
                                        Entity entityFindByRegonAndModificationDateAsync = await FindByRegonAndModificationDateAsync(entity.Regon);
                                        if (null == entityFindByRegonAndModificationDateAsync)
                                        {
                                            await AddOrModifyEntity(entity);
                                        }
                                    }
                                }
                                return await FindByBankAccountAsync(bankAccount) ?? entityList;
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<Entity> ApiFindByNipsAsync(string nips)
        /// <summary>
        /// Znajdź podmioty wedłóg listy numerów NIP
        /// Find entities according to the list of NIP numbers
        /// </summary>
        /// <param name="nips">
        /// Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        /// A list of up to 30 NIP, separated by commas
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        public static async Task<List<Entity>> ApiFindByNipsAsync(string nips)
        {
            try
            {
                return await Task.Run<List<Entity>>(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != nips && !string.IsNullOrWhiteSpace(nips))
                    {
                        List<Entity> findByNipsAndModificationDateList = await FindByNipsAndModificationDateAsync(nips);
                        if (null != findByNipsAndModificationDateList)
                        {
                            return findByNipsAndModificationDateList;
                        }
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/search/nips/{nips}").AddUrlSegment("nips", nips).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        //RestRequest request = new RestRequest(@"https://localhost:5001/api/APIRejestWLExampleDataEntityResponse");
                        IRestResponse<EntityListResponse> response = await client.ExecuteAsync<EntityListResponse>(request);
                        //_log4net.Debug($"{ response.StatusCode } { response.Content }");
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result.Subjects)
                        {
                            List<Entity> entityList = (List<Entity>)response.Data.Result.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Nip && !string.IsNullOrWhiteSpace(entity.Nip))
                                    {
                                        Entity entityFindByNipAndModificationDateAsync = await FindByNipAndModificationDateAsync(entity.Nip);
                                        if (null == entityFindByNipAndModificationDateAsync)
                                        {
                                            await AddOrModifyEntity(entity);
                                        }
                                    }
                                }
                                return await FindByNipsAsync(nips) ?? entityList;
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<Entity> ApiFindByRegonsAsync(string regons)
        /// <summary>
        /// https://wl-test.mf.gov.pl/api/search/regons/{regons}?date={date format yyyy-MM-dd}
        /// https://wl-api.mf.gov.pl/api/search/regons/{regons}?date={date format yyyy-MM-dd}
        /// Znajdź podmioty wedłóg listy numerów REGON
        /// Find entities according to the list of REGON numbers
        /// </summary>
        /// <param name="regons">
        /// Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        /// A list of up to 30 REGON, separated by commas
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        public static async Task<List<Entity>> ApiFindByRegonsAsync(string regons)
        {
            try
            {
                return await Task.Run<List<Entity>>(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != regons && !string.IsNullOrWhiteSpace(regons))
                    {
                        List<Entity> findByRegonsAndModificationDateList = await FindByRegonsAndModificationDateAsync(regons);
                        if (null != findByRegonsAndModificationDateList)
                        {
                            return findByRegonsAndModificationDateList;
                        }
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/search/regons/{regons}").AddUrlSegment("regons", regons).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        //RestRequest request = new RestRequest(@"https://localhost:5001/api/APIRejestWLExampleDataEntityResponse");
                        IRestResponse<EntityListResponse> response = await client.ExecuteAsync<EntityListResponse>(request);
                        //_log4net.Debug($"{ response.StatusCode } { response.Content }");
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result.Subjects)
                        {
                            List<Entity> entityList = (List<Entity>)response.Data.Result.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Regon && !string.IsNullOrWhiteSpace(entity.Regon))
                                    {
                                        Entity entityFindByRegonAndModificationDateAsync = await FindByRegonAndModificationDateAsync(entity.Regon);
                                        if (null == entityFindByRegonAndModificationDateAsync)
                                        {
                                            await AddOrModifyEntity(entity);
                                        }
                                    }
                                }
                                return await FindByRegonsAsync(regons) ?? entityList;
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<Entity> ApiFindByBankAccountsAsync(string bankAccounts)
        /// <summary>
        /// GET https://wl-test.mf.gov.pl/api/search/bank-accounts/{bankAccounts}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/bank-accounts/{bankAccounts}?date={date format yyyy-MM-dd}
        /// Znajdź podmioty wedłóg listy numerów rachunków NRB
        /// Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        /// Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List obiektów Entity lub null
        /// List of entities as List of Entity objects or null
        /// </returns>
        public static async Task<List<Entity>> ApiFindByBankAccountsAsync(string bankAccounts)
        {
            try
            {
                return await Task.Run<List<Entity>>(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != bankAccounts && !string.IsNullOrWhiteSpace(bankAccounts))
                    {
                        List<Entity> findByBankAccountsAndModificationDateList = await FindByBankAccountsAndModificationDateAsync(bankAccounts);
                        if (null != findByBankAccountsAndModificationDateList)
                        {
                            return findByBankAccountsAndModificationDateList;
                        }
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/search/bank-accounts/{bankAccounts}").AddUrlSegment("bankAccounts", bankAccounts).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        //RestRequest request = new RestRequest(@"https://localhost:5001/api/APIRejestWLExampleDataEntityResponse");
                        IRestResponse<EntityListResponse> response = await client.ExecuteAsync<EntityListResponse>(request);
                        //_log4net.Debug($"{ response.StatusCode } { response.Content }");
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result.Subjects)
                        {
                            List<Entity> entityList = (List<Entity>)response.Data.Result.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Nip && !string.IsNullOrWhiteSpace(entity.Nip))
                                    {
                                        Entity entityFindByNipAndModificationDateAsync = await FindByNipAndModificationDateAsync(entity.Nip);
                                        if (null == entityFindByNipAndModificationDateAsync)
                                        {
                                            await AddOrModifyEntity(entity);
                                        }
                                    }
                                    else if (null != entity && null != entity.Regon && !string.IsNullOrWhiteSpace(entity.Regon))
                                    {
                                        Entity entityFindByRegonAndModificationDateAsync = await FindByRegonAndModificationDateAsync(entity.Regon);
                                        if (null == entityFindByRegonAndModificationDateAsync)
                                        {
                                            await AddOrModifyEntity(entity);
                                        }
                                    }
                                }
                                return await FindByBankAccountsAsync(bankAccounts) ?? entityList;
                            }
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<EntityCheck> ApiCheckBankAccountByNipAsync (string nip, string bankAccount)
        /// <summary>
        /// Sprawdź, czy dany rachunek jest przypisany do podmiotu według numeru NIP i numeru rachunku bankowego NRB
        /// Check if a given account is assigned to the entity according to the NIP number and NRB bank account number
        /// https://wl-test.mf.gov.pl/api/check/nip/{nip}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// https://wl-api.mf.gov.pl/api/check/nip/{nip}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="nip">
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        /// NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <returns>
        /// Odpowiedź, czy dany rachunek jest przypisany do podmiotu jako EntityCheck
        /// Reply whether the account is assigned to the subject as EntityCheck
        /// </returns>
        public static async Task<EntityCheck> ApiCheckBankAccountByNipAsync(string nip, string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != nip && !string.IsNullOrWhiteSpace(nip) && null != bankAccount && !string.IsNullOrWhiteSpace(bankAccount))
                    {
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/check/nip/{nip}/bank-account/{bankAccount}").AddUrlSegment("nip", nip).AddUrlSegment("bankAccount", bankAccount).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityCheckResponse> response = await client.ExecuteAsync<EntityCheckResponse>(request);
                        //_log4net.Debug($"{ response.StatusCode } { response.Content }");
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result)
                        {
                            EntityCheck entityCheck = response.Data.Result;
                            entityCheck.UniqueIdentifierOfTheLoggedInUser = "test";
                            entityCheck.Nip = nip;
                            entityCheck.AccountNumber = bankAccount;
                            IFormatProvider culture = new CultureInfo("pl-PL", true);
                            entityCheck.RequestDateTimeAsDate = DateTime.ParseExact(entityCheck.RequestDateTime, "dd-MM-yyyy HH:mm:ss", culture);
                            entityCheck.DateOfModification = DateTime.Now;
                            try
                            {
                                using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                                {
                                    context.Entry(entityCheck).State = EntityState.Added;
                                    await context.SaveChangesAsync();
                                }
                            }
                            catch (Exception e)
                            {
                                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                            }
                            return entityCheck;
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static async Task<EntityCheck> ApiCheckBankAccountByRegonAsync(string regon, string bankAccount)
        /// <summary>
        /// Sprawdź, czy dany rachunek jest przypisany do podmiotu według numeru rachunku bankowego NRB i numeru REGON
        /// Check if a given account is assigned to the entity according to the NRB bank account number and REGON number
        /// https://wl-test.mf.gov.pl/api/check/regon/{regon}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// https://wl-api.mf.gov.pl/api/check/regon/{regon}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="regon">
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </param>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <returns>
        /// Odpowiedź, czy dany rachunek jest przypisany do podmiotu jako EntityCheck
        /// Reply whether the account is assigned to the subject as EntityCheck
        /// </returns>
        public static async Task<EntityCheck> ApiCheckBankAccountByRegonAsync(string regon, string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != regon && !string.IsNullOrWhiteSpace(regon) && null != bankAccount && !string.IsNullOrWhiteSpace(bankAccount))
                    {
                        RestClient client = new RestClient(_restClientUrl);
                        RestRequest request = (RestRequest)new RestRequest(@"/api/check/regon/{regon}/bank-account/{bankAccount}").AddUrlSegment("regon", regon).AddUrlSegment("bankAccount", bankAccount).AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityCheckResponse> response = await client.ExecuteAsync<EntityCheckResponse>(request);
                        //_log4net.Debug($"{ response.StatusCode } { response.Content }");
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && null != response.Data.Result)
                        {
                            EntityCheck entityCheck = response.Data.Result;
                            entityCheck.UniqueIdentifierOfTheLoggedInUser = "test";
                            entityCheck.Regon = regon;
                            entityCheck.AccountNumber = bankAccount;
                            IFormatProvider culture = new CultureInfo("pl-PL", true);
                            entityCheck.RequestDateTimeAsDate = DateTime.ParseExact(entityCheck.RequestDateTime, "dd-MM-yyyy HH:mm:ss", culture);
                            entityCheck.DateOfModification = DateTime.Now;
                            try
                            {
                                using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DatabaseMssql.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                                {
                                    context.Entry(entityCheck).State = EntityState.Added;
                                    await context.SaveChangesAsync();
                                }
                            }
                            catch (Exception e)
                            {
                                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                            }
                            return entityCheck;
                        }
                    }
                    return null;
                });
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion
    }
}