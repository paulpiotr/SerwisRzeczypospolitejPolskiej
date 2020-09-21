using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class ApiWykazuPodatnikowVatDataEntity, Model danych ApiWykazuPodatnikowVatDataEntity, oryginalnie Entity
    /// <summary>
    /// Model danych ApiWykazuPodatnikowVatDataEntity, oryginalnie Entity
    /// </summary>
    [Table("ApiWykazuPodatnikowVatDataEntity", Schema = "dbo")]
    public partial class ApiWykazuPodatnikowVatDataEntity
    {
        #region public Guid Id { get; set; }, identyfikator, klucz główny
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public string Name { get; set; }, Firma (nazwa) lub imię i nazwisko
        /// <summary>
        /// Firma (nazwa) lub imię i nazwisko
        /// </summary>
        [Column("Name", TypeName = "varchar(256)")]
        [Display(Name = "Firma (nazwa) lub imię i nazwisko", Prompt = "Wpisz firmę (nazwę) lub imię i nazwisko", Description = "Firma (nazwa) lub imię i nazwisko")]
        [Required]
        [StringLength(256)]
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; }
        #endregion

        #region public string Nip { get; set; }, Numer nip
        /// <summary>
        /// Numer nip
        /// </summary>
        [Column("Nip", TypeName = "varchar(10)")]
        [Display(Name = "Numer nip", Prompt = "Wpisz nip", Description = "Numer nip")]
        [Required]
        [StringLength(10)]
        [MinLength(10)]
        [MaxLength(10)]
        public string Nip { get; set; }
        #endregion

        #region public string StatusVat { get; set; }, Status podatnika VAT, - Czynny lub Zwolniony lub Niezarejestrowany
        /// <summary>
        /// Status podatnika VAT, - Czynny lub Zwolniony lub Niezarejestrowany
        /// </summary>
        [Column("StatusVat", TypeName = "varchar(32)")]
        [Display(Name = "Status podatnika VAT", Prompt = "Wybierz status podatnika VAT", Description = "Status podatnika VAT, (Czynny lub Zwolniony lub Niezarejestrowany)")]
        [Required]
        [StringLength(32)]
        [MinLength(1)]
        [MaxLength(32)]
        public string StatusVat { get; set; }
        #endregion

        #region public string Regon { get; set; }, Numer regon
        /// <summary>
        /// Numer regon
        /// </summary>
        [Column("Regon", TypeName = "varchar(14)")]
        [Display(Name = "Regon", Prompt = "Wpisz regon", Description = "Numer regon")]
        [Required]
        [StringLength(14)]
        [MinLength(9)]
        [MaxLength(14)]
        [RegularExpression(@"^\d{9}$|^\d{14}$")]
        public string Regon { get; set; }
        #endregion

        #region public Guid? ApiWykazuPodatnikowVatDataEntityPeselId { get; set; }
        /// <summary>
        /// Powiązanie do tabeli ApiWykazuPodatnikowVatDataEntityPesel
        /// Numer Pesel
        /// </summary>
        [Display(Name = "Numer Pesel", Prompt = "Wybierz powiązanie numeru pesel", Description = "Numer pesel")]
        public Guid? ApiWykazuPodatnikowVatDataEntityPeselId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntityPesel Pesel { get; set; }
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityPeselId))]
        [InverseProperty(nameof(ApiWykazuPodatnikowVatDataEntityPesel.ApiWykazuPodatnikowVatDataEntity))]
        public virtual ApiWykazuPodatnikowVatDataEntityPesel Pesel { get; set; }
        #endregion

        #region public string Krs { get; set; }, Numer Krs
        /// <summary>
        /// Numer Krs
        /// </summary>
        [Column("Krs", TypeName = "varchar(10)")]
        [Display(Name = "Numer Krs", Prompt = "Wpisz numer Krs", Description = "Numer Krs")]
        [Required]
        [StringLength(10)]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$")]
        public string Krs { get; set; }
        #endregion

        #region public string ResidenceAddress { get; set; }, Adres siedziby
        /// <summary>
        /// Adres siedziby
        /// </summary>
        [Column("ResidenceAddress", TypeName = "varchar(200)")]
        [Display(Name = "Adres siedziby", Prompt = "Wpisz adres siedziby", Description = "Adres siedziby")]
        [Required]
        [StringLength(200)]
        [MinLength(1)]
        [MaxLength(200)]
        public string ResidenceAddress { get; set; }
        #endregion

        #region public string WorkingAddress { get; set; }, Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania
        /// <summary>
        /// Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania
        /// </summary>
        [Column("WorkingAddress", TypeName = "varchar(200)")]
        [Display(Name = "Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania", Prompt = "Wpisz adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności", Description = "Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności")]
        [Required]
        [StringLength(200)]
        [MinLength(1)]
        [MaxLength(200)]
        public string WorkingAddres { get; set; }
        #endregion

        //#region public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> Representatives { get; set; }
        ///// <summary>
        /////Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL
        /////representatives:
        /////type: array
        /////items:
        /////$ref: '#/components/schemas/EntityPerson'
        ///// </summary>
        //[Display(Name = "Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL", Prompt = "Dodaj osoby wchodzące w skład organu uprawnionego do reprezentowania podmiotu", Description = "Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i / lub PESEL")]
        //[InverseProperty("Representatives")]
        //public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> Representatives { get; set; }
        //#endregion

        //#region public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> AuthorizedClerks { get; set; }
        ///// <summary>
        ///// Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL
        ///// </summary>
        //[Display(Name = "Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL", Prompt = "Dodaj osoby prokurentów oraz ich numery NIP i/lub PESEL", Description = "Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL")]
        //[InverseProperty("AuthorizedClerks")]
        //public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> AuthorizedClerks { get; set; }
        //#endregion

        //#region public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> Partners { get; set; }
        ///// <summary>
        ///// Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL
        ///// </summary>
        //[Display(Name = "Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL", Prompt = "Dodaj osoby wspólników Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL", Description = "Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numery NIP i/lub PESEL")]
        //[InverseProperty("Partners")]
        //public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> Partners { get; set; }
        //#endregion

        #region public DateTime? RegistrationLegalDate, Data rejestracji jako podatnika VAT
        /// <summary>
        /// Data rejestracji jako podatnika VAT
        /// </summary>
        [Column("RegistrationLegalDate", TypeName = "date")]
        [Display(Name = "Data rejestracji jako podatnika VAT", Prompt = "Wpisz lub wybierz datę rejestracji jako podatnika VAT", Description = "Data rejestracji jako podatnika VAT")]
        public DateTime? RegistrationLegalDate { get; set; }
        #endregion

        #region public DateTime? RestorationDate, Data odmowy rejestracji jako podatnika VAT
        /// <summary>
        /// Data odmowy rejestracji jako podatnika VAT
        /// </summary>
        [Column("RegistrationDenialDate", TypeName = "date")]
        [Display(Name = "Data odmowy rejestracji jako podatnika VAT", Prompt = "Wpisz lub wybierz datę odmowy rejestracji jako podatnika VAT", Description = "Data odmowy rejestracji jako podatnika VAT")]
        public DateTime? RegistrationDenialDate { get; set; }
        #endregion

        #region public string RegistrationDenialBasis { get; set; }, Podstawa prawna odmowy rejestracji
        /// <summary>
        /// Podstawa prawna odmowy rejestracji
        /// </summary>
        [Column("RegistrationDenialBasis", TypeName = "varchar(200)")]
        [Display(Name = "Podstawa prawna odmowy rejestracji", Prompt = "Wpisz podstawę prawną odmowy rejestracji", Description = "Podstawa prawna odmowy rejestracji")]
        [StringLength(200)]
        [MaxLength(200)]
        public string RegistrationDenialBasis { get; set; }
        #endregion

        #region public DateTime? RestorationDate { get; set; }, Data przywrócenia jako podatnika VAT
        /// <summary>
        /// Data przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RestorationDate", TypeName = "date")]
        [Display(Name = "Data przywrócenia jako podatnika VAT", Prompt = "Wpisz lub wybierz datę przywrócenia jako podatnika VAT", Description = "Data przywrócenia jako podatnika VAT")]
        public DateTime? RestorationDate { get; set; }
        #endregion

        #region public string RestorationBasis { get; set; }, Podstawa prawna przywrócenia jako podatnika VAT
        /// <summary>
        /// Podstawa prawna przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RestorationBasis", TypeName = "varchar(200)")]
        [Display(Name = "Podstawa prawna przywrócenia jako podatnika VAT", Prompt = "Wpisz podstawę prawną przywrócenia jako podatnika VAT", Description = "Podstawa prawna przywrócenia jako podatnika VAT")]
        [StringLength(200)]
        [MaxLength(200)]
        public string RestorationBasis { get; set; }
        #endregion

        #region public DateTime? RemovalDate { get; set; } Data wykreślenia odmowy rejestracji jako podatnika VAT
        /// <summary>
        /// Data przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RemovalDate", TypeName = "date")]
        [Display(Name = "Data wykreślenia odmowy rejestracji jako podatnika VAT", Prompt = "Wpisz lub wybierz datę wykreślenia odmowy rejestracji jako podatnika VAT", Description = "Data wykreślenia odmowy rejestracji jako podatnika VAT")]
        public DateTime? RemovalDate { get; set; }
        #endregion

        #region public string RemovalBasis { get; set; }, Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT
        /// <summary>
        /// Podstawa prawna przywrócenia jako podatnika VAT
        /// </summary>
        [Column("RemovalBasis", TypeName = "varchar(200)")]
        [Display(Name = "Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT", Prompt = "Wpisz podstawę prawną wykreślenia odmowy rejestracji jako podatnika VAT", Description = "Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT")]
        [StringLength(200)]
        [MaxLength(200)]
        public string RemovalBasis { get; set; }
        #endregion

        #region accountNumbers to do
        //accountNumbers:
        //  type: array
        //  items:
        //    type: string
        //    minLength: 26
        //    maxLength: 26
        //    example: '90249000050247256316596736'
        //    description: |
        //      Numer konta bankowego w formacie NRB
        #endregion

        #region public bool HasVirtualAccounts { get; set; }, Podmiot posiada maski kont wirtualnych
        /// <summary>
        /// Podmiot posiada maski kont wirtualnych
        /// </summary>
        [Column("HasVirtualAccounts", TypeName = "bit")]
        [Display(Name = "Podmiot posiada maski kont wirtualnych", Prompt = "Wybierz, czy podmiot posiada maski kont wirtualnych", Description = "Podmiot posiada maski kont wirtualnych")]
        public bool HasVirtualAccounts { get; set; }
        #endregion

        #region public DateTime DateOfCreate, Data utworzenia
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Column("DateOfCreate", TypeName = "datetime")]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia", Description = "Data utworzenia")]
        public DateTime DateOfCreate { get; set; }
        #endregion

        #region public DateTime? DateOfModification, Data modyfikacji
        /// <summary>
        /// Data modyfikacji
        /// </summary>
        [Column("DateOfModification", TypeName = "datetime")]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji", Description = "Data modyfikacji")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfModification { get; set; }
        #endregion
    }
    #endregion
}
