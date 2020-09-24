using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class EntityAccountNumber
    /// <summary>
    /// Model danych EntityAccountNumber
    /// </summary>
    [Table("EntityAccountNumber", Schema = "ApiWykazuPodatnikowVat")]
    public partial class EntityAccountNumber
    {
        #region public Guid Id { get; set; }
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
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

        #region public Guid? EntityId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli Entity jako Guid?
        /// </summary>
        public Guid? EntityId { get; set; }
        #endregion

        #region public virtual Entity Entity { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli Entity
        /// </summary>
        [ForeignKey(nameof(EntityId))]
        [InverseProperty("EntityAccountNumber")]
        public virtual Entity Entity { get; set; }
        #endregion

        #region public string AccountNumber { get; set; }
        /// <summary>
        /// Numer konta bankowego w formacie NRB
        /// </summary>
        [Column("AccountNumber", TypeName = "varchar(32)")]
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
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia", Description = "Data utworzenia")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateOfCreate { get; set; }
        #endregion

        #region public DateTime? DateOfModification
        /// <summary>
        /// Data modyfikacji
        /// </summary>
        [Column("DateOfModification", TypeName = "datetime")]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji", Description = "Data modyfikacji")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfModification { get; set; }
        #endregion
    }
    #endregion
}
