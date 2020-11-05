using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class EntityAccountNumber
    /// <summary>
    /// Model danych numery rachunków bankowych podmiotu jako EntityAccountNumber
    /// Data model Entity's bank account numbers as EntityAccountNumber
    /// </summary>
    [Table("EntityAccountNumber", Schema = "awpv")]
    public partial class EntityAccountNumber
    {
        #region public EntityAccountNumber()
        /// <summary>
        /// Konstruktor
        /// Construktor
        /// </summary>
        public EntityAccountNumber()
        {
            SetUniqueIdentifierOfTheLoggedInUser();
        }
        #endregion

        #region public Guid Id { get; set; }
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [JsonProperty(nameof(Id))]
        [Display(Name = "Identyfikator numeru rachunku bankowowego", Prompt = "Wpisz identyfikator numeru rachunku bankowowego", Description = "Identyfikator numeru rachunku bankowowego klucz główny")]
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
                UniqueIdentifierOfTheLoggedInUser = string.Empty;
            }
        }
        #endregion

        #region public Guid? EntityId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        [JsonProperty(nameof(EntityId))]
        public Guid? EntityId { get; set; }
        #endregion

        #region public virtual Entity Entity { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli Entity
        /// </summary>
        [ForeignKey(nameof(EntityId))]
        [InverseProperty("EntityAccountNumber")]
        [JsonProperty(nameof(Entity))]
        public virtual Entity Entity { get; set; }
        #endregion

        #region public string AccountNumber { get; set; }
        /// <summary>
        /// Numer rachunku bankowego (26 znaków) w formacie NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// Bank account number (26 characters) in the format NRB (kkAAAAAAAABBBBBBBBBBBBBBBB)
        /// </summary>
        [Column("AccountNumber", TypeName = "varchar(32)")]
        [JsonProperty(nameof(AccountNumber))]
        [Display(Name = "Numer konta bankowego w formacie NRB", Prompt = "Wpisz numer konta bankowego w formacie NRB", Description = "Numer konta bankowego w formacie NRB")]
        [Required]
        [StringLength(32)]
        [MinLength(26)]
        [MaxLength(32)]
        [RegularExpression(@"^\d{26}$")]
        public string AccountNumber { get; set; }
        #endregion

        #region public DateTime DateOfCreate
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Column("DateOfCreate", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfCreate))]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia", Description = "Data utworzenia")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateOfCreate { get; set; }
        #endregion

        #region public DateTime? DateOfModification
        /// <summary>
        /// Data modyfikacji
        /// </summary>
        [Column("DateOfModification", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfModification))]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji", Description = "Data modyfikacji")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfModification { get; set; }
        #endregion
    }
    #endregion
}