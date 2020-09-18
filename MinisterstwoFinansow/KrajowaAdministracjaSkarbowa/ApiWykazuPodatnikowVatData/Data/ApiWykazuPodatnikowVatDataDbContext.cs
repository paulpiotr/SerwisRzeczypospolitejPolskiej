//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ApiWykazuPodatnikowVatData.Data
//{
//    class ApiWykazuPodatnikowVatDataDbContext
//    {
//    }
//}


//using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using NetAppCommon;
using System;
//using System.Data.Entity;
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

        //#region public virtual DbSet<DocumentHeader> DocumentHeader - model danych nagłówek dokumentu
        ///// <summary>
        ///// Model danych nagłówek dokumentu AS DbSet of DocumentHeade
        ///// Document header data model AS DbSet of DocumentHeader
        ///// </summary>
        //public virtual DbSet<DocumentHeader> DocumentHeader { get; set; }
        //#endregion

        //#region public virtual DbSet<DocumentPosition> DocumentPosition - model danych pozycja dokumentu
        ///// <summary>
        ///// Model danych pozycja dokumentu AS DbSet of DocumentPosition
        ///// Document item data model AS DbSet of DocumentPosition
        ///// </summary>
        //public virtual DbSet<DocumentPosition> DocumentPosition { get; set; }
        //#endregion

        //#region public virtual DbSet<DocumentAttachment> DocumentAttachment- model danych załącznik dokumentu
        ///// <summary>
        ///// Model danych załącznik dokumentu AS DbSet of DocumentAttachment
        ///// Document attachment data model AS DbSet of DocumentAttachment
        ///// </summary>
        //public virtual DbSet<DocumentAttachment> DocumentAttachment { get; set; }
        //#endregion

        //#region public virtual DbSet<Transaction> Transaction - model danych transakcja
        ///// <summary>
        ///// Model danych pozycja dokumentu AS DbSet of Transaction
        ///// Document item data model AS DbSet of Transaction
        ///// </summary>
        //public virtual DbSet<Transaction> Transaction { get; set; }
        //#endregion

        //#region public ApiWykazuPodatnikowVatDataDbContext() ApiWykazuPodatnikowVatDataDbContext() Konstruktor Klasy kontekstu bazy danych api wykazu podatników VAT
        ///// <summary>
        ///// Konstruktor Klasy kontekstu bazy danych
        ///// Constructor Database Context Classes
        ///// </summary>
        //public ApiWykazuPodatnikowVatDataDbContext()
        //{
        //}
        //#endregion

        #region public ApiWykazuPodatnikowVatDataDbContext(DbContextOptions<ApiWykazuPodatnikowVatDataDbContext> options) Konstruktor Klasy kontekstu bazy danych
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

        #region protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) Zdarzenie wyzwalające konfigurację bazy danych
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

        #region protected override void OnModelCreating(ModelBuilder modelBuilder) Zdarzenie wyzwalające tworzenie modelu bazy danych
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
