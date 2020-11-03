using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class Entity
    /// <summary>
    /// Model danych podmiot gospodarczy jako Entity
    /// Entity data model
    /// </summary>
    [Table("Entity", Schema = "awpv")]
    public partial class Entity
    {
        #region public Entity()
        /// <summary>
        /// Konstruktor Entity()
        /// </summary>
        public Entity()
        {
            Representative = new HashSet<EntityPerson>();
            AuthorizedClerk = new HashSet<EntityPerson>();
            Partner = new HashSet<EntityPerson>();
            EntityAccountNumber = new HashSet<EntityAccountNumber>();
            RequestAndResponseHistory = new RequestAndResponseHistory();
            SetUniqueIdentifierOfTheLoggedInUser();
        }
        #endregion

        #region public Guid Id { get; set; }
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [JsonProperty(nameof(Id))]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public string UniqueIdentifierOfTheLoggedInUser { get; private set; }
        /// <summary>
        /// Jednoznaczny identyfikator zalogowanego użytkownika
        /// Unique identifier of the logged in user
        /// </summary>
        [Column("UniqueIdentifierOfTheLoggedInUser", TypeName = "varchar(512)")]
        [JsonProperty(nameof(UniqueIdentifierOfTheLoggedInUser))]
        [Display(Name = "Identyfikator zalogowanego użytkownika", Prompt = "Wybierz identyfikator zalogowanego użytkownika", Description = "Identyfikator zalogowanego użytkownika")]
        [StringLength(512)]
        [Required]
        public string UniqueIdentifierOfTheLoggedInUser { get; private set; }
        #endregion

        #region public void SetUniqueIdentifierOfTheLoggedInUser()
        /// <summary>
        /// Ustaw jednoznaczny identyfikator zalogowanego użytkownika
        /// Set a unique identifier for the logged in user
        /// </summary>
        public void SetUniqueIdentifierOfTheLoggedInUser()
        {
            try
            {
                UniqueIdentifierOfTheLoggedInUser = NetAppCommon.HttpContextAccessor.AppContext.GetCurrentUserIdentityName();
            }
            catch (Exception)
            {
                UniqueIdentifierOfTheLoggedInUser = null;
            }
        }
        #endregion

        #region public Guid? RequestAndResponseHistoryId { get; set; }
        /// <summary>
        /// Identyfikator odpowiedzi żądania dotyczącego wyszukiwania podmiotu Entity (klucz obcy) jako Guid
        /// Identifier of the response in the subject search (foreign key) as Guid
        /// </summary>
        [JsonProperty(nameof(RequestAndResponseHistoryId))]
        [Display(Name = "Numer RequestAndResponseHistory", Prompt = "Wybierz powiązanie numeru pesel", Description = "Numer pesel")]
        public Guid? RequestAndResponseHistoryId { get; set; }
        #endregion

        #region public virtual RequestAndResponseHistory RequestAndResponseHistory { get; set; }
        /// <summary>
        /// Odpowiedź żądania dotyczącego wyszukiwania podmiotu Entity jako obiekt RequestAndResponseHistory
        /// The response of the Entity lookup request as an RequestAndResponseHistory
        /// </summary>
        [ForeignKey(nameof(RequestAndResponseHistoryId))]
        [InverseProperty(nameof(Models.RequestAndResponseHistory.Entity))]
        public virtual RequestAndResponseHistory RequestAndResponseHistory { get; set; }
        #endregion

        #region public string Name { get; set; }
        /// <summary>
        /// Firma (nazwa) lub imię i nazwisko
        /// </summary>
        [Column("Name", TypeName = "varchar(256)")]
        [JsonProperty(nameof(Name))]
        [Display(Name = "Firma (nazwa) lub imię i nazwisko", Prompt = "Wpisz firmę (nazwę) lub imię i nazwisko", Description = "Firma (nazwa) lub imię i nazwisko")]
        [Required]
        [StringLength(256)]
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; }
        #endregion

        #region public string Nip { get; set; }
        /// <summary>
        /// Numer identyfikacji podatkowej NIP jako string
        /// </summary>
        [Column("Nip", TypeName = "varchar(10)")]
        [JsonProperty(nameof(Nip))]
        [Display(Name = "Numer NIP", Prompt = "Wpisz nip", Description = "Numer NIP")]
        [StringLength(10)]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$")]
        public string Nip { get; set; }
        #endregion

        #region public string StatusVat { get; set; }
        /// <summary>
        /// Status podatnika VAT, - Czynny lub Zwolniony lub Niezarejestrowany
        /// </summary>
        [Column("StatusVat", TypeName = "varchar(32)")]
        [JsonProperty(nameof(StatusVat))]
        [Display(Name = "Status podatnika VAT", Prompt = "Wybierz status podatnika VAT", Description = "Status podatnika VAT, (Czynny lub Zwolniony lub Niezarejestrowany)")]
        [StringLength(32)]
        [MinLength(1)]
        [MaxLength(32)]
        public string StatusVat { get; set; }
        #endregion

        #region public string Regon { get; set; }
        /// <summary>
        /// Numer identyfikacyjny REGON przypisany przez Krajowy Rejestr Urzędowy Podmiotów Gospodarki Narodowej jako string [^\d{9}$|^\d{14}$]
        /// REGON identification number assigned by the National Register of Entities of National Economy as string [^\d{9}$|^\d{14}$]
        /// </summary>
        [Column("Regon", TypeName = "varchar(14)")]
        [JsonProperty(nameof(Regon))]
        [Display(Name = "Regon", Prompt = "Wpisz regon", Description = "Numer regon")]
        [StringLength(14)]
        [MinLength(9)]
        [MaxLength(14)]
        [RegularExpression(@"^\d{9}$|^\d{14}$")]
        public string Regon { get; set; }
        #endregion

        #region public string Pesel { get; set; }
        /// <summary>
        /// Numer pesel
        /// </summary>
        [Column("Pesel", TypeName = "varchar(11)")]
        [JsonProperty(nameof(Pesel))]
        [Display(Name = "Pesel", Prompt = "Wpisz pesel", Description = "Numer pesel")]
        [StringLength(11)]
        [MinLength(11)]
        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$")]
        public string Pesel { get; set; }
        #endregion

        #region public string Krs { get; set; }
        /// <summary>
        /// Numer KRS
        /// </summary>
        [Column("Krs", TypeName = "varchar(10)")]
        [JsonProperty(nameof(Krs))]
        [Display(Name = "Numer KRS", Prompt = "Wpisz Numer KRS", Description = "Numer KRS")]
        [StringLength(10)]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$")]
        public string Krs { get; set; }
        #endregion

        #region public string ResidenceAddress { get; set; }
        /// <summary>
        /// Adres siedziby
        /// </summary>
        [Column("ResidenceAddress", TypeName = "varchar(200)")]
        [JsonProperty(nameof(ResidenceAddress))]
        [Display(Name = "Adres siedziby", Prompt = "Wpisz adres siedziby", Description = "Adres siedziby")]
        [StringLength(200)]
        [MaxLength(200)]
        public string ResidenceAddress { get; set; }
        #endregion

        #region public string WorkingAddress { get; set; }
        /// <summary>
        /// Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania
        /// </summary>
        [Column("WorkingAddress", TypeName = "varchar(200)")]
        [JsonProperty(nameof(WorkingAddress))]
        [Display(Name = "Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania", Prompt = "Wpisz adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności", Description = "Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności")]
        [StringLength(200)]
        [MaxLength(200)]
        public string WorkingAddress { get; set; }
        #endregion

        #region public IEnumerable<EntityPerson> Representatives { get; set; }
        /// <summary>
        ///Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL
        /// </summary>
        [NotMapped]
        [Display(Name = "Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL", Prompt = "Dodaj osoby wchodzące w skład organu uprawnionego do reprezentowania podmiotu", Description = "Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i / lub PESEL")]
        public IEnumerable<EntityPerson> Representatives { get; set; }
        #endregion

        #region public virtual ICollection<EntityPerson> Representative { get; set; }
        /// <summary>
        ///Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL
        /// </summary>
        [JsonProperty(nameof(Representative))]
        [Display(Name = "Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL", Prompt = "Dodaj osoby wchodzące w skład organu uprawnionego do reprezentowania podmiotu", Description = "Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i / lub PESEL")]
        [InverseProperty("Representative")]
        public virtual ICollection<EntityPerson> Representative { get; set; }
        #endregion

        #region public IEnumerable<EntityPerson> AuthorizedClerks { get; set; }
        /// <summary>
        /// Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL
        /// </summary>
        [NotMapped]
        [Display(Name = "Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL", Prompt = "Dodaj osoby prokurentów oraz ich numery NIP i/lub PESEL", Description = "Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL")]
        public IEnumerable<EntityPerson> AuthorizedClerks { get; set; }
        #endregion

        #region public virtual ICollection<EntityPerson> AuthorizedClerk { get; set; }
        /// <summary>
        /// Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL
        /// </summary>
        [JsonProperty(nameof(AuthorizedClerk))]
        [Display(Name = "Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL", Prompt = "Dodaj osoby prokurentów oraz ich numery NIP i/lub PESEL", Description = "Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL")]
        [InverseProperty("AuthorizedClerk")]
        public virtual ICollection<EntityPerson> AuthorizedClerk { get; set; }
        #endregion

        #region public IEnumerable<EntityPerson> Partners { get; set; }
        /// <summary>
        /// Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL
        /// </summary
        [NotMapped]
        [Display(Name = "Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL", Prompt = "Dodaj osoby wspólników Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL", Description = "Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL")]
        public IEnumerable<EntityPerson> Partners { get; set; }
        #endregion

        #region public virtual ICollection<EntityPerson> Partner { get; set; }
        /// <summary>
        /// Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL
        /// </summary>
        [JsonProperty(nameof(Partner))]
        [Display(Name = "Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL", Prompt = "Dodaj osoby wspólników Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL", Description = "Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL")]
        [InverseProperty("Partner")]
        public virtual ICollection<EntityPerson> Partner { get; set; }
        #endregion

        #region public DateTime? RegistrationLegalDate { get; set; }
        /// <summary>
        /// Data rejestracji jako podatnika VAT
        /// </summary>
        [Column("RegistrationLegalDate", TypeName = "date")]
        [JsonProperty(nameof(RegistrationLegalDate))]
        [Display(Name = "Data rejestracji jako podatnika VAT", Prompt = "Wpisz lub wybierz datę rejestracji jako podatnika VAT", Description = "Data rejestracji jako podatnika VAT")]
        public DateTime? RegistrationLegalDate { get; set; }
        #endregion

        #region public DateTime? RegistrationDenialDate { get; set; }
        /// <summary>
        /// Data odmowy rejestracji jako podatnika VAT
        /// </summary>
        [Column("RegistrationDenialDate", TypeName = "date")]
        [JsonProperty(nameof(RegistrationDenialDate))]
        [Display(Name = "Data odmowy rejestracji jako podatnika VAT", Prompt = "Wpisz lub wybierz datę odmowy rejestracji jako podatnika VAT", Description = "Data odmowy rejestracji jako podatnika VAT")]
        public DateTime? RegistrationDenialDate { get; set; }
        #endregion

        #region public string RegistrationDenialBasis { get; set; }
        /// <summary>
        /// Podstawa prawna odmowy rejestracji
        /// </summary>
        [Column("RegistrationDenialBasis", TypeName = "varchar(200)")]
        [JsonProperty(nameof(RegistrationDenialBasis))]
        [Display(Name = "Podstawa prawna odmowy rejestracji", Prompt = "Wpisz podstawę prawną odmowy rejestracji", Description = "Podstawa prawna odmowy rejestracji")]
        [StringLength(200)]
        [MaxLength(200)]
        public string RegistrationDenialBasis { get; set; }
        #endregion

        #region public DateTime? RestorationDate { get; set; }
        /// <summary>
        /// Data przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RestorationDate", TypeName = "date")]
        [JsonProperty(nameof(RestorationDate))]
        [Display(Name = "Data przywrócenia jako podatnika VAT", Prompt = "Wpisz lub wybierz datę przywrócenia jako podatnika VAT", Description = "Data przywrócenia jako podatnika VAT")]
        public DateTime? RestorationDate { get; set; }
        #endregion

        #region public string RestorationBasis { get; set; }
        /// <summary>
        /// Podstawa prawna przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RestorationBasis", TypeName = "varchar(200)")]
        [JsonProperty(nameof(RestorationBasis))]
        [Display(Name = "Podstawa prawna przywrócenia jako podatnika VAT", Prompt = "Wpisz podstawę prawną przywrócenia jako podatnika VAT", Description = "Podstawa prawna przywrócenia jako podatnika VAT")]
        [StringLength(200)]
        [MaxLength(200)]
        public string RestorationBasis { get; set; }
        #endregion

        #region public DateTime? RemovalDate { get; set; }
        /// <summary>
        /// Data przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RemovalDate", TypeName = "date")]
        [JsonProperty(nameof(RemovalDate))]
        [Display(Name = "Data wykreślenia odmowy rejestracji jako podatnika VAT", Prompt = "Wpisz lub wybierz datę wykreślenia odmowy rejestracji jako podatnika VAT", Description = "Data wykreślenia odmowy rejestracji jako podatnika VAT")]
        public DateTime? RemovalDate { get; set; }
        #endregion

        #region public string RemovalBasis { get; set; }
        /// <summary>
        /// Podstawa prawna przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RemovalBasis", TypeName = "varchar(200)")]
        [JsonProperty(nameof(RemovalBasis))]
        [Display(Name = "Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT", Prompt = "Wpisz podstawę prawną wykreślenia odmowy rejestracji jako podatnika VAT", Description = "Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT")]
        [StringLength(200)]
        [MaxLength(200)]
        public string RemovalBasis { get; set; }
        #endregion

        #region public IEnumerable<EntityAccountNumber> AccountNumbers { get; set; }
        /// <summary>
        /// Numery kont bankowych w formacie NRB
        /// </summary>
        [NotMapped]
        [Display(Name = "Numery kont bankowych w formacie NRB", Prompt = "Dodaj numery kont bankowych w formacie NRB", Description = "Numery kont bankowych w formacie NRB")]
        public IEnumerable<string> AccountNumbers { get; set; }
        #endregion

        #region public virtual ICollection<EntityAccountNumber> EntityAccountNumber { get; set; }
        /// <summary>
        /// Numery kont bankowych w formacie NRB
        /// </summary>
        [JsonProperty(nameof(EntityAccountNumber))]
        [Display(Name = "Numery kont bankowych w formacie NRB", Prompt = "Dodaj numery kont bankowych w formacie NRB", Description = "Numery kont bankowych w formacie NRB")]
        [InverseProperty("Entity")]
        public virtual ICollection<EntityAccountNumber> EntityAccountNumber { get; set; }
        #endregion

        #region public bool HasVirtualAccounts { get; set; }
        /// <summary>
        /// Podmiot posiada maski kont wirtualnych
        /// </summary>
        [Column("HasVirtualAccounts", TypeName = "bit")]
        [JsonProperty(nameof(HasVirtualAccounts))]
        [Display(Name = "Podmiot posiada maski kont wirtualnych", Prompt = "Wybierz, czy podmiot posiada maski kont wirtualnych", Description = "Podmiot posiada maski kont wirtualnych")]
        public bool HasVirtualAccounts { get; set; }
        #endregion

        #region public DateTime DateOfCreate { get; set; }
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Column("DateOfCreate", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfCreate))]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia", Description = "Data utworzenia")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateOfCreate { get; set; }
        #endregion

        #region public DateTime? DateOfModification { get; set; }
        /// <summary>
        /// Data modyfikacji
        /// </summary>
        [Column("DateOfModification", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfModification))]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji", Description = "Data modyfikacji")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfModification { get; set; }
        #endregion

        #region public DateTime? DateOfChecking { get; set; }
        /// <summary>
        /// Data modyfikacji
        /// </summary>
        [Column("DateOfChecking", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfChecking))]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji", Description = "Data modyfikacji")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfChecking { get; set; }
        #endregion

        //#region public string ObjectMD5Hash { get; set; }
        ///// <summary>
        ///// Skrót MD5
        ///// MD5 hash of the file content
        ///// </summary>
        //[JsonProperty(nameof(ObjectMD5Hash))]
        //[Column("ObjectMD5Hash", TypeName = "varchar(24)")]
        //[Display(Name = "Skrót MD5", Prompt = "Wpisz skrót MD5", Description = "Skrót MD5")]
        //[Required]
        //[StringLength(24)]
        //[MinLength(24)]
        //[MaxLength(24)]
        //public string ObjectMD5Hash { get; set; }
        //#endregion

        //#region public void SetObjectMD5Hash(string separator = null)
        ///// <summary>
        ///// Ustaw skrót MD5
        ///// Set the MD5 hash of the file content
        ///// </summary>
        ///// <param name="separator">
        ///// Separator rozdzielający wartości właściwości obiektu jako string
        ///// Separator separating object property values as a string
        ///// </param>
        ///// <returns>
        ///// Bieżący obiekt jako InvoiceFromIcasaXML
        ///// The current object as InvoiceFromIcasaXML
        ///// </returns>
        //public void SetObjectMD5Hash(string separator = null)
        //{
        //    try
        //    {
        //        ObjectMD5Hash = NetAppCommon.Helpers.Object.ObjectHelper.ConvertObjectValuesToMD5Hash(this, separator);
        //    }
        //    catch (Exception)
        //    {
        //        ObjectMD5Hash = null;
        //    }
        //}
        //#endregion

        #region public string GetValuesToString(string separator = null)
        /// <summary>
        /// Pobierz wartości właściwości obiektu i zbuduj ciąg tekstowy rozdzielonych wartości właściwości separatorem
        /// Get the object property values and build a text string separated by a property value with a separator
        /// </summary>
        /// <param name="separator">
        /// Separator rozdzielający wartości właściwości obiektu jako string
        /// Separator separating object property values as a string
        /// </param>
        /// <returns>
        /// Wartości właściwości obiektu rozdzielone separatorem jako string
        /// Object property values separated by a separator as a string
        /// </returns>
        public string GetValuesToString(string separator = null)
        {
            return NetAppCommon.Helpers.Object.ObjectHelper.GetValuesToString(this, separator);
        }
        #endregion
    }
    #endregion
}