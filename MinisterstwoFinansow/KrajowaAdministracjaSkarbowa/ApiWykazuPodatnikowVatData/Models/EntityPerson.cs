#region using

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetAppCommon;
using Newtonsoft.Json;

#endregion

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public class EntityPerson

    /// <summary>
    ///     Model danych EntityPerson osoby powiązane z podmiotem gospodarczym
    ///     EntityPerson data model persons associated with the business entity
    /// </summary>
    [Table("EntityPerson", Schema = "awpv")]
    public class EntityPerson
    {
        #region public EntityPerson()

        /// <summary>
        ///     Konstruktor
        ///     Constructor
        /// </summary>
        public EntityPerson()
        {
            SetUniqueIdentifierOfTheLoggedInUser();
        }

        #endregion

        #region public Guid Id { get; set; }, identyfikator, klucz główny

        /// <summary>
        ///     Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [JsonProperty(nameof(Id))]
        [Display(Name = "Identyfikator osoby powiązanj z podmiotem gospodarczym",
            Prompt = "Wpisz identyfikator osoby powiązanj z podmiotem gospodarczym",
            Description = "Identyfikator osoby powiązanj z podmiotem gospodarczym, klucz główny")]
        public Guid Id { get; set; }

        #endregion

        #region public string UniqueIdentifierOfTheLoggedInUser { get; private set; }

        /// <summary>
        ///     Jednoznaczny identyfikator zalogowanego użytkownika
        ///     Unique identifier of the logged in user
        /// </summary>
        [JsonProperty(nameof(UniqueIdentifierOfTheLoggedInUser))]
        [Column("UniqueIdentifierOfTheLoggedInUser", TypeName = "varchar(512)")]
        [Display(Name = "Identyfikator zalogowanego użytkownika",
            Prompt = "Wybierz identyfikator zalogowanego użytkownika",
            Description = "Identyfikator zalogowanego użytkownika")]
        [StringLength(512)]
        [Required]
        public string UniqueIdentifierOfTheLoggedInUser { get; set; }

        #endregion

        #region public Guid? EntityRepresentativeId { get; set; }

        /// <summary>
        ///     Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        [JsonProperty(nameof(EntityRepresentativeId))]
        public Guid? EntityRepresentativeId { get; set; }

        #endregion

        #region public virtual Entity Representative { get; set; }

        /// <summary>
        ///     Kolekcja objektów tabeli Entity
        /// </summary>
        [JsonProperty(nameof(Representative))]
        [ForeignKey(nameof(EntityRepresentativeId))]
        [InverseProperty(nameof(Entity.Representative))]
        public virtual Entity Representative { get; set; }

        #endregion

        #region public Guid? EntityAuthorizedClerkId { get; set; }

        /// <summary>
        ///     Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        [JsonProperty(nameof(EntityAuthorizedClerkId))]
        public Guid? EntityAuthorizedClerkId { get; set; }

        #endregion

        #region public virtual Entity AuthorizedClerk { get; set; }

        /// <summary>
        ///     Kolekcja objektów tabeli Entity
        /// </summary>
        [JsonProperty(nameof(AuthorizedClerk))]
        [ForeignKey(nameof(EntityAuthorizedClerkId))]
        [InverseProperty(nameof(Entity.AuthorizedClerk))]
        public virtual Entity AuthorizedClerk { get; set; }

        #endregion

        #region public Guid? EntityPartnerId { get; set; }

        /// <summary>
        ///     Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        [JsonProperty(nameof(EntityPartnerId))]
        public Guid? EntityPartnerId { get; set; }

        #endregion

        #region public virtual Entity Partner { get; set; }

        /// <summary>
        ///     Kolekcja objektów tabeli Entity
        /// </summary>
        [JsonProperty(nameof(Partner))]
        [ForeignKey(nameof(EntityPartnerId))]
        [InverseProperty(nameof(Entity.Partner))]
        public virtual Entity Partner { get; set; }

        #endregion

        #region public string Pesel { get; set; }

        /// <summary>
        ///     Numer pesel
        /// </summary>
        [JsonProperty(nameof(Pesel))]
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
        ///     Nazwa firmy
        /// </summary>
        [JsonProperty(nameof(CompanyName))]
        [Column("CompanyName", TypeName = "varchar(256)")]
        [Display(Name = "Nazwa firmy", Prompt = "Wpisz nazwę firmy", Description = "Nazwa firmy")]
        [StringLength(256)]
        [MaxLength(256)]
        public string CompanyName { get; set; }

        #endregion

        #region public string FirstName { get; set; }, Imię

        /// <summary>
        ///     Imię
        /// </summary>
        [JsonProperty(nameof(FirstName))]
        [Column("FirstName", TypeName = "varchar(60)")]
        [Display(Name = "Imię", Prompt = "Wpisz imię", Description = "Imię")]
        [StringLength(60)]
        [MaxLength(60)]
        public string FirstName { get; set; }

        #endregion

        #region public string LastName { get; set; }, Nazwisko

        /// <summary>
        ///     Nazwisko
        /// </summary>
        [JsonProperty(nameof(LastName))]
        [Column("LastName", TypeName = "varchar(160)")]
        [Display(Name = "Nazwisko", Prompt = "Wpisz imię", Description = "Nazwisko")]
        [StringLength(160)]
        [MaxLength(160)]
        public string LastName { get; set; }

        #endregion

        #region public string Nip { get; set; }

        /// <summary>
        ///     Numer NIP
        /// </summary>
        [JsonProperty(nameof(Nip))]
        [Column("Nip", TypeName = "varchar(10)")]
        [Display(Name = "Numer NIP", Prompt = "Wpisz nip", Description = "Numer NIP")]
        [StringLength(10)]
        [MinLength(10)]
        [MaxLength(10)]
        public string Nip { get; set; }

        #endregion

        #region public DateTime DateOfCreate, Data utworzenia

        /// <summary>
        ///     Data utworzenia
        /// </summary>
        [JsonProperty(nameof(DateOfCreate))]
        [Column("DateOfCreate", TypeName = "datetime")]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia",
            Description = "Data utworzenia")]
        public DateTime DateOfCreate { get; set; }

        #endregion

        #region public DateTime? DateOfModification, Data modyfikacji

        /// <summary>
        ///     Data modyfikacji
        /// </summary>
        [JsonProperty(nameof(DateOfModification))]
        [Column("DateOfModification", TypeName = "datetime")]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji",
            Description = "Data modyfikacji")]
        public DateTime? DateOfModification { get; set; }

        #endregion

        #region public void SetUniqueIdentifierOfTheLoggedInUser()

        /// <summary>
        ///     Ustaw jednoznaczny identyfikator zalogowanego użytkownika
        ///     Set a unique identifier for the logged in user
        /// </summary>
        public void SetUniqueIdentifierOfTheLoggedInUser()
        {
            try
            {
                UniqueIdentifierOfTheLoggedInUser = HttpContextAccessor.AppContext.GetCurrentUserIdentityName();
            }
            catch (Exception)
            {
                UniqueIdentifierOfTheLoggedInUser = string.Empty;
            }
        }

        #endregion
    }

    #endregion
}
