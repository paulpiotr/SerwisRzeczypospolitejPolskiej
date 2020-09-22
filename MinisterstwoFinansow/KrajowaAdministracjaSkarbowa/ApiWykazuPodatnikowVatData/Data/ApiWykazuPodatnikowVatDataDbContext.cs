using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using NetAppCommon;
using System;
using System.Reflection;

//#nullable disable

namespace ApiWykazuPodatnikowVatData.Data
{
    /// <summary>
    /// Klasa kontekstu bazy danych api wykazu podatników VAT
    /// Database context class api list of VAT taxpayers
    /// </summary>
    public partial class ApiWykazuPodatnikowVatDataDbContext : DbContext
    {
        #region Log4 Net Logger
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private static readonly log4net.ILog _log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
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

        #region public ApiWykazuPodatnikowVatDataDbContext()
        /// <summary>
        /// Konstruktor Klasy kontekstu bazy danych
        /// Constructor Database Context Classes
        /// </summary>
        public ApiWykazuPodatnikowVatDataDbContext()
        {
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
        }
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
                if (!optionsBuilder.IsConfigured)
                {
                    //_log4net.Info(DataContext.GetConnectionString("ApiWykazuPodatnikowVatDataDbContext"));
                    optionsBuilder.UseSqlServer(DataContext.GetConnectionString("ApiWykazuPodatnikowVatDataDbContext"));
                }
            }
            catch (Exception e)
            {
                _log4net.Error(string.Format("{0}, {1}.", e.Message, e.StackTrace), e);
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
            //modelBuilder.ApplyConfiguration(new DocumentHeaderConfiguration());
            //modelBuilder.ApplyConfiguration(new DocumentPositionConfiguration());
            //modelBuilder.ApplyConfiguration(new DocumentAttachmentConfiguration());
            //modelBuilder.ApplyConfiguration(new DocumentOfContractorDepositsAndWithdrawalsConfiguration());
            //modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
        #endregion

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
