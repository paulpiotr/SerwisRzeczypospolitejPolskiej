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
        #region private readonly log4net.ILog log4net
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private readonly log4net.ILog log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
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

        #region public static AppSettings GetInstance()
        /// <summary>
        /// Pobierz instancję klasy AppSettings
        /// Get an instance of the AppSettings class
        /// </summary>
        /// <returns>
        /// Instanacja klasy AppSettings
        /// Instance of the AppSettings class
        /// </returns>
        public static AppSettings GetInstance()
        {
            return new AppSettings();
        }
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
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            try
            {
                CacheLifeTime = Configuration.GetValue<int>(FileName, "CacheLifeTime");
                if (CacheLifeTime < 0)
                {
                    CacheLifeTime = 1 * 1000 * 60 * 60 * 24;
                }
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            try
            {
                ConnectionString = Configuration.GetValue<string>(FileName, string.Format("{0}:{1}", "ConnectionStrings", ConnectionStringName)) ?? @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=%Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)%\MSSQLLocalDB\MSSQLLocalDB.mdf; Database=%AttachDbFilename%; MultipleActiveResultSets=true; Integrated Security=True; Trusted_Connection=Yes";
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            try
            {
                CheckForUpdateEveryDays = Configuration.GetValue<int>(FileName, "CheckForUpdateEveryDays");
                if (CheckForUpdateEveryDays < 0)
                {
                    CheckForUpdateEveryDays = 0;
                }
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            try
            {
                LastMigrateDateTime = Configuration.GetValue<DateTime>(FileName, "LastMigrateDateTime");
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
        }
        #endregion

        #region RestClientUrl { get; set; }
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

        #region int CacheLifeTime { get; set; }
        /// <summary>
        /// Okres istnienia pamięci podręcznej dla zapytań w serwisie API (w sekundach)
        /// Cache lifetime for API service queries (in seconds)
        /// </summary>
        [JsonProperty(nameof(CacheLifeTime))]
        [Display(Name = "Okres istnienia pamięci podręcznej dla zapytań w serwisie API (w sekundach)", Prompt = "Wpisz okres istnienia pamięci podręcznej dla zapytań w serwisie API (w sekundach)", Description = "Okres istnienia pamięci podręcznej dla zapytań w serwisie API (w sekundach)")]
        [Required]
        [Range(0, 2147483647)]
        public int CacheLifeTime { get; set; }
        #endregion

        #region public int CheckForUpdateEveryDays { get; set; }
        /// <summary>
        /// Czas sprawdzania aktualizacji migracji bazy danych w dniach
        /// Checking database migration updates in days
        /// </summary>
        [JsonProperty(nameof(CheckForUpdateEveryDays))]
        [Display(Name = "Czas sprawdzania aktualizacji migracji bazy danych w dniach", Prompt = "Wpisz czas sprawdzania aktualizacji migracji bazy danych w dniach", Description = "Czas sprawdzania aktualizacji migracji bazy danych w dniach")]
        [Required]
        [Range(0, 2147483647)]
        public int CheckForUpdateEveryDays { get; set; }
        #endregion

        #region public bool CheckForUpdateAndMigrate { get; set; }
        /// <summary>
        /// Sprawdź czy baza danych wymaga aktualizacji i przeprowadź instalację oraz migrację bazy danych
        /// Check if the database needs to be updated and perform the installation and database migration
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Sprawdź czy baza danych wymaga aktualizacji i przeprowadź instalację oraz migrację bazy danych", Prompt = "Zaznacz, jeśli chcesz sprawdzić czy baza danych wymaga aktualizacji i przeprowadź instalację oraz migrację bazy danych", Description = "Sprawdź czy baza danych wymaga aktualizacji i przeprowadź instalację oraz migrację bazy danych")]
        public bool CheckForUpdateAndMigrate { get; set; }
        #endregion

        #region public DateTime LastMigrateDateTime { get; private set; }
        /// <summary>
        /// Data ostatniej próby aktualizacji migracji bazy danych
        /// Date of the last database migration update attempt
        /// </summary>
        [JsonProperty(nameof(LastMigrateDateTime))]
        [Display(Name = "Data ostatniej próby aktualizacji migracji bazy danych", Prompt = "Wpisz lub wybierz datę ostatniej próby aktualizacji migracji bazy danych", Description = "Data ostatniej próby aktualizacji migracji bazy danych")]
        public DateTime LastMigrateDateTime { get; set; }
        #endregion

        #region private string _ConnectionString { get; set; }
        /// <summary>
        /// Dostęp prywatny - ciąg połączenia do bazy danych Mssql jako string
        /// Private Access - Mssql database connection string as string
        /// </summary>
        private string _ConnectionString { get; set; }
        #endregion

        #region public bool CheskForConnection { get; set; }
        /// <summary>
        /// Sprawdź możliwość podłączenia do bazy danych z wpisanego parametru Ciąg połączenia do bazy danych Mssql
        /// Check the possibility of connecting to the database by entering the Mssql database connection string parameter
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Sprawdź możliwość podłączenia do bazy danych z wpisanego parametru Ciąg połączenia do bazy danych Mssql", Prompt = "Zaznacz, jeśli chcesz sprawdzić możliwość podłączenia do bazy danych z wpisanego parametru Ciąg połączenia do bazy danych Mssql", Description = "Sprawdź możliwość podłączenia do bazy danych z wpisanego parametru Ciąg połączenia do bazy danych Mssql")]
        public bool CheskForConnection { get; set; }
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

        #region public string GetConnectionString()
        /// <summary>
        /// Pobierz parametry połączenia
        /// Get the connection string
        /// </summary>
        /// <returns>
        /// Parametry połączenia jako string lub null
        /// Connection string as string or null
        /// </returns>
        public string GetConnectionString()
        {
            try
            {
                return DatabaseMssql.ParseConnectionString(ConnectionString);
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return null;
        }
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
                log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
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
                await Task.Run(() => log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e));
            }
        }
        #endregion
    }
    #endregion
}