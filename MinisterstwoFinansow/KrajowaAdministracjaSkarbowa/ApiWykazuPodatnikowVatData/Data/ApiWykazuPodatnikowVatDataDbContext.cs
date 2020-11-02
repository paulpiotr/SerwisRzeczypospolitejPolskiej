using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetAppCommon;
using System;
//using System.Data.Entity;
using System.Reflection;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region public partial class ApiWykazuPodatnikowVatDataDbContext : DbContext
    /// <summary>
    /// Klasa kontekstu bazy danych api wykazu podatników VAT
    /// Database context class api list of VAT taxpayers
    /// </summary>
    //[DbConfigurationType(typeof(ApiWykazuPodatnikowVatDataDbConfiguration))
    public partial class ApiWykazuPodatnikowVatDataDbContext : DbContext
    {
        #region Log4 Net Logger
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private static readonly log4net.ILog log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region private static readonly AppSettings appSettings...
        /// <summary>
        /// Instancja do klasy modelu ustawień jako AppSettings
        /// Instance to the settings model class as AppSettings
        /// </summary>
        private static readonly AppSettings appSettings = AppSettings.GetInstance();
        #endregion

        #region private static readonly MemoryCacheEntryOptions memoryCacheEntryOptions
        /// <summary>
        /// Opcje wpisu pamięci podręcznej
        /// Memory Cache Entry Options
        /// </summary>
        private static readonly MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(appSettings.CacheLifeTime > 0 ? appSettings.CacheLifeTime : 86400));
        #endregion

        #region public ApiWykazuPodatnikowVatDataDbContext()
        /// <summary>
        /// Konstruktor Klasy kontekstu bazy danych
        /// Constructor Database Context Classes
        /// </summary>
        public ApiWykazuPodatnikowVatDataDbContext()
        {
            //CheckForUpdateAndMigrate();
        }
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
            //CheckForUpdateAndMigrate();
        }
        #endregion

        # region public async System.Threading.Tasks.Task CheckForUpdateAndMigrateAsync()
        /// <summary>
        /// Sprawdź warunek daty ostatniej migracji i przeprowadź migrację jeśli warunek jest spełniony
        /// Check the condition of the last migration date and perform the migration if the condition is met
        /// </summary>
        /// <returns>
        /// Metoda asynchroniczna
        /// Asynchronous method
        /// </returns>
        public async System.Threading.Tasks.Task CheckForUpdateAndMigrateAsync()
        {
            try
            {
                int result = (DateTime.Now - appSettings.LastMigrateDateTime).Days;
                log4net.Debug($"Check for update and migrate, compare { DateTime.Now } and { appSettings.LastMigrateDateTime } is { result } CheckForUpdateEveryDays is { appSettings.CheckForUpdateEveryDays }");
                if (/*CheckForUpdateEveryDays > 0 && */result >= appSettings.CheckForUpdateEveryDays)
                {
                    try
                    {
                        try
                        {
                            DatabaseMssqlMdf.GetInstance(Database.GetDbConnection().ConnectionString).Create();
                        }
                        catch (Exception e)
                        {
                            log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                        }
                        try
                        {
                            await Database.MigrateAsync();
                        }
                        catch (Exception e)
                        {
                            log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                        }
                    }
                    catch (Exception e)
                    {
                        log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
                    }
                    finally
                    {
                        appSettings.LastMigrateDateTime = DateTime.Now;
                        await appSettings.SaveAsync();
                    }
                }
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
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
                    optionsBuilder.UseSqlServer(appSettings.GetConnectionString());
                }
            }
            catch (Exception e)
            {
                log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
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
            return memoryCacheEntryOptions;
        }
        #endregion
    }
    #endregion
}
