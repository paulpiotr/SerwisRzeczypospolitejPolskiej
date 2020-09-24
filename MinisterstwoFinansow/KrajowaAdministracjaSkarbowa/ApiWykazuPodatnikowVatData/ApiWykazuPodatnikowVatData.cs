using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
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
        /// Wartość parametru RestClientUrl z pliku ApiWykazuPodatnikowVatData.json
        /// Paramentr url https://wl-test.mf.gov.pl or https://wl-api.mf.gov.pl
        /// The value of the RestClientUrl parameter from the ApiWykazuPodatnikowVatData.json file
        /// </summary>
        private static readonly string _restClientUrl = NetAppCommon.DataConfiguration.GetValue<string>("ApiWykazuPodatnikowVatData.json", "RestClientUrl");
        #endregion

        #region private static readonly string _connectionStrings
        /// <summary>
        /// Połączenie do bazy danych pobrane z pliku konfigracyjnego aplikacji ApiWykazuPodatnikowVatData.json.
        /// Database connection taken from the ApiWykazuPodatnikowVatData.json application configuration file.
        /// </summary>
        private static readonly string _connectionStrings = NetAppCommon.DataContext.GetConnectionString("ApiWykazuPodatnikowVatDataDbContext", "ApiWykazuPodatnikowVatData.json");
        #endregion

        #region private static readonly int _cacheLifeTime
        /// <summary>
        /// Czas życia pamięci podręcznej dla zapytań do serwisu
        /// Cache lifetime for site queries
        /// </summary>
        private static readonly int _cacheLifetimeForSiteQueries = NetAppCommon.DataConfiguration.GetValue<int>("ApiWykazuPodatnikowVatData.json", "CacheLifetimeForSiteQueries");
        #endregion

        #region private static async Task<Entity> FindByNipAndModificationDateAsync(string nip)
        /// <summary>
        /// Znajdź Entity według nipu i ostatniej daty modyfikacji
        /// Find Entity by NIP and Last Modified Date
        /// </summary>
        /// <param name="nip">
        /// Parament NIP jako string
        /// NIP parameter as a string
        /// </param>
        /// <returns></returns>
        private static async Task<Entity> FindByNipAndModificationDateAsync(string nip)
        {
            try
            {
                using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                {
                    bool canConnectAsync = await context.Database.CanConnectAsync();
                    if (canConnectAsync)
                    {
                        return await Task.Run(() =>
                        {
                            Entity entity = context.Entity.Where(w => !string.IsNullOrWhiteSpace(nip) && !string.IsNullOrWhiteSpace(w.Nip) && w.Nip == nip && w.DateOfModification >= DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).FirstOrDefault();
                            if (null != entity)
                            {
                                return entity;
                            }
                            return null;
                        });
                    }
                }
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
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        private static async Task<List<Entity>> FindByNipsAsync(string nips)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> nipList = new List<string>(nips.Split(',')).ToList();
                            if (null != nipList && nipList.Count > 0)
                            {
                                return context.Entity.Where(w => nipList.Contains(w.Nip)).ToList();
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
        /// Znajdź podmioty według listy numerów NIP jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla parametru CacheLifetimeForSiteQueries
        /// Find entities by NIP number list if the modification date is greater than or equal to the date calculated for the CacheLifetimeForSiteQueries parameter
        /// </summary>
        /// <param name="nips">
        /// Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        /// A list of up to 30 NIP, separated by commas
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        private static async Task<List<Entity>> FindByNipsAndModificationDateAsync(string nips)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
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
                                return context.Entity.Where(w => nipList.Contains(w.Nip)).ToList();
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
        /// Znajdź Entity według regonu i ostatniej daty modyfikacji
        /// Find Entity by NIP and Last Modified Date
        /// </summary>
        /// <param name="regon">
        /// Parament NIP jako string
        /// NIP parameter as a string
        /// </param>
        /// <returns></returns>
        private static async Task<Entity> FindByRegonAndModificationDateAsync(string regon)
        {
            try
            {
                using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                {
                    bool canConnectAsync = await context.Database.CanConnectAsync();
                    if (canConnectAsync)
                    {
                        return await Task.Run(() =>
                        {
                            Entity entity = context.Entity.Where(w => !string.IsNullOrWhiteSpace(regon) && !string.IsNullOrWhiteSpace(w.Regon) && w.Regon == regon && w.DateOfModification >= DateTime.Now.AddSeconds((double)_cacheLifetimeForSiteQueries * -1)).FirstOrDefault();
                            if (null != entity)
                            {
                                return entity;
                            }
                            return null;
                        });
                    }
                }
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
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        private static async Task<List<Entity>> FindByRegonsAsync(string regons)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> regonList = new List<string>(regons.Split(',')).ToList();
                            if (null != regonList && regonList.Count > 0)
                            {
                                return context.Entity.Where(w => regonList.Contains(w.Regon)).ToList();
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
        /// Znajdź podmioty według listy numerów REGON jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla parametru CacheLifetimeForSiteQueries
        /// Find entities by REGON number list if the modification date is greater than or equal to the date calculated for the CacheLifetimeForSiteQueries parameter
        /// </summary>
        /// <param name="regons">
        /// Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        /// A list of up to 30 REGON, separated by commas
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        private static async Task<List<Entity>> FindByRegonsAndModificationDateAsync(string regons)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
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
                                return context.Entity.Where(w => regonList.Contains(w.Regon)).ToList();
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
        /// Znajdź Entity według bankAccountu i ostatniej daty modyfikacji
        /// Find Entity by NIP and Last Modified Date
        /// </summary>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (Numer Rachunku Bankowego) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// Bank account number (26 characters) in the format NRB (Bank Account Number) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        private static async Task<List<Entity>> FindByBankAccountAndModificationDateAsync(string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    List<Entity> entityList = null;
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
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
                                return context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(bankAccount) && !string.IsNullOrWhiteSpace(f.AccountNumber) && f.AccountNumber.Contains(bankAccount) select f.EntityId).Contains(w.Id)).ToList();
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
        /// Znajdź Entity według bankAccountu i ostatniej daty modyfikacji
        /// Find Entity by NIP and Last Modified Date
        /// </summary>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (Numer Rachunku Bankowego) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// Bank account number (26 characters) in the format NRB (Bank Account Number) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns></returns>
        private static async Task<List<Entity>> FindByBankAccountAsync(string bankAccount)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {

                            List<Entity> entityList = context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(bankAccount) && !string.IsNullOrWhiteSpace(f.AccountNumber) && f.AccountNumber.Contains(bankAccount) select f.EntityId).Contains(w.Id)).ToList();
                            if (null != entityList && entityList.Count > 0)
                            {
                                return entityList;
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

        #region private static async Task<List<Entity>> FindByBankAccountsAsync(string bankAccounts)
        /// <summary>
        /// Znajdź podmioty wedłóg listy numerów rachunków NRB
        /// Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        /// Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie NRB (Numer Rachunku Bankowego) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        private static async Task<List<Entity>> FindByBankAccountsAsync(string bankAccounts)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
                    {
                        bool canConnectAsync = await context.Database.CanConnectAsync();
                        if (canConnectAsync)
                        {
                            List<string> bankAccountsList = new List<string>(bankAccounts.Split(',')).ToList();
                            if (null != bankAccountsList && bankAccountsList.Count > 0)
                            {
                                return context.Entity.Where(w => (from f in context.EntityAccountNumber where !string.IsNullOrWhiteSpace(f.AccountNumber) && bankAccountsList.Contains(f.AccountNumber) select f.EntityId).Contains(w.Id)).ToList();
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
        /// Parametr Entity
        /// Entity parameter
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
                        using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
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
        /// Parametr Lista rachunków bankowych jako List <string>
        /// Parameter List of bank accounts as List <string>
        /// </param>
        /// <param name="entity">
        /// Parametr Entity
        /// Entity parameter
        /// </param>
        /// <returns>
        /// Lista numerów kont bankowwych dla podmiotu jako List <EntityAccountNumber>
        /// List of bank account numbers for the entity as List <EntityAccountNumber>
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
                        using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
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
        /// Dodaj lub zaktualizuj rekord w bazie danych.
        /// Add or update a record in the database.
        /// </summary>
        /// <param name="entity">
        /// Parametr Entity
        /// Entity parameter
        /// </param>
        /// <returns></returns>
        private static async Task<Entity> AddOrModifyEntity(Entity entity)
        {
            try
            {
                return await Task.Run<Entity>(async () =>
                {
                    if (null != entity && null != entity.Nip && (!string.IsNullOrWhiteSpace(entity.Nip) || !string.IsNullOrWhiteSpace(entity.Pesel) || !string.IsNullOrWhiteSpace(entity.Regon)))
                    {
                        using (Data.ApiWykazuPodatnikowVatDataDbContext context = await NetAppCommon.DataContext.CreateInstancesForDatabaseContextClassAsync<Data.ApiWykazuPodatnikowVatDataDbContext>())
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
                            int isEntitySaveChangesAsync = await context.SaveChangesAsync();
                            _log4net.Debug($"Save Entity Changes Async to database: { isEntitySaveChangesAsync } id: { entity.Id }");
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
                                return context.Entity.Where(w => !string.IsNullOrWhiteSpace(w.Nip) && !string.IsNullOrWhiteSpace(entity.Nip) && w.Nip == entity.Nip).FirstOrDefault();
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

        #region public static async Task<Entity> ApiFindByNipAsync(string nip)
        /// <summary>
        /// GET https://wl-test.mf.gov.pl/api/search/nip/{nip}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/nip/{nip}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="nip"></param>
        /// <returns></returns>
        public static async Task<Entity> ApiFindByNipAsync(string nip)
        {
            try
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
        /// GET https://wl-test.mf.gov.pl/api/search/regon/{regon}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/regon/{regon}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="regon"></param>
        /// <returns></returns>
        public static async Task<Entity> ApiFindByRegonAsync(string regon)
        {
            try
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
        /// GET https://wl-test.mf.gov.pl/api/search/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="bankAccount">
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (Numer Rachunku Bankowego) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// Bank account number (26 characters) in the format NRB (Bank Account Number) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns></returns>
        public static async Task<List<Entity>> ApiFindByBankAccountAsync(string bankAccount)
        {
            try
            {
                return await Task.Run<List<Entity>>(async () =>
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
                                return await FindByBankAccountAsync(bankAccount);
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
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
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
                                return await FindByNipsAsync(nips);
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
        /// Znajdź podmioty wedłóg listy numerów REGON
        /// Find entities according to the list of REGON numbers
        /// </summary>
        /// <param name="regons">
        /// Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        /// A list of up to 30 REGON, separated by commas
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
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
                                return await FindByRegonsAsync(regons);
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
        /// Znajdź podmioty wedłóg listy numerów rachunków NRB
        /// Find entities by the list of NRB account numbers
        /// GET https://wl-test.mf.gov.pl/api/search/bank-accounts/{bankAccounts}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/bank-accounts/{bankAccounts}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="bankAccounts">
        /// Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie NRB (Numer Rachunku Bankowego) kkAAAAAAAABBBBBBBBBBBBBBBB
        /// A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <returns>
        /// Lista podmiotów jako List <Entity>
        /// Entity list as List <Entity>
        /// </returns>
        public static async Task<List<Entity>> ApiFindByBankAccountsAsync(string bankAccounts)
        {
            try
            {
                return await Task.Run<List<Entity>>(async () =>
                {
                    if (null != _restClientUrl && !string.IsNullOrWhiteSpace(_restClientUrl) && null != bankAccounts && !string.IsNullOrWhiteSpace(bankAccounts))
                    {
                        //List<Entity> findByBankAccountsAndModificationDateList = await FindByBankAccountsAndModificationDateAsync(bankAccounts);
                        //if (null != findByBankAccountsAndModificationDateList)
                        //{
                        //    return findByBankAccountsAndModificationDateList;
                        //}
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
                                return await FindByBankAccountsAsync(bankAccounts);
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
    }
}