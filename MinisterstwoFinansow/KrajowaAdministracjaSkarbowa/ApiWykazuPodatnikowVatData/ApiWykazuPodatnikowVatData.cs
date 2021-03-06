#region using

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using ApiWykazuPodatnikowVatData.Data;
using ApiWykazuPodatnikowVatData.Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using Z.EntityFramework.Plus;

#endregion

namespace ApiWykazuPodatnikowVatData
{
    #region public class ApiWykazuPodatnikowVatData

    /// <summary>
    ///     Klasa usługi https://wl-test.mf.gov.pl lub https://wl-api.mf.gov.pl
    ///     Service class https://wl-test.mf.gov.pl or https://wl-api.mf.gov.pl
    /// </summary>
    public class ApiWykazuPodatnikowVatData
    {
        #region private static readonly AppSettings appSettings

        /// <summary>
        ///     Instancja do modelu ustawień jako AppSettings
        ///     Instance to the settings model as AppSettings
        /// </summary>
        private readonly AppSettings _appSettings = new();

        #endregion

        #region private ApiWykazuPodatnikowVatDataDbContext context;

        /// <summary>
        ///     Kontekst bazy danych jako ApiWykazuPodatnikowVatDataDbContext
        ///     Database context as ApiWykazuPodatnikowVatDataDbContext
        /// </summary>
        private readonly ApiWykazuPodatnikowVatDataDbContext _context;

        #endregion

        # region private readonly log4net.ILog log4net

        /// <summary>
        ///     private readonly ILog _log4Net
        /// </summary>
        private readonly ILog _log4Net =
            Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod()?.DeclaringType);

        #endregion

        #region public ApiWykazuPodatnikowVatData()

        /// <summary>
        ///     Konstruktor
        ///     Constructor
        /// </summary>
        public ApiWykazuPodatnikowVatData()
        {
            _context = new ApiWykazuPodatnikowVatDataDbContext(_appSettings
                .GetDbContextOptions<ApiWykazuPodatnikowVatDataDbContext>());
        }

        #endregion

        #region public ApiWykazuPodatnikowVatData...

        /// <summary>
        ///     Konstruktor
        ///     Constructor
        /// </summary>
        /// <param name="context">
        ///     Kontekst bazy danych jako ApiWykazuPodatnikowVatDataDbContext
        ///     Database context as ApiWykazuPodatnikowVatDataDbContext
        /// </param>
        public ApiWykazuPodatnikowVatData(ApiWykazuPodatnikowVatDataDbContext context)
        {
            _context = context;
        }

        #endregion

        #region public static ApiWykazuPodatnikowVatData GetInstance()

        /// <summary>
        ///     Utwórz i pobierz statyczną instancję do klasy ApiWykazuPodatnikowVatData
        ///     Create and get a static instance to the ApiWykazuPodatnikowVatData class
        /// </summary>
        /// <returns>
        ///     Statyczna instancja do klasy ApiWykazuPodatnikowVatData
        ///     A static instance to the ApiWykazuPodatnikowVatData class
        /// </returns>
        public static ApiWykazuPodatnikowVatData GetInstance() => new();

        #endregion

        #region public static ApiWykazuPodatnikowVatData GetInstance...

        /// <summary>
        ///     Utwórz i pobierz statyczną instancję do klasy ApiWykazuPodatnikowVatData
        ///     Create and get a static instance to the ApiWykazuPodatnikowVatData class
        /// </summary>
        /// <param name="context">
        ///     Kontekst bazy danych jako ApiWykazuPodatnikowVatDataDbContext
        ///     Database context as ApiWykazuPodatnikowVatDataDbContext
        /// </param>
        /// <returns>
        ///     Statyczna instancja do klasy ApiWykazuPodatnikowVatData
        ///     A static instance to the ApiWykazuPodatnikowVatData class
        /// </returns>
        public static ApiWykazuPodatnikowVatData GetInstance(ApiWykazuPodatnikowVatDataDbContext context) =>
            new(context);

        #endregion

        #region private async Task<Entity> FindByNipAsync...

        /// <summary>
        ///     Znajdź podmiot według numeru NIP
        ///     Find entity by tax identification NIP number
        /// </summary>
        /// <param name="nip">
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$],
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Podmiot jako obiekt Entity lub null,
        ///     Entity as an Entity or null object
        /// </returns>
        private async Task<Entity> FindByNipAsync(string nip, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    return await _context.Entity
                        .Where(w => !string.IsNullOrWhiteSpace(nip) && !string.IsNullOrWhiteSpace(w.Nip) &&
                                    w.Nip == nip && w.DateOfChecking.HasValue &&
                                    w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                    w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                    w.DateOfChecking.Value.Day == dateOfChecking.Day)
                        .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                        .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                        .IncludeOptimized(w => w.RequestAndResponseHistory)
                        .IncludeOptimized(w => w.RequestAndResponseHistory).FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<Entity> FindByNipAndModificationDateAsync...

        /// <summary>
        ///     Znajdź podmiot według numeru NIP i ostatniej daty modyfikacji,
        ///     Find entity by tax identification NIP number and last modified date
        /// </summary>
        /// <param name="nip">
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$],
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Podmiot jako obiekt Entity lub null,
        ///     Entity as an Entity or null object
        /// </returns>
        private async Task<Entity> FindByNipAndModificationDateAsync(string nip, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    return await _context.Entity
                        .Where(w => !string.IsNullOrWhiteSpace(nip) && !string.IsNullOrWhiteSpace(w.Nip) &&
                                    w.Nip == nip && w.DateOfChecking.HasValue &&
                                    w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                    w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                    w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification >=
                                    DateTime.Now.AddSeconds((double)_appSettings.CacheLifeTime * -1))
                        .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                        .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                        .IncludeOptimized(w => w.RequestAndResponseHistory)
                        .IncludeOptimized(w => w.RequestAndResponseHistory).FirstOrDefaultAsync();
                    //return context.Entity.Where(w => !string.IsNullOrWhiteSpace(nip) && !string.IsNullOrWhiteSpace(w.Nip) && w.Nip == nip &&  w.DateOfChecking.HasValue && w.DateOfChecking.Value.Year == dateOfChecking.Year && w.DateOfChecking.Value.Month == dateOfChecking.Month && w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification >= DateTime.Now.AddSeconds((double)appSettings.CacheLifeTime * -1)).FromCache(context.GetMemoryCacheEntryOptions(), nip, dateOfChecking.ToShortDateString()).FirstOrDefault();
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByNipsAsync...

        /// <summary>
        ///     Znajdź podmioty wedłóg listy numerów NIP
        ///     Find entities according to the list of NIP numbers
        /// </summary>
        /// <param name="nips">
        ///     Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        ///     A list of up to 30 NIP, separated by commas
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByNipsAsync(string nips, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    var nipList = new List<string>(nips.Split(',')).ToList();
                    if (nipList.Count > 0)
                    {
                        return await _context.Entity
                            .Where(w => nipList.Contains(w.Nip) && w.DateOfChecking.HasValue &&
                                        w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                        w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                        w.DateOfChecking.Value.Day == dateOfChecking.Day)
                            .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                            .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                            .IncludeOptimized(w => w.RequestAndResponseHistory).ToListAsync();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByNipsAndModificationDateAsync...

        /// <summary>
        ///     Znajdź podmioty według listy numerów NIP jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla
        ///     parametru appSettings.CacheLifeTime
        ///     Find entities by NIP number list if the modification date is greater than or equal to the date calculated for the
        ///     appSettings.CacheLifeTime parameter
        /// </summary>
        /// <param name="nips">
        ///     Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        ///     A list of up to 30 NIP, separated by commas
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByNipsAndModificationDateAsync(string nips, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    var nipList = new List<string>(nips.Split(',')).ToList();
                    if (nipList.Count > 0)
                    {
                        if (await _context.Entity.Where(w =>
                            (from f in _context.Entity
                                where !string.IsNullOrWhiteSpace(w.Nip) && nipList.Contains(w.Nip)
                                select f.Id).Contains(w.Id) && w.DateOfChecking.HasValue &&
                            w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                            w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                            w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification <
                            DateTime.Now.AddSeconds((double)_appSettings.CacheLifeTime * -1)).AnyAsync())
                        {
                            return null;
                        }

                        return await _context.Entity
                            .Where(w => nipList.Contains(w.Nip) && w.DateOfChecking.HasValue &&
                                        w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                        w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                        w.DateOfChecking.Value.Day == dateOfChecking.Day)
                            .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                            .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                            .IncludeOptimized(w => w.RequestAndResponseHistory).ToListAsync();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<Entity> FindByRegonAndModificationDateAsync...

        /// <summary>
        ///     Znajdź podmiot według numeru REGON jeśli data modyfikacji jest większa lub równa od daty obliczonej dla parametru
        ///     appSettings.CacheLifeTime
        ///     Find the entity by REGON number if the modification date is greater than or equal to the date calculated for the
        ///     appSettings.CacheLifeTime parameter
        /// </summary>
        /// <param name="regon">
        ///     Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string
        ///     [^\d{9}$|^\d{14}$]
        ///     REGON identification number assigned by the National Register of Entities of National Economy as string
        ///     [^\d{9}$|^\d{14}$]
        /// </param>
        /// <returns>
        ///     Podmiot jako obiekt Entity lub null,
        ///     Entity as an Entity or null object
        /// </returns>
        private async Task<Entity> FindByRegonAndModificationDateAsync(string regon, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    return await _context.Entity
                        .Where(w => !string.IsNullOrWhiteSpace(regon) && !string.IsNullOrWhiteSpace(w.Regon) &&
                                    w.Regon == regon && w.DateOfChecking.HasValue &&
                                    w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                    w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                    w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification >=
                                    DateTime.Now.AddSeconds((double)_appSettings.CacheLifeTime * -1))
                        .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                        .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                        .IncludeOptimized(w => w.RequestAndResponseHistory)
                        .IncludeOptimized(w => w.RequestAndResponseHistory).FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByRegonsAsync...

        /// <summary>
        ///     Znajdź podmioty wedłóg listy numerów REGON
        ///     Find entities according to the list of REGON numbers
        /// </summary>
        /// <param name="regons">
        ///     Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        ///     A list of up to 30 REGON, separated by commas
        ///     Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string
        ///     [^\d{9}$|^\d{14}$]
        ///     REGON identification number assigned by the National Register of Entities of National Economy as string
        ///     [^\d{9}$|^\d{14}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByRegonsAsync(string regons, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    var regonList = new List<string>(regons.Split(',')).ToList();
                    if (regonList.Count > 0)
                    {
                        return await _context.Entity
                            .Where(w => regonList.Contains(w.Regon) && w.DateOfChecking.HasValue &&
                                        w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                        w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                        w.DateOfChecking.Value.Day == dateOfChecking.Day)
                            .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                            .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                            .IncludeOptimized(w => w.RequestAndResponseHistory).ToListAsync();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByRegonsAndModificationDateAsync...

        /// <summary>
        ///     Znajdź podmioty według listy numerów REGON jeśli data modyfikacji jest więkasza lub równa od daty obliczonej dla
        ///     parametru appSettings.CacheLifeTime
        ///     Find entities by REGON number list if the modification date is greater than or equal to the date calculated for the
        ///     appSettings.CacheLifeTime parameter
        /// </summary>
        /// <param name="regons">
        ///     Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        ///     A list of up to 30 REGON, separated by commas
        ///     Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string
        ///     [^\d{9}$|^\d{14}$]
        ///     REGON identification number assigned by the National Register of Entities of National Economy as string
        ///     [^\d{9}$|^\d{14}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByRegonsAndModificationDateAsync(string regons, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    var regonList = new List<string>(regons.Split(',')).ToList();
                    if (regonList.Count > 0)
                    {
                        if (await _context.Entity.Where(w =>
                            (from f in _context.Entity
                                where !string.IsNullOrWhiteSpace(w.Regon) && regonList.Contains(w.Regon)
                                select f.Id).Contains(w.Id) && w.DateOfChecking.HasValue &&
                            w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                            w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                            w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification <
                            DateTime.Now.AddSeconds((double)_appSettings.CacheLifeTime * -1)).AnyAsync())
                        {
                            return null;
                        }

                        return await _context.Entity
                            .Where(w => regonList.Contains(w.Regon) && w.DateOfChecking.HasValue &&
                                        w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                        w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                        w.DateOfChecking.Value.Day == dateOfChecking.Day)
                            .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                            .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                            .IncludeOptimized(w => w.RequestAndResponseHistory).ToListAsync();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<Entity> FindByBankAccountAndModificationDateAsync...

        /// <summary>
        ///     Znajdź podmioty według numeru rachunku bankowego NRB jeśli data modyfikacji jest więkasza lub równa od daty
        ///     obliczonej dla parametru appSettings.CacheLifeTime
        ///     Find entities by NRB bank account number if the modification date is greater than or equal to the date calculated
        ///     for the appSettings.CacheLifeTime parameter
        /// </summary>
        /// <param name="bankAccount">
        ///     Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista Podmiotów jako lista obiektów Entity lub null
        ///     Entity List as a list of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByBankAccountAndModificationDateAsync(string bankAccount,
            DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    if (await _context.Entity.Where(w =>
                        (from f in _context.EntityAccountNumber
                            where !string.IsNullOrWhiteSpace(bankAccount) &&
                                  !string.IsNullOrWhiteSpace(f.AccountNumber) && f.AccountNumber.Contains(bankAccount)
                            select f.EntityId).Contains(w.Id) && w.DateOfChecking.HasValue &&
                        w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                        w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                        w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification <
                        DateTime.Now.AddSeconds((double)_appSettings.CacheLifeTime * -1)).AnyAsync())
                    {
                        return null;
                    }

                    return await _context.Entity
                        .Where(w =>
                            (from f in _context.EntityAccountNumber
                                where !string.IsNullOrWhiteSpace(bankAccount) &&
                                      !string.IsNullOrWhiteSpace(f.AccountNumber) &&
                                      f.AccountNumber.Contains(bankAccount)
                                select f.EntityId).Contains(w.Id) && w.DateOfChecking.HasValue &&
                            w.DateOfChecking.HasValue && w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                            w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                            w.DateOfChecking.Value.Day == dateOfChecking.Day)
                        .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                        .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                        .IncludeOptimized(w => w.RequestAndResponseHistory).ToListAsync();
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByBankAccountAsync...

        /// <summary>
        ///     Znajdź podmioty według numeru rachunku bankowego NRB
        ///     Find entities by NRB bank account number
        /// </summary>
        /// <param name="bankAccount">
        ///     Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista Podmiotów jako lista obiektów Entity lub null
        ///     Entity List as a list of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByBankAccountAsync(string bankAccount, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    return _context.Entity
                        .Where(w =>
                            (from f in _context.EntityAccountNumber
                                where !string.IsNullOrWhiteSpace(bankAccount) &&
                                      !string.IsNullOrWhiteSpace(f.AccountNumber) &&
                                      f.AccountNumber.Contains(bankAccount)
                                select f.EntityId).Contains(w.Id) && w.DateOfChecking.HasValue &&
                            w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                            w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                            w.DateOfChecking.Value.Day == dateOfChecking.Day)
                        .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                        .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                        .IncludeOptimized(w => w.RequestAndResponseHistory).ToList();
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByBankAccountsAsync...

        /// <summary>
        ///     Znajdź podmioty wedłóg listy numerów rachunków NRB
        ///     Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        ///     Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie
        ///     NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank
        ///     Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByBankAccountsAsync(string bankAccounts, DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    var bankAccountsList = new List<string>(bankAccounts.Split(',')).ToList();
                    if (bankAccountsList.Count > 0)
                    {
                        return _context.Entity
                            .Where(w =>
                                (from f in _context.EntityAccountNumber
                                    where !string.IsNullOrWhiteSpace(f.AccountNumber) &&
                                          bankAccountsList.Contains(f.AccountNumber)
                                    select f.EntityId).Contains(w.Id) && w.DateOfChecking.HasValue &&
                                w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                w.DateOfChecking.Value.Day == dateOfChecking.Day)
                            .IncludeOptimized(w => w.EntityAccountNumber).IncludeOptimized(w => w.AuthorizedClerk)
                            .IncludeOptimized(w => w.Partner).IncludeOptimized(w => w.Representative)
                            .IncludeOptimized(w => w.RequestAndResponseHistory).ToList();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<Entity>> FindByBankAccountsAndModificationDateAsync...

        /// <summary>
        ///     Znajdź podmioty wedłóg listy numerów rachunków NRB
        ///     Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        ///     Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie
        ///     NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank
        ///     Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        private async Task<List<Entity>> FindByBankAccountsAndModificationDateAsync(string bankAccounts,
            DateTime dateOfChecking) =>
            await Task.Run(async () =>
            {
                try
                {
                    var bankAccountsList = new List<string>(bankAccounts.Split(',')).ToList();
                    if (bankAccountsList.Count > 0)
                    {
                        if (await _context.Entity.Where(w =>
                            (from f in _context.EntityAccountNumber
                                where !string.IsNullOrWhiteSpace(f.AccountNumber) &&
                                      bankAccountsList.Contains(f.AccountNumber)
                                select f.EntityId).Contains(w.Id) && w.DateOfChecking.HasValue &&
                            w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                            w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                            w.DateOfChecking.Value.Day == dateOfChecking.Day && w.DateOfModification <
                            DateTime.Now.AddSeconds((double)_appSettings.CacheLifeTime * -1)).AnyAsync())
                        {
                            return null;
                        }

                        return await _context.Entity
                            .Where(w => (from f in _context.EntityAccountNumber
                                where !string.IsNullOrWhiteSpace(f.AccountNumber) &&
                                      bankAccountsList.Contains(f.AccountNumber) && w.DateOfChecking.HasValue &&
                                      w.DateOfChecking.Value.Year == dateOfChecking.Year &&
                                      w.DateOfChecking.Value.Month == dateOfChecking.Month &&
                                      w.DateOfChecking.Value.Day == dateOfChecking.Day
                                select f.EntityId).Contains(w.Id)).IncludeOptimized(w => w.EntityAccountNumber)
                            .IncludeOptimized(w => w.AuthorizedClerk).IncludeOptimized(w => w.Partner)
                            .IncludeOptimized(w => w.Representative).IncludeOptimized(w => w.RequestAndResponseHistory)
                            .ToListAsync();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region private async Task<List<EntityAccountNumber>> AddOrModifyEntityAccountNumberAsync...

        /// <summary>
        ///     Dodaj lub zmodyfikuj numer konta bankowego dla podmiotu
        ///     Add or modify the bank account number for the entity
        /// </summary>
        /// <param name="accountNumbersList">
        ///     Parametr Lista rachunków bankowych jako List
        ///     <string>
        ///         Parameter List of bank accounts as List <string>
        /// </param>
        /// <param name="entity">
        ///     Obiekt podmiotu jako Entity
        ///     The subject object as Entity
        /// </param>
        /// <returns>
        ///     Lista numerów kont bankowwych dla podmiotu jako List
        ///     <EntityAccountNumber>
        ///         List of bank account numbers for the entity as List <EntityAccountNumber>
        /// </returns>
        private async Task<List<EntityAccountNumber>> AddOrModifyEntityAccountNumberAsync(
            List<string> accountNumbersList, Entity entity)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (null != accountNumbersList && accountNumbersList.Count > 0)
                    {
                        await _context.EntityAccountNumber
                            .Where(w => w.EntityId == entity.Id && !accountNumbersList.Contains(w.AccountNumber))
                            .DeleteFromQueryAsync();

                        async Task AddOrUpdateEntityAccountNumber(string accountNumber)
                        {
                            try
                            {
                                if (null != accountNumber && !string.IsNullOrWhiteSpace(accountNumber) &&
                                    !_context.EntityAccountNumber.Any(w =>
                                        w.EntityId == entity.Id && w.AccountNumber.Contains(accountNumber)))
                                {
                                    var entityAccountNumber = new EntityAccountNumber
                                    {
                                        Entity = entity, AccountNumber = accountNumber
                                    };
                                    EntityState entityState = EntityState.Added;
                                    _context.Entry(entityAccountNumber).State = entityState;
                                    var isEntityAccountNumberAdded = await _context.SaveChangesAsync();
#if DEBUG
                                    _log4Net.Debug(
                                        $"Save EntityAccountNumber Changes Async [{entityState}] to database: [{isEntityAccountNumberAdded}] id: [{entityAccountNumber.Id}]");
#endif
                                }
                            }
                            catch (Exception e)
                            {
                                await Task.Run(() =>
                                {
                                    _log4Net.Error(
                                        $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                        e);
                                });
                            }
                        }

                        foreach (var accountNumber in accountNumbersList)
                        {
                            await AddOrUpdateEntityAccountNumber(accountNumber);
                        }

                        return _context.EntityAccountNumber.Where(w =>
                            w.EntityId == entity.Id && accountNumbersList.Contains(w.AccountNumber)).ToList();
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });
        }

        #endregion

        #region private async Task<List<EntityPerson>> AddOrModifyEntityPersonAsync...

        /// <summary>
        ///     Dodaj lub zmodyfikuj numer konta bankowego dla podmiotu
        ///     Add or modify the bank account number for the entity
        /// </summary>
        /// <param name="accountNumbersList">
        ///     Lista maksymalnie 30 numerow rachunkow bankowych rozdzielonych przecinkami jako string
        ///     A list of up to 30 bank account numbers separated by commas as a string
        ///     Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="entity">
        ///     Obiekt podmiotu jako Entity
        ///     The subject object as Entity
        /// </param>
        /// <returns>
        ///     Lista numerów kont bankowych dla podmiotu jako Lista obiektów encji EntityAccountNumber
        ///     List of bank account numbers for the entity as EntityAccountNumber Entity Object List
        /// </returns>
        private async Task<List<EntityPerson>> AddOrModifyEntityPersonAsync(List<EntityPerson> entityPerson,
            Entity entity, string property)
        {
            return await Task.Run<List<EntityPerson>>(async () =>
            {
                try
                {
                    if (null != entityPerson && entityPerson.Count > 0 &&
                        new List<string> {"EntityRepresentativeId", "EntityAuthorizedClerkId", "EntityPartnerId"}
                            .Contains(property))
                    {
                        PropertyInfo propertyInfo = entityPerson.FirstOrDefault()?.GetType().GetProperty(property);
                        try
                        {
                            _context.EntityPerson.RemoveRange(
                                _context.EntityPerson.Where(
                                    w => EF.Property<Guid>(w, property) == entity.Id
                                )
                            );
                            var isEntityPersonRemoveRange = await _context.SaveChangesAsync();
#if DEBUG
                            _log4Net.Debug(
                                $"Remove EntityPerson if is not found in list and Save Changes Async to database: {isEntityPersonRemoveRange}");
#endif
                        }
                        catch (Exception e)
                        {
                            await Task.Run(() =>
                            {
                                _log4Net.Error(
                                    $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                    e);
                            });
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

                                //x.UniqueIdentifierOfTheLoggedInUser = NetAppCommon.HttpContextAccessor.AppContext.GetCurrentUserIdentityName();
                                x.DateOfModification = DateTime.Now;
                            });
                            _context.EntityPerson.AddRange(entityPerson.Where(w =>
                                "00000000-0000-0000-0000-000000000000" == w.Id.ToString()));
                            var isEntityPersonAddRange = await _context.SaveChangesAsync();
#if DEBUG
                            _log4Net.Debug(
                                $"Add EntityPerson if is not found in list and Save Changes Async to database: {isEntityPersonAddRange}");
#endif
                        }
                        catch (Exception e)
                        {
                            await Task.Run(() =>
                            {
                                _log4Net.Error(
                                    $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                    e);
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });
        }

        #endregion

        #region private async Task<RequestAndResponseHistory> FindOrAddRequestAndResponseHistory...

        /// <summary>
        ///     Znajdź lub dodaj historię żądań i odpowiedzi (obiekt modelu RequestAndResponseHistory)
        ///     Find or add request and response history (RequestAndResponseHistory model object)
        /// </summary>
        /// <param name="requestAndResponseHistory">
        ///     obiekt modelu RequestAndResponseHistory
        ///     RequestAndResponseHistory model object
        /// </param>
        /// <returns>
        ///     obiekt modelu RequestAndResponseHistory
        ///     RequestAndResponseHistory model object
        /// </returns>
        private async Task<RequestAndResponseHistory> FindOrAddRequestAndResponseHistoryAsync(
            RequestAndResponseHistory requestAndResponseHistory) =>
            await Task.Run(async () =>
            {
                try
                {
                    if (_context.RequestAndResponseHistory.Any(w =>
                        w.ObjectMD5Hash == requestAndResponseHistory.ObjectMD5Hash))
                    {
                        return await _context.RequestAndResponseHistory
                            .Where(w => w.ObjectMD5Hash == requestAndResponseHistory.ObjectMD5Hash)
                            .FirstOrDefaultAsync();
                    }

                    _context.Entry(requestAndResponseHistory).State = EntityState.Added;
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return requestAndResponseHistory;
            });

        #endregion

        #region private async Task<Entity> AddOrModifyEntity...

        /// <summary>
        ///     Dodaj lub zaktualizuj rekord w bazie danych
        ///     Add or update a record in the database
        /// </summary>
        /// <param name="entityResponse.Result.Subject">
        ///     Obiekt podmiotu jako Entity
        ///     The subject object as Entity
        /// </param>
        /// <returns>
        ///     Zaktualizowany lub wstawiony obiekt Entity lub przekazany obiekt Entity lub null
        ///     An updated or inserted Entity or a passed Entity, or null
        /// </returns>
        private async Task<Entity> AddOrModifyEntityAsync(RequestAndResponseHistory requestAndResponseHistory,
            Entity entity, DateTime dateOfChecking)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var isEntitySaveChanges = 0;
                    try
                    {
                        Entity entityWhere = await FindByNipAsync(entity.Nip, dateOfChecking);
                        if (null != entityWhere)
                        {
                            entity.Id = entityWhere.Id;
                            _context.Entry(entityWhere).State = EntityState.Detached;
                        }

                        entity.RequestAndResponseHistoryId = requestAndResponseHistory.Id;
                        entity.RequestAndResponseHistory = requestAndResponseHistory;
                        entity.DateOfChecking = dateOfChecking;
                        entity.DateOfModification = DateTime.Now;
                        EntityState entityState = "00000000-0000-0000-0000-000000000000" != entity.Id.ToString()
                            ? EntityState.Modified
                            : EntityState.Added;
                        _context.Entry(entity).State = entityState;
                        isEntitySaveChanges = await _context.SaveChangesAsync();
#if DEBUG
                        _log4Net.Debug(
                            $"Save Entity Changes Async [{entityState}] to database: [{isEntitySaveChanges}] id: [{entity.Id}]");
#endif
                    }
                    catch (Exception e)
                    {
                        await Task.Run(() =>
                        {
                            _log4Net.Error(
                                $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                e);
                        });
                    }

                    if (isEntitySaveChanges == 1)
                    {
                        try
                        {
                            var accountNumbersList = (List<string>)entity.AccountNumbers;
                            List<EntityAccountNumber> entityAccountNumbersList =
                                AddOrModifyEntityAccountNumberAsync(accountNumbersList, entity).Result;
                        }
                        catch (Exception e)
                        {
                            await Task.Run(() =>
                            {
                                _log4Net.Error(
                                    $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                    e);
                            });
                        }

                        try
                        {
                            var entityPersonListRepresentatives = (List<EntityPerson>)entity.Representatives;
                            List<EntityPerson> entityPersonListRepresentative =
                                AddOrModifyEntityPersonAsync(entityPersonListRepresentatives, entity,
                                    "EntityRepresentativeId").Result;
                        }
                        catch (Exception e)
                        {
                            await Task.Run(() =>
                            {
                                _log4Net.Error(
                                    $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                    e);
                            });
                        }

                        try
                        {
                            var entityPersonListAuthorizedClerks = (List<EntityPerson>)entity.AuthorizedClerks;
                            List<EntityPerson> entityPersonListAuthorizedClerk =
                                AddOrModifyEntityPersonAsync(entityPersonListAuthorizedClerks, entity,
                                    "EntityAuthorizedClerkId").Result;
                        }
                        catch (Exception e)
                        {
                            await Task.Run(() =>
                            {
                                _log4Net.Error(
                                    $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                    e);
                            });
                        }

                        try
                        {
                            var entityPersonListPartners = (List<EntityPerson>)entity.Partners;
                            List<EntityPerson> entityPersonListPartner =
                                AddOrModifyEntityPersonAsync(entityPersonListPartners, entity, "EntityPartnerId")
                                    .Result;
                        }
                        catch (Exception e)
                        {
                            await Task.Run(() =>
                            {
                                _log4Net.Error(
                                    $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                    e);
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return await FindByNipAsync(entity.Nip, (DateTime)entity.DateOfChecking) ?? entity;
            });
        }

        #endregion

        #region public async Task<Entity> ApiFindByNipAsync...

        /// Wyszukaj podmiot w serwisie mf.gov.pl według numeru NIP
        /// Search for an entity on the mf.gov.pl website by tax identification number NIP
        /// GET https://wl-test.mf.gov.pl/api/search/nip/{nip}?date={date format yyyy-MM-dd}
        /// GET https://wl-api.mf.gov.pl/api/search/nip/{nip}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="nip">
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Podmiot jako obiekt Entity lub null
        ///     Entity as an Entity or null object
        /// </returns>
        public async Task<Entity> ApiFindByNipAsync(string nip, DateTime? dateOfChecking = null) =>
            await Task.Run(async () =>
            {
                try
                {
                    dateOfChecking ??= DateTime.Now;
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != nip && !string.IsNullOrWhiteSpace(nip) && null != dateOfChecking)
                    {
                        Entity entityFindByNipAndModificationDate =
                            await FindByNipAndModificationDateAsync(nip, (DateTime)dateOfChecking);
                        if (null != entityFindByNipAndModificationDate)
                        {
                            return entityFindByNipAndModificationDate;
                        }

                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/search/nip/{nip}").AddUrlSegment("nip", nip)
                            .AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityResponse> response = await client.ExecuteAsync<EntityResponse>(request);
                        if (response?.StatusCode == HttpStatusCode.OK ||
                            response?.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            return null != response?.Data?.Result?.Subject &&
                                   response?.Data?.Result?.Subject?.Nip == nip
                                ? await AddOrModifyEntityAsync(requestAndResponseHistory, response.Data.Result.Subject,
                                    (DateTime)dateOfChecking) ?? response.Data.Result.Subject
                                : new Entity {RequestAndResponseHistory = requestAndResponseHistory};
                        }
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region public async Task<Entity> ApiFindByRegonAsync...

        /// <summary>
        ///     Wyszukaj podmiot w serwisie mf.gov.pl według numeru REGON
        ///     Search for an entity on the mf.gov.pl website by REGON number
        ///     GET https://wl-test.mf.gov.pl/api/search/regon/{regon}?date={date format yyyy-MM-dd}
        ///     GET https://wl-api.mf.gov.pl/api/search/regon/{regon}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="regon">
        ///     Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string
        ///     [^\d{9}$|^\d{14}$]
        ///     REGON identification number assigned by the National Register of Entities of National Economy as string
        ///     [^\d{9}$|^\d{14}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Podmiot jako obiekt Entity lub null
        ///     Entity as an Entity or null object
        /// </returns>
        public async Task<Entity> ApiFindByRegonAsync(string regon, DateTime? dateOfChecking = null) =>
            await Task.Run(async () =>
            {
                try
                {
                    dateOfChecking ??= DateTime.Now;
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != regon && !string.IsNullOrWhiteSpace(regon))
                    {
                        Entity entityFindByRegonAndModificationDate =
                            await FindByRegonAndModificationDateAsync(regon, (DateTime)dateOfChecking);
                        if (null != entityFindByRegonAndModificationDate)
                        {
                            return entityFindByRegonAndModificationDate;
                        }

                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/search/regon/{regon}")
                            .AddUrlSegment("regon", regon).AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityResponse> response = await client.ExecuteAsync<EntityResponse>(request);
                        if (response?.StatusCode == HttpStatusCode.OK ||
                            response?.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            //return await AddOrModifyEntity(requestAndResponseHistory, response.Data.Result.Subject, (DateTime)dateOfChecking) ?? response.Data.Result.Subject;
                            if (null != response?.Data?.Result?.Subject &&
                                response?.Data?.Result?.Subject?.Regon == regon)
                            {
                                return await AddOrModifyEntityAsync(requestAndResponseHistory,
                                           response.Data.Result.Subject, (DateTime)dateOfChecking) ??
                                       response.Data.Result.Subject;
                            }

                            return new Entity {RequestAndResponseHistory = requestAndResponseHistory};
                        }
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region public async Task<Entity> ApiFindByBankAccountAsync...

        /// <summary>
        ///     Wyszukaj podmioty w serwisie mf.gov.pl według numeru rachunku bankowego NRB
        ///     Search and get entities on the mf.gov.pl website according to the NRB bank account number
        ///     GET https://wl-test.mf.gov.pl/api/search/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        ///     GET https://wl-api.mf.gov.pl/api/search/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="bankAccount">
        ///     Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista Podmiotów jako lista obiektów Entity lub null
        ///     Entity List as a list of Entity objects or null
        /// </returns>
        public async Task<List<Entity>>
            ApiFindByBankAccountAsync(string bankAccount, DateTime? dateOfChecking = null) =>
            await Task.Run(async () =>
            {
                try
                {
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != bankAccount && !string.IsNullOrWhiteSpace(bankAccount))
                    {
                        dateOfChecking ??= DateTime.Now;
                        List<Entity> findByBankAccountAndModificationDateList =
                            await FindByBankAccountAndModificationDateAsync(bankAccount, (DateTime)dateOfChecking);
                        if (null != findByBankAccountAndModificationDateList &&
                            findByBankAccountAndModificationDateList.Count > 0)
                        {
                            return findByBankAccountAndModificationDateList;
                        }

                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/search/bank-account/{bankAccount}")
                            .AddUrlSegment("bankAccount", bankAccount).AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityListResponse> response =
                            await client.ExecuteAsync<EntityListResponse>(request);
                        if (response?.StatusCode == HttpStatusCode.OK ||
                            response?.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            var entityList = (List<Entity>)response?.Data?.Result?.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Nip && !string.IsNullOrWhiteSpace(entity.Nip))
                                    {
                                        Entity entityFindByNipAndModificationDate =
                                            await FindByNipAndModificationDateAsync(entity.Nip,
                                                (DateTime)dateOfChecking);
                                        if (null == entityFindByNipAndModificationDate)
                                        {
                                            await AddOrModifyEntityAsync(requestAndResponseHistory, entity,
                                                (DateTime)dateOfChecking);
                                        }
                                    }
                                    else if (null != entity && null != entity.Regon &&
                                             !string.IsNullOrWhiteSpace(entity.Regon))
                                    {
                                        Entity entityFindByRegonAndModificationDate =
                                            await FindByRegonAndModificationDateAsync(entity.Regon,
                                                (DateTime)dateOfChecking);
                                        if (null == entityFindByRegonAndModificationDate)
                                        {
                                            await AddOrModifyEntityAsync(requestAndResponseHistory, entity,
                                                (DateTime)dateOfChecking);
                                        }
                                    }
                                }

                                return await FindByBankAccountAsync(bankAccount, (DateTime)dateOfChecking) ??
                                       new List<Entity> {new() {RequestAndResponseHistory = requestAndResponseHistory}};
                            }

                            return new List<Entity> {new() {RequestAndResponseHistory = requestAndResponseHistory}};
                        }
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() =>
                    {
                        _log4Net.Error(
                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    });
                }

                return null;
            });

        #endregion

        #region public async Task<Entity> ApiFindByNipsAsync...

        /// <summary>
        ///     Znajdź podmioty wedłóg listy numerów NIP
        ///     Find entities according to the list of NIP numbers
        /// </summary>
        /// <param name="nips">
        ///     Lista maksymalnie 30 numerów NIP rozdzielonych przecinkami
        ///     A list of up to 30 NIP, separated by commas
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        public async Task<List<Entity>> ApiFindByNipsAsync(string nips, DateTime? dateOfChecking = null)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    dateOfChecking ??= DateTime.Now;
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != nips && !string.IsNullOrWhiteSpace(nips))
                    {
                        List<Entity> findByNipsAndModificationDateList =
                            await FindByNipsAndModificationDateAsync(nips, (DateTime)dateOfChecking);
                        if (null != findByNipsAndModificationDateList && findByNipsAndModificationDateList.Count > 0)
                        {
                            return findByNipsAndModificationDateList;
                        }

                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/search/nips/{nips}")
                            .AddUrlSegment("nips", nips).AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityListResponse> response =
                            await client.ExecuteAsync<EntityListResponse>(request);
                        _log4Net.Info($"{response.StatusCode} {response.Content}");
                        if (response?.StatusCode == HttpStatusCode.OK ||
                            response?.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            var entityList = (List<Entity>)response?.Data?.Result?.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Nip && !string.IsNullOrWhiteSpace(entity.Nip))
                                    {
                                        Entity entityFindByNipAndModificationDate =
                                            await FindByNipAndModificationDateAsync(entity.Nip,
                                                (DateTime)dateOfChecking);
                                        if (null == entityFindByNipAndModificationDate)
                                        {
                                            await AddOrModifyEntityAsync(requestAndResponseHistory, entity,
                                                (DateTime)dateOfChecking);
                                        }
                                    }
                                }

                                return await FindByNipsAsync(nips, (DateTime)dateOfChecking) ?? new List<Entity>
                                {
                                    new() {RequestAndResponseHistory = requestAndResponseHistory}
                                };
                            }

                            return new List<Entity> {new() {RequestAndResponseHistory = requestAndResponseHistory}};
                        }
                    }

                    return null;
                });
            }
            catch (Exception e)
            {
                await Task.Run(() =>
                {
                    _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                        e);
                });
            }

            return null;
        }

        #endregion

        #region public async Task<Entity> ApiFindByRegonsAsync...

        /// <summary>
        ///     https://wl-test.mf.gov.pl/api/search/regons/{regons}?date={date format yyyy-MM-dd}
        ///     https://wl-api.mf.gov.pl/api/search/regons/{regons}?date={date format yyyy-MM-dd}
        ///     Znajdź podmioty wedłóg listy numerów REGON
        ///     Find entities according to the list of REGON numbers
        /// </summary>
        /// <param name="regons">
        ///     Lista maksymalnie 30 numerów REGON rozdzielonych przecinkami
        ///     A list of up to 30 REGON, separated by commas
        ///     Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string
        ///     [^\d{9}$|^\d{14}$]
        ///     REGON identification number assigned by the National Register of Entities of National Economy as string
        ///     [^\d{9}$|^\d{14}$]
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        public async Task<List<Entity>> ApiFindByRegonsAsync(string regons, DateTime? dateOfChecking = null)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    dateOfChecking ??= DateTime.Now;
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != regons && !string.IsNullOrWhiteSpace(regons))
                    {
                        List<Entity> findByRegonsAndModificationDateList =
                            await FindByRegonsAndModificationDateAsync(regons, (DateTime)dateOfChecking);
                        if (null != findByRegonsAndModificationDateList &&
                            findByRegonsAndModificationDateList.Count > 0)
                        {
                            return findByRegonsAndModificationDateList;
                        }

                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/search/regons/{regons}")
                            .AddUrlSegment("regons", regons).AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityListResponse> response =
                            await client.ExecuteAsync<EntityListResponse>(request);
                        if (response.StatusCode == HttpStatusCode.OK ||
                            response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            var entityList = (List<Entity>)response?.Data?.Result?.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Regon &&
                                        !string.IsNullOrWhiteSpace(entity.Regon))
                                    {
                                        Entity entityFindByRegonAndModificationDate =
                                            await FindByRegonAndModificationDateAsync(entity.Regon,
                                                (DateTime)dateOfChecking);
                                        if (null == entityFindByRegonAndModificationDate)
                                        {
                                            await AddOrModifyEntityAsync(requestAndResponseHistory, entity,
                                                (DateTime)dateOfChecking);
                                        }
                                    }
                                }

                                return await FindByRegonsAsync(regons, (DateTime)dateOfChecking) ?? new List<Entity>
                                {
                                    new() {RequestAndResponseHistory = requestAndResponseHistory}
                                };
                            }

                            return new List<Entity> {new() {RequestAndResponseHistory = requestAndResponseHistory}};
                        }
                    }

                    return null;
                });
            }
            catch (Exception e)
            {
                await Task.Run(() =>
                {
                    _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                        e);
                });
            }

            return null;
        }

        #endregion

        #region public async Task<Entity> ApiFindByBankAccountsAsync...

        /// <summary>
        ///     GET https://wl-test.mf.gov.pl/api/search/bank-accounts/{bankAccounts}?date={date format yyyy-MM-dd}
        ///     GET https://wl-api.mf.gov.pl/api/search/bank-accounts/{bankAccounts}?date={date format yyyy-MM-dd}
        ///     Znajdź podmioty wedłóg listy numerów rachunków NRB
        ///     Find entities by the list of NRB account numbers
        /// </summary>
        /// <param name="bankAccounts">
        ///     Lista maksymalnie 30 numerów rachunkow bankowych rozdzielonych przecinkami, rachunek bankowy (26 znaków) w formacie
        ///     NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     A list of up to 30 bank account numbers separated by commas, a bank account (26 characters) in the NRB (Bank
        ///     Account Number) format kkAAAAAAAABBBBBBBBBBBBBBBB
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Lista podmiotów jako List obiektów Entity lub null
        ///     List of entities as List of Entity objects or null
        /// </returns>
        public async Task<List<Entity>> ApiFindByBankAccountsAsync(string bankAccounts, DateTime? dateOfChecking = null)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != bankAccounts && !string.IsNullOrWhiteSpace(bankAccounts))
                    {
                        dateOfChecking ??= DateTime.Now;
                        List<Entity> findByBankAccountsAndModificationDateList =
                            await FindByBankAccountsAndModificationDateAsync(bankAccounts, (DateTime)dateOfChecking);
                        if (null != findByBankAccountsAndModificationDateList &&
                            findByBankAccountsAndModificationDateList.Count > 0)
                        {
                            return findByBankAccountsAndModificationDateList;
                        }

                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/search/bank-accounts/{bankAccounts}")
                            .AddUrlSegment("bankAccounts", bankAccounts).AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityListResponse> response =
                            await client.ExecuteAsync<EntityListResponse>(request);
                        if (response.StatusCode == HttpStatusCode.OK ||
                            response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            var entityList = (List<Entity>)response?.Data?.Result?.Subjects;
                            if (null != entityList && entityList.Count > 0)
                            {
                                foreach (Entity entity in entityList)
                                {
                                    if (null != entity && null != entity.Nip && !string.IsNullOrWhiteSpace(entity.Nip))
                                    {
                                        Entity entityFindByNipAndModificationDate =
                                            await FindByNipAndModificationDateAsync(entity.Nip,
                                                (DateTime)dateOfChecking);
                                        if (null == entityFindByNipAndModificationDate)
                                        {
                                            await AddOrModifyEntityAsync(requestAndResponseHistory, entity,
                                                (DateTime)dateOfChecking);
                                        }
                                    }
                                    else if (null != entity && null != entity.Regon &&
                                             !string.IsNullOrWhiteSpace(entity.Regon))
                                    {
                                        Entity entityFindByRegonAndModificationDate =
                                            await FindByRegonAndModificationDateAsync(entity.Regon,
                                                (DateTime)dateOfChecking);
                                        if (null == entityFindByRegonAndModificationDate)
                                        {
                                            await AddOrModifyEntityAsync(requestAndResponseHistory, entity,
                                                (DateTime)dateOfChecking);
                                        }
                                    }
                                }

                                return await FindByBankAccountsAsync(bankAccounts, (DateTime)dateOfChecking) ??
                                       new List<Entity> {new() {RequestAndResponseHistory = requestAndResponseHistory}};
                            }

                            return new List<Entity> {new() {RequestAndResponseHistory = requestAndResponseHistory}};
                        }
                    }

                    return null;
                });
            }
            catch (Exception e)
            {
                await Task.Run(() =>
                {
                    _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                        e);
                });
            }

            return null;
        }

        #endregion

        #region public async Task<EntityCheck> ApiCheckBankAccountByNipAsync...

        /// <summary>
        ///     Sprawdź, czy dany rachunek jest przypisany do podmiotu według numeru NIP i numeru rachunku bankowego NRB
        ///     Check if a given account is assigned to the entity according to the NIP number and NRB bank account number
        ///     https://wl-test.mf.gov.pl/api/check/nip/{nip}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        ///     https://wl-api.mf.gov.pl/api/check/nip/{nip}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="nip">
        ///     Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        ///     NIP tax identification number as string [^\d{10}$]
        /// </param>
        /// <param name="bankAccount">
        ///     Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Odpowiedź, czy dany rachunek jest przypisany do podmiotu jako EntityCheck
        ///     Reply whether the account is assigned to the subject as EntityCheck
        /// </returns>
        public async Task<EntityCheck> ApiCheckBankAccountByNipAsync(string nip, string bankAccount,
            DateTime? dateOfChecking = null)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != nip && !string.IsNullOrWhiteSpace(nip) && null != bankAccount &&
                        !string.IsNullOrWhiteSpace(bankAccount))
                    {
                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request = (RestRequest)new RestRequest(@"/api/check/nip/{nip}/bank-account/{bankAccount}")
                            .AddUrlSegment("nip", nip).AddUrlSegment("bankAccount", bankAccount).AddParameter("date",
                                dateOfChecking.HasValue
                                    ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityCheckResponse> response =
                            await client.ExecuteAsync<EntityCheckResponse>(request);
                        if (response.StatusCode == HttpStatusCode.OK ||
                            response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            if (null != response?.Data?.Result)
                            {
                                EntityCheck entityCheck = response.Data.Result;
                                entityCheck.RequestAndResponseHistory = requestAndResponseHistory;
                                entityCheck.RequestAndResponseHistoryId = requestAndResponseHistory.Id;
                                entityCheck.Nip = nip;
                                entityCheck.AccountNumber = bankAccount;
                                IFormatProvider culture = new CultureInfo("pl-PL", true);
                                entityCheck.RequestDateTimeAsDate = DateTime.ParseExact(entityCheck.RequestDateTime,
                                    "dd-MM-yyyy HH:mm:ss", culture);
                                entityCheck.DateOfModification = DateTime.Now;
                                try
                                {
                                    _context.Entry(entityCheck).State = EntityState.Added;
                                    await _context.SaveChangesAsync();
                                }
                                catch (Exception e)
                                {
                                    await Task.Run(() =>
                                    {
                                        _log4Net.Error(
                                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                            e);
                                    });
                                }

                                return entityCheck ?? new EntityCheck
                                {
                                    RequestAndResponseHistory = requestAndResponseHistory
                                };
                            }

                            return new EntityCheck {RequestAndResponseHistory = requestAndResponseHistory};
                        }
                    }

                    return null;
                });
            }
            catch (Exception e)
            {
                await Task.Run(() =>
                {
                    _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                        e);
                });
            }

            return null;
        }

        #endregion

        #region public async Task<EntityCheck> ApiCheckBankAccountByRegonAsync...

        /// <summary>
        ///     Sprawdź, czy dany rachunek jest przypisany do podmiotu według numeru rachunku bankowego NRB i numeru REGON
        ///     Check if a given account is assigned to the entity according to the NRB bank account number and REGON number
        ///     https://wl-test.mf.gov.pl/api/check/regon/{regon}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        ///     https://wl-api.mf.gov.pl/api/check/regon/{regon}/bank-account/{bankAccount}?date={date format yyyy-MM-dd}
        /// </summary>
        /// <param name="regon">
        ///     Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string
        ///     [^\d{9}$|^\d{14}$]
        ///     REGON identification number assigned by the National Register of Entities of National Economy as string
        ///     [^\d{9}$|^\d{14}$]
        /// </param>
        /// <param name="bankAccount">
        ///     Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        ///     Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </param>
        /// <param name="dateOfChecking">
        ///     Określ datę sprawdzenia danych w dniu jako DateTime lub brak (null - domyśnie data bieżąca)
        ///     Specify the date of checking the data on the date as DateTime or none (null - current date by default)
        /// </param>
        /// <returns>
        ///     Odpowiedź, czy dany rachunek jest przypisany do podmiotu jako EntityCheck
        ///     Reply whether the account is assigned to the subject as EntityCheck
        /// </returns>
        public async Task<EntityCheck> ApiCheckBankAccountByRegonAsync(string regon, string bankAccount,
            DateTime? dateOfChecking = null)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (null != _appSettings.RestClientUrl && !string.IsNullOrWhiteSpace(_appSettings.RestClientUrl) &&
                        null != regon && !string.IsNullOrWhiteSpace(regon) && null != bankAccount &&
                        !string.IsNullOrWhiteSpace(bankAccount))
                    {
                        var client = new RestClient(_appSettings.RestClientUrl);
                        var request =
                            (RestRequest)new RestRequest(@"/api/check/regon/{regon}/bank-account/{bankAccount}")
                                .AddUrlSegment("regon", regon).AddUrlSegment("bankAccount", bankAccount)
                                .AddParameter("date",
                                    dateOfChecking.HasValue
                                        ? dateOfChecking.Value.ToString("yyyy-MM-dd")
                                        : DateTime.Now.ToString("yyyy-MM-dd"));
                        IRestResponse<EntityCheckResponse> response =
                            await client.ExecuteAsync<EntityCheckResponse>(request);
                        if (response.StatusCode == HttpStatusCode.OK ||
                            response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            RequestAndResponseHistory requestAndResponseHistory =
                                await FindOrAddRequestAndResponseHistoryAsync(
                                    new RequestAndResponseHistory().SetRequestAndResponseHistory(client, request,
                                        response));
                            if (null != response?.Data?.Result)
                            {
                                EntityCheck entityCheck = response.Data.Result;
                                entityCheck.RequestAndResponseHistory = requestAndResponseHistory;
                                entityCheck.RequestAndResponseHistoryId = requestAndResponseHistory.Id;
                                entityCheck.Regon = regon;
                                entityCheck.AccountNumber = bankAccount;
                                IFormatProvider culture = new CultureInfo("pl-PL", true);
                                entityCheck.RequestDateTimeAsDate = DateTime.ParseExact(entityCheck.RequestDateTime,
                                    "dd-MM-yyyy HH:mm:ss", culture);
                                entityCheck.DateOfModification = DateTime.Now;
                                try
                                {
                                    _context.Entry(entityCheck).State = EntityState.Added;
                                    await _context.SaveChangesAsync();
                                }
                                catch (Exception e)
                                {
                                    await Task.Run(() =>
                                    {
                                        _log4Net.Error(
                                            $"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                                            e);
                                    });
                                }

                                return entityCheck ?? new EntityCheck
                                {
                                    RequestAndResponseHistory = requestAndResponseHistory
                                };
                            }

                            return new EntityCheck {RequestAndResponseHistory = requestAndResponseHistory};
                        }
                    }

                    return null;
                });
            }
            catch (Exception e)
            {
                await Task.Run(() =>
                {
                    _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n",
                        e);
                });
            }

            return null;
        }

        #endregion
    }

    #endregion
}
