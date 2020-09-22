using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class ApiWykazuPodatnikowVatDataEntityPerson
    /// <summary>
    /// Model danych ApiWykazuPodatnikowVatDataEntityPerson, oryginalnie EntityPerson
    /// </summary>
    [Table("ApiWykazuPodatnikowVatDataEntityPerson", Schema = "ApiWykazuPodatnikowVat")]
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

        #region public Guid? ApiWykazuPodatnikowVatDataEntityRepresentativesId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli ApiWykazuPodatnikowVatDataEntity jako Guid?
        /// </summary>
        public Guid? ApiWykazuPodatnikowVatDataEntityRepresentativesId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntity Representative { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli ApiWykazuPodatnikowVatDataEntity
        /// </summary>
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityRepresentativesId))]
        [InverseProperty(nameof(ApiWykazuPodatnikowVatDataEntity.Representative))]
        public virtual ApiWykazuPodatnikowVatDataEntity Representative { get; set; }
        #endregion

        #region public Guid? ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli ApiWykazuPodatnikowVatDataEntity jako Guid?
        /// </summary>
        public Guid? ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntity AuthorizedClerk { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli ApiWykazuPodatnikowVatDataEntity
        /// </summary>
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityAuthorizedClerksId))]
        [InverseProperty(nameof(ApiWykazuPodatnikowVatDataEntity.AuthorizedClerk))]
        public virtual ApiWykazuPodatnikowVatDataEntity AuthorizedClerk { get; set; }
        #endregion

        #region public Guid? ApiWykazuPodatnikowVatDataEntityPartnersId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli ApiWykazuPodatnikowVatDataEntity jako Guid?
        /// </summary>
        public Guid? ApiWykazuPodatnikowVatDataEntityPartnersId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntity Partner { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli ApiWykazuPodatnikowVatDataEntity
        /// </summary>
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityPartnersId))]
        [InverseProperty(nameof(ApiWykazuPodatnikowVatDataEntity.Partner))]
        public virtual ApiWykazuPodatnikowVatDataEntity Partner { get; set; }
        #endregion

        #region public Guid? ApiWykazuPodatnikowVatDataEntityPeselId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli ApiWykazuPodatnikowVatDataEntityPesel jako Guid?
        /// </summary>
        public Guid? ApiWykazuPodatnikowVatDataEntityPeselId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntityPesel Pesel { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli ApiWykazuPodatnikowVatDataEntityPesel
        /// </summary>
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityPeselId))]
        [InverseProperty(nameof(ApiWykazuPodatnikowVatDataEntityPesel.ApiWykazuPodatnikowVatDataEntityPerson))]
        public virtual ApiWykazuPodatnikowVatDataEntityPesel Pesel { get; set; }
        #endregion

        #region public string CompanyName { get; set; }, Nazwa firmy
        /// <summary>
        /// Nazwa firmy
        /// </summary>
        [Column("CompanyName", TypeName = "varchar(256)")]
        [Display(Name = "Nazwa firmy", Prompt = "Wpisz nazwę firmy", Description = "Nazwa firmy")]
        [StringLength(256)]
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

        #region public string Nip { get; set; }, Numer nip
        /// <summary>
        /// Numer nip
        /// </summary>
        [Column("Nip", TypeName = "varchar(10)")]
        [Display(Name = "Numer nip", Prompt = "Wpisz nip", Description = "Numer nip")]
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
