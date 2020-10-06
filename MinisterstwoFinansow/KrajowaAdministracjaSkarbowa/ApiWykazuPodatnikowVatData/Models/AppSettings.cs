using NetAppCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class AppSettings
    /// <summary>
    /// Klasa modelu ustawień aplikacji ApiWykazuPodatnikowVatData
    /// The settings model class of the ApiWykazuPodatnikowVatData
    /// </summary>
    [NotMapped]
    public partial class AppSettings
    {
        #region private static readonly log4net.ILog _log4net
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private static readonly log4net.ILog _log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region private static readonly string FileName
        /// <summary>
        /// Nazwa pliku z ustawieniami aplikacji ustawiona w zależności od wersji środowiska
        /// Application settings file name set depending on the version of the environment
        /// </summary>
#if DEBUG
        private static readonly string FileName = "api.wykazu.podatnikow.vat.data.appsettings.debug.json";
#else
        private static readonly string FileName = "api.wykazu.podatnikow.vat.data.appsettings.release.json";
#endif
        #endregion

        #region private static readonly string FilePath...
        /// <summary>
        /// Absolutna ścieżka do pliku konfiguracji
        /// The absolute path to the configuration file
        /// </summary>
        private static readonly string FilePath = Path.Combine(Configuration.GetBaseDirectory(), FileName);
        #endregion

        #region private static readonly string ConnectionStringName
        /// <summary>
        /// Nazwa połączenia bazy danych Mssql dla bieżącej aplikacji
        /// The name of the Mssql database connection for the current application
        /// </summary>
        private static readonly string ConnectionStringName = "ApiWykazuPodatnikowVatDataDbContext";
        #endregion

        #region public AppSettings()
        /// <summary>
        /// Konstruktor - przypisanie zmiennych z pliku konfiguracyjnego
        /// Constructor - assigning variables from the configuration file
        /// </summary>
        public AppSettings()
        {
            try
            {
                RestClientUrl = Configuration.GetValue<string>(FileName, "RestClientUrl") ?? "https://wl-api.mf.gov.pl";
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            try
            {
                CacheLifeTimeForApiServiceQueries = Configuration.GetValue<int>(FileName, "CacheLifeTimeForApiServiceQueries");
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
            try
            {
                ConnectionString = Configuration.GetValue<string>(FileName, string.Format("{0}:{1}", "ConnectionStrings", ConnectionStringName));
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
        }
        #endregion

        #region public string RestClientUrl
        /// <summary>
        /// Ustawienie adresu URL łącza do serwisu API
        /// Paramentr url https://wl-test.mf.gov.pl lub https://wl-api.mf.gov.pl
        /// Set a link to the API service
        /// Paramentr url https://wl-test.mf.gov.pl or https://wl-api.mf.gov.pl
        /// </summary>
        [JsonProperty(nameof(RestClientUrl))]
        [Display(Name = "Ustawienie adresu URL łącza do serwisu API", Prompt = "Wpisz ustawienie adresu URL łącza do serwisu API", Description = "Ustawienie adresu URL łącza do serwisu API")]
        [Required]
        [NetAppCommon.Validation.InListOfString(@"https://wl-api.mf.gov.pl, https://wl-test.mf.gov.pl")]
        public string RestClientUrl { get; set; }
        #endregion

        #region public int CacheLifeTimeForApiServiceQueries
        /// <summary>
        /// Okres istnienia pamięci podręcznej dla zapytań w serwisie API
        /// Cache lifetime for API service queries
        /// </summary>
        [JsonProperty(nameof(CacheLifeTimeForApiServiceQueries))]
        [Display(Name = "Okres istnienia pamięci podręcznej dla zapytań w serwisie API", Prompt = "Wpisz okres istnienia pamięci podręcznej dla zapytań w serwisie API (w sekundach)", Description = "Okres istnienia pamięci podręcznej dla zapytań w serwisie API")]
        [Required]
        [Range(0, 2147483647)]
        public int CacheLifeTimeForApiServiceQueries { get; set; }
        #endregion

        #region private string _ConnectionString { get; set; }
        /// <summary>
        /// Dostęp prywatny - ciąg połączenia do bazy danych Mssql jako string
        /// Private Access - Mssql database connection string as string
        /// </summary>
        private string _ConnectionString { get; set; }
        #endregion

        #region public string ConnectionString
        /// <summary>
        /// Dostęp publiczny - ciąg połączenia do bazy danych Mssql jako string
        /// Public Access - Mssql database connection string as a string
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Ciąg połączenia do bazy danych Mssql", Prompt = "Wpisz ciąg połączenia do bazy danych Mssql", Description = "Ciąg połączenia do bazy danych Mssql")]
        [Required]
        [NetAppCommon.Validation.MssqlCanConnect]
        public string ConnectionString
        {
            get => _ConnectionString;
            set
            {
                if (null != value && !string.IsNullOrWhiteSpace(value))
                {
                    _ConnectionString = value;
                    ConnectionStrings = new Dictionary<string, string>
                    {
                        { ConnectionStringName, value }
                    };
                }
            }
        }
        #endregion

        #region public Dictionary<string, string> ConnectionStrings { get; private set; }
        /// <summary>
        /// Słownik zawierający definicję połączenia z nazwąklucza konfiguracji i wartością jako Dictionary
        /// A dictionary containing a connection definition with a configuration key name and value as Dictionary
        /// </summary>
        [JsonProperty(nameof(ConnectionStrings))]
        public Dictionary<string, string> ConnectionStrings { get; private set; }
        #endregion

        #region public void Save()
        /// <summary>
        /// Zapisz kofigurację do pliku
        /// Save configuration to file
        /// </summary>
        public void Save()
        {
            try
            {
                Configuration.SaveConfigurationToFile(this, FilePath);
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
            }
        }
        #endregion

        #region public async Task SaveAsync()
        /// <summary>
        /// Zapisz kofigurację do pliku asynchronicznie
        /// Save configuration to file asynchronously
        /// </summary>
        public async Task SaveAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    await Configuration.SaveConfigurationToFileAsync(this, FilePath);
                });
            }
            catch (Exception e)
            {
                await Task.Run(() => _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e));
            }
        }
        #endregion
    }
    #endregion
}