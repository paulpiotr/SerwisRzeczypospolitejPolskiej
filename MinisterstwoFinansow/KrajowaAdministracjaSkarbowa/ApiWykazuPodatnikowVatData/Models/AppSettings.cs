#region using

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using log4net;
using NetAppCommon.AppSettings.Models.Base;
using NetAppCommon.Helpers.Cache;
using NetAppCommon.Validation;
using Newtonsoft.Json;

#endregion

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class AppSettings : AppSettingsBaseModel

    /// <summary>
    ///     Klasa modelu ustawień aplikacji ApiWykazuPodatnikowVatData
    ///     The settings model class of the ApiWykazuPodatnikowVatData
    /// </summary>
    [NotMapped]
    public sealed class AppSettings : AppSettingsWithDatabase
    {
        ///Important !!!

        #region AppSettingsModel()

        public AppSettings()
        {
            try
            {
                var memoryCacheProvider = MemoryCacheProvider.GetInstance();
                var filePathKey = $"{MethodBase.GetCurrentMethod()?.DeclaringType?.FullName}.FilePath";
                object filePath = memoryCacheProvider.Get(filePathKey);
                if (null == filePath)
                {
                    AppSettingsRepository?.MergeAndCopyToUserDirectory(this);
                    memoryCacheProvider.Put(filePathKey, FilePath, TimeSpan.FromDays(1));
                }

                if (null != UserProfileDirectory && null != FileName)
                {
                    FilePath = (string)(filePath ?? Path.Combine(UserProfileDirectory, FileName));
                }

                var useGlobalDatabaseConnectionSettingsKey =
                    $"{MethodBase.GetCurrentMethod()?.DeclaringType?.FullName}.UseGlobalDatabaseConnectionSettings";
                object useGlobalDatabaseConnectionSettings =
                    memoryCacheProvider.Get(useGlobalDatabaseConnectionSettingsKey);
                if (null == useGlobalDatabaseConnectionSettings)
                {
                    memoryCacheProvider.Put(useGlobalDatabaseConnectionSettingsKey, UseGlobalDatabaseConnectionSettings,
                        TimeSpan.FromDays(1));
                    if (UseGlobalDatabaseConnectionSettings)
                    {
                        var appSettingsModel = new NetAppCommon.AppSettings.Models.AppSettings();
                        ConnectionString = appSettingsModel.ConnectionString;
                        AppSettingsRepository?.MergeAndSave(this);
                    }
                }
            }
            catch (Exception e)
            {
                _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
            }
        }

        #endregion

        ///Important !!!

        #region public static AppSettingsBaseModel GetInstance()

        /// <summary>
        ///     Pobierz statyczną referencję do instancji AppSettingsBaseModel
        ///     Get a static reference to the AppSettingsBaseModel instance
        /// </summary>
        /// <returns>
        ///     Statyczna referencja do instancji AppSettingsBaseModel
        ///     A static reference to the AppSettingsBaseModel instance
        /// </returns>
        public static AppSettings GetInstance() => new();

        #endregion

        #region private readonly log4net.ILog log4net

        /// <summary>
        ///     Instancja do klasy Log4netLogger
        ///     Instance to Log4netLogger class
        /// </summary>
        private readonly ILog _log4Net =
            Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod()?.DeclaringType);

        #endregion

        #region protected new string _fileName = FILENAME;

#if DEBUG
        private const string Filename = "apiwykazupodatnikowvatdata.appsettings.json";
#else
        private const string Filename = "apiwykazupodatnikowvatdata.appsettings.json";
#endif

        private string _fileName = Filename;

        public override string FileName
        {
            get => _fileName;
            protected set
            {
                if (value != _fileName)
                {
                    _fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }

        #endregion

        #region protected new string _connectionStringName = CONNECTIONSTRINGNAME;

#if DEBUG
        private const string Connectionstringname = "ApiWykazuPodatnikowVatDataDbContext";
#else
        private const string Connectionstringname = "ApiWykazuPodatnikowVatDataDbContext";
#endif

        private string _connectionStringName = Connectionstringname;

        public override string ConnectionStringName
        {
            get => _connectionStringName;
            set
            {
                if (value != _connectionStringName)
                {
                    _connectionStringName = value;
                }
            }
        }

        #endregion

        #region private string _restClientUrl; public string RestClientUrl

        private string _restClientUrl;

        /// <summary>
        ///     Ustawienie adresu URL łącza do serwisu API
        ///     Paramentr url https://wl-test.mf.gov.pl lub https://wl-api.mf.gov.pl
        ///     Set a link to the API service
        ///     Paramentr url https://wl-test.mf.gov.pl or https://wl-api.mf.gov.pl
        /// </summary>
        [JsonProperty(nameof(RestClientUrl))]
        [Display(Name = "Ustawienie adresu URL łącza do serwisu API",
            Prompt = "Wpisz ustawienie adresu URL łącza do serwisu API",
            Description = "Ustawienie adresu URL łącza do serwisu API")]
        [Required]
        [InListOfString(@"https://wl-api.mf.gov.pl, https://wl-test.mf.gov.pl")]
        public string RestClientUrl
        {
            get
            {
                if (null == _restClientUrl)
                {
                    _restClientUrl = AppSettingsRepository?.GetValue<string>(this, nameof(RestClientUrl));
                }

                return _restClientUrl;
            }
            set
            {
                if (value != _restClientUrl)
                {
                    _restClientUrl = value;
                    OnPropertyChanged(nameof(RestClientUrl));
                }
            }
        }

        #endregion
    }

    #endregion
}
