using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region public partial class ApiWykazuPodatnikowVatDataDbContext : DbContext
    /// <summary>
    /// Klasa kontekstu bazy danych api wykazu podatników VAT
    /// Database context class api list of VAT taxpayers
    /// </summary>
    public partial class ApiWykazuPodatnikowVatDataDbContext : DbContext
    {
        #region private readonly log4net.ILog _log4Net
        /// <summary>
        /// Log4 Net Logger
        /// Log4 Net Logger
        /// </summary>
        private readonly log4net.ILog _log4Net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod()?.DeclaringType);
        #endregion

        #region private readonly AppSettings _appSettings
        /// <summary>
        /// Instancja do klasy modelu ustawień jako AppSettings
        /// Instance to the settings model class as AppSettings
        /// </summary>
        private readonly AppSettings _appSettings = new AppSettings();
        #endregion

        #region private static readonly MemoryCacheEntryOptions memoryCacheEntryOptions
        /// <summary>
        /// Opcje wpisu pamięci podręcznej
        /// Memory Cache Entry Options
        /// </summary>
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
        #endregion

        #region public ApiWykazuPodatnikowVatDataDbContext(DbContextOptions<ApiWykazuPodatnikowVatDataDbContext> options)
        /// <summary>
        /// Konstruktor klasy kontekstu bazy danych api wykazu podatników VAT
        /// Constructor database context classes api list of VAT taxpayers
        /// </summary>
        /// <param name="options">
        /// Opcje połączenia da bazy danych options AS DbContextOptions<ApiWykazuPodatnikowVatDataDbContext>
        /// Connection options will give the options AS DbContextOptions<ApiWykazuPodatnikowVatDataDbContext>
        /// </param>
        public ApiWykazuPodatnikowVatDataDbContext(DbContextOptions<ApiWykazuPodatnikowVatDataDbContext> options)
            : base(options)
        {
            CheckAndMigrate();
        }
        #endregion

        #region public void CheckAndMigrate()
        /// <summary>
        /// Sprawdź ostatnią datę migracji bazy danych lub wymuś wykonanie, jeśli opcja CheckAndMigrate jest zaznaczona i wykonaj migrację bazy danych.
        /// Check the latest database migration date or force execution if CheckAndMigrate is selected and perform database migration.
        /// </summary>
        public void CheckAndMigrate()
        {
            Task.Run(async () =>
            {
                await CheckAndMigrateAsync();
            }).Wait();
        }
        #endregion

        #region public async Task CheckAndMigrateAsync()
        /// <summary>
        /// Sprawdź ostatnią datę migracji bazy danych lub wymuś wykonanie, jeśli opcja CheckAndMigrate jest zaznaczona i wykonaj migrację bazy danych.
        /// Check the latest database migration date or force execution if CheckAndMigrate is selected and perform database migration.
        /// </summary>
        /// <returns>
        /// async Task
        /// async Task
        /// </returns>
        public async Task CheckAndMigrateAsync()
        {
            DateTime? lastMigrateDateTime = null;
            try
            {
                lastMigrateDateTime = await _appSettings.AppSettingsRepository.GetValueAsync<DateTime>(_appSettings, "LastMigrateDateTime");
                var isCheckAndMigrate = await _appSettings.AppSettingsRepository.GetValueAsync<bool>(_appSettings, "CheckAndMigrate");
                var dateTimeDiffDays = (DateTime.Now - (DateTime)lastMigrateDateTime).Days;
                if ((isCheckAndMigrate || dateTimeDiffDays >= 1) && (await Database.GetPendingMigrationsAsync()).Any())
                {
                    try
                    {
#if DEBUG
                        _log4Net.Debug($"Try CheckAndMigrateAsync...");
#endif
                        await NetAppCommon.Helpers.EntityContextHelper.RunMigrationAsync(this);
#if DEBUG
                        _log4Net.Debug($"Ok");
#endif
                    }
                    catch (Exception e)
                    {
                        _log4Net.Warn($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
                    }
                    _appSettings.LastMigrateDateTime = DateTime.Now;
                    await _appSettings.AppSettingsRepository.MergeAndSaveAsync(_appSettings);
                }
            }
            catch (Exception e)
            {
                _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
            }
            finally
            {
                if (null == lastMigrateDateTime || lastMigrateDateTime == DateTime.MinValue)
                {
                    _appSettings.LastMigrateDateTime = DateTime.Now;
                    await _appSettings.AppSettingsRepository.MergeAndSaveAsync(_appSettings);
                }
            }
        }
        #endregion

        #region public virtual DbSet<Entity> Entity { get; set; }
        /// <summary>
        /// Model danych Entity, oryginalnie Entity
        /// </summary>
        public virtual DbSet<Entity> Entity { get; set; }
        #endregion

        #region public virtual DbSet<EntityPesel> EntityPesel { get; set; }
        /// <summary>
        /// Model danych EntityPesel, oryginalnie Pesel
        /// </summary>
        public virtual DbSet<EntityPesel> EntityPesel { get; set; }
        #endregion

        #region public virtual DbSet<EntityPerson> EntityPerson { get; set; }
        /// <summary>
        /// Model danych EntityPerson, oryginalnie EntityPerson
        /// </summary>
        public virtual DbSet<EntityPerson> EntityPerson { get; set; }
        #endregion

        #region public virtual DbSet<EntityAccountNumber> EntityAccountNumber { get; set; }
        /// <summary>
        /// Model danych EntityAccountNumber
        /// </summary>
        public virtual DbSet<EntityAccountNumber> EntityAccountNumber { get; set; }
        #endregion

        #region public virtual DbSet<EntityCheck> EntityCheck { get; set; }
        /// <summary>
        /// Model danych EntityCheck
        /// </summary>
        public virtual DbSet<EntityCheck> EntityCheck { get; set; }
        #endregion

        #region virtual DbSet<RequestAndResponseHistory> RequestAndResponseHistory { get; set; }
        /// <summary>
        /// Model danych RequestAndResponseHistory
        /// </summary>
        public virtual DbSet<RequestAndResponseHistory> RequestAndResponseHistory { get; set; }
        #endregion

        #region protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        /// <summary>
        /// Zdarzenie wyzwalające konfigurację bazy danych
        /// Database configuration triggering event
        /// </summary>
        /// <param name="optionsBuilder">
        /// Fabryka budowania połączenia do bazy danych optionsBuilder jako DbContextOptionsBuilder
        /// Factory building connection to the database optionsBuilder AS DbContextOptionsBuilder
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                //#if DEBUG
                //                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                //                {
                //                    builder.AddFilter(level => level == LogLevel.Debug).AddConsole();
                //                });
                //                optionsBuilder.UseLoggerFactory(loggerFactory);
                //#endif
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(_appSettings.GetConnectionString());
                }
            }
            catch (Exception e)
            {
                _log4Net.Error($"\n{e.GetType()}\n{e.InnerException?.GetType()}\n{e.Message}\n{e.StackTrace}\n", e);
            }
        }
        #endregion

        #region protected override void OnModelCreating(ModelBuilder modelBuilder)
        /// <summary>
        /// Zdarzenie wyzwalające tworzenie modelu bazy danych
        /// The event that triggers the creation of the database model
        /// </summary>
        /// <param name="modelBuilder">
        /// Fabryka budowania modelu bazy danych modelBuilder jako ModelBuilder
        /// ModelBuilder database model building as ModelBuilder
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityAccountNumberConfiguration());
            modelBuilder.ApplyConfiguration(new EntityCheckConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfiguration());
            modelBuilder.ApplyConfiguration(new EntityPersonConfiguration());
            modelBuilder.ApplyConfiguration(new RequestAndResponseHistoryConfiguration());
        }
        #endregion

        #region partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        /// <summary>
        /// partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        /// partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        /// </summary>
        /// <param name="modelBuilder">
        /// ModelBuilder modelBuilder
        /// ModelBuilder modelBuilder
        /// </param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion

        #region public MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
        /// <summary>
        /// Uzyskaj opcje wpisu pamięci podręcznej
        /// Get Memory Cache Entry Options
        /// </summary>
        /// <returns>
        /// Opcje wpisu pamięci podręcznej
        /// Memory Cache Entry Options
        /// </returns>
        public MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
        {
            return _memoryCacheEntryOptions;
        }
        #endregion
    }
    #endregion
}
