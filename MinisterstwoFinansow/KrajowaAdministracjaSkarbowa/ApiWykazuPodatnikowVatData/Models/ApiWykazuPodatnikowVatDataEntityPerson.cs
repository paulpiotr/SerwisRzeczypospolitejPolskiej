using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class ApiWykazuPodatnikowVatDataEntityPerson
    /// <summary>
    /// Model danych ApiWykazuPodatnikowVatDataEntityPerson, oryginalnie EntityPerson
    /// </summary>
    public partial class ApiWykazuPodatnikowVatDataEntityPerson
    {
        #region public Guid Id { get; set; }, identyfikator, klucz główny
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator, klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public Guid ApiWykazuPodatnikowVatDataEntityId, klucz obcy, odniesienie do rekordu w tabeli ApiWykazuPodatnikowVatDataEntity
        /// <summary>
        /// Guid ApiWykazuPodatnikowVatDataEntityId, klucz obcy, odniesienie do rekordu w tabeli ApiWykazuPodatnikowVatDataEntity
        /// </summary>
        [Display(Name = "Odniesienie do rekordu w tabeli ApiWykazuPodatnikowVatDataEntity", Prompt = "Wybierz odniesienie do rekordu w tabeli ApiWykazuPodatnikowVatDataEntity", Description = "Odniesienie do rekordu w tabeli ApiWykazuPodatnikowVatDataEntity")]
        public Guid? ApiWykazuPodatnikowVatDataEntityId { get; set; }
        #endregion

        #region public string CompanyName { get; set; }, Nazwa firmy
        /// <summary>
        /// Nazwa firmy
        /// </summary>
        [Column("CompanyName", TypeName = "varchar(256)")]
        [Display(Name = "Nazwa firmy", Prompt = "Wpisz nazwę firmy", Description = "Nazwa firmy")]
        [Required]
        [StringLength(256)]
        [MinLength(1)]
        [MaxLength(256)]
        public string CompanyName { get; set; }
        #endregion

        #region public string FirstName { get; set; }, Imię
        /// <summary>
        /// Imię
        /// </summary>
        [Column("FirstName", TypeName = "varchar(60)")]
        [Display(Name = "Imię", Prompt = "Wpisz imię", Description = "Imię")]
        [StringLength(60)]
        [MaxLength(60)]
        public string FirstName { get; set; }
        #endregion

        #region public string LastName { get; set; }, Nazwisko
        /// <summary>
        /// Nazwisko
        /// </summary>
        [Column("LastName", TypeName = "varchar(160)")]
        [Display(Name = "Nazwisko", Prompt = "Wpisz imię", Description = "Nazwisko")]
        [StringLength(160)]
        [MaxLength(160)]
        public string LastName { get; set; }
        #endregion

        #region public Guid? ApiWykazuPodatnikowVatDataEntityPersonPeselId { get; set; }
        /// <summary>
        /// public Guid? ApiWykazuPodatnikowVatDataEntityPersonPeselId { get; set; }
        /// Numer Pesel
        /// </summary>
        [Display(Name = "Numer Pesel", Prompt = "Wpisz pesel", Description = "Numer pesel")]
        public Guid? ApiWykazuPodatnikowVatDataEntityPersonPeselId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntityPesel Pesel { get; set; }
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityPersonPeselId))]
        [InverseProperty(nameof(ApiWykazuPodatnikowVatDataEntityPesel.ApiWykazuPodatnikowVatDataEntityPerson))]
        public virtual ApiWykazuPodatnikowVatDataEntityPesel Pesel { get; set; }
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
