using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class EntityCheck
    /// <summary>
    /// Model danych EntityCheck
    /// </summary>
    [Table("EntityCheck", Schema = "ApiWykazuPodatnikowVat")]
    public partial class EntityCheck
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

        #region public string AccountAssigned { get; set; }
        /// <summary>
        /// Czy rachunek przypisany do podmiotu czynnego
        /// </summary>
        [Column("AccountAssigned", TypeName = "varchar(3)")]
        [Display(Name = "Czy rachunek przypisany do podmiotu czynnego", Prompt = "Wybierz, czy rachunek jest przypisany do podmiotu czynnego", Description = "Czy rachunek przypisany do podmiotu czynnego")]
        [StringLength(3)]
        [MinLength(3)]
        [MaxLength(3)]
        public string AccountAssigned { get; set; }
        #endregion

        #region public DateTime RequestDateTime
        /// <summary>
        /// Data wysłania żądania
        /// </summary>
        [Column("RequestDateTime", TypeName = "datetime")]
        [Display(Name = "Data wysłania żądania", Prompt = "Wpisz lub wybierz datę wysłania żądania", Description = "Data wysłania żądania")]
        public DateTime RequestDateTime { get; set; }
        #endregion

        #region public string RequestId { get; set; }
        /// <summary>
        /// Numer (id) odpowiedzi jako string
        /// </summary>
        [Column("RequestId", TypeName = "varchar(18)")]
        [Display(Name = "Numer (id) odpowiedzi", Prompt = "Wpisz numer (id) odpowiedzi", Description = "Numer (id) odpowiedzi")]
        [StringLength(18)]
        [MinLength(18)]
        [MaxLength(18)]
        public string RequestId { get; set; }
        #endregion

        #region public DateTime DateOfCreate
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Column("DateOfCreate", TypeName = "datetime")]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia", Description = "Data utworzenia")]
        public DateTime DateOfCreate { get; set; }
        #endregion

        #region public DateTime? DateOfModification
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
