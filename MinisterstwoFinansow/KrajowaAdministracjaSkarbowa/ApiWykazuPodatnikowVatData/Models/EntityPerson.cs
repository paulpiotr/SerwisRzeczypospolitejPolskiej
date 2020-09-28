using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class EntityPerson
    /// <summary>
    /// Model danych EntityPerson, oryginalnie EntityPerson
    /// </summary>
    [Table("EntityPerson", Schema = "ApiWykazuPodatnikowVat")]
    public partial class EntityPerson
    {
        #region public Guid Id { get; set; }, identyfikator, klucz główny
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator, klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public string UniqueIdentifierOfTheLoggedInUser { get; set; }
        /// <summary>
        /// Jednoznaczny identyfikator zalogowanego użytkownika
        /// Unique identifier of the logged in user
        /// </summary>
        [Column("UniqueIdentifierOfTheLoggedInUser", TypeName = "varchar(512)")]
        [Display(Name = "Identyfikator zalogowanego użytkownika", Prompt = "Wybierz identyfikator zalogowanego użytkownika", Description = "Identyfikator zalogowanego użytkownika")]
        [StringLength(512)]
        [Required]
        public string UniqueIdentifierOfTheLoggedInUser { get; set; }
        #endregion

        #region public Guid? EntityRepresentativeId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        public Guid? EntityRepresentativeId { get; set; }
        #endregion

        #region public virtual Entity Representative { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli Entity
        /// </summary>
        [ForeignKey(nameof(EntityRepresentativeId))]
        [InverseProperty(nameof(Entity.Representative))]
        public virtual Entity Representative { get; set; }
        #endregion

        #region public Guid? EntityAuthorizedClerkId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        public Guid? EntityAuthorizedClerkId { get; set; }
        #endregion

        #region public virtual Entity AuthorizedClerk { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli Entity
        /// </summary>
        [ForeignKey(nameof(EntityAuthorizedClerkId))]
        [InverseProperty(nameof(Entity.AuthorizedClerk))]
        public virtual Entity AuthorizedClerk { get; set; }
        #endregion

        #region public Guid? EntityPartnerId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        public Guid? EntityPartnerId { get; set; }
        #endregion

        #region public virtual Entity Partner { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli Entity
        /// </summary>
        [ForeignKey(nameof(EntityPartnerId))]
        [InverseProperty(nameof(Entity.Partner))]
        public virtual Entity Partner { get; set; }
        #endregion

        //#region public Guid? EntityPeselId { get; set; }
        ///// <summary>
        ///// Odniesienie (klucz obcy) do tabeli EntityPesel jako Guid?
        ///// </summary>
        //public Guid? EntityPeselId { get; set; }
        //#endregion

        //#region public virtual EntityPesel Pesel { get; set; }
        ///// <summary>
        ///// Kolekcja objektów tabeli EntityPesel
        ///// </summary>
        //[ForeignKey(nameof(EntityPeselId))]
        //[InverseProperty(nameof(EntityPesel.EntityPerson))]
        //public virtual EntityPesel Pesel { get; set; }
        //#endregion

        #region public string Pesel { get; set; }
        /// <summary>
        /// Numer pesel
        /// </summary>
        [Column("Pesel", TypeName = "varchar(11)")]
        [Display(Name = "Pesel", Prompt = "Wpisz pesel", Description = "Numer pesel")]
        [StringLength(11)]
        [MinLength(11)]
        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$")]
        public string Pesel { get; set; }
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

        #region public string Nip { get; set; }
        /// <summary>
        /// Numer NIP
        /// </summary>
        [Column("Nip", TypeName = "varchar(10)")]
        [Display(Name = "Numer NIP", Prompt = "Wpisz nip", Description = "Numer NIP")]
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
