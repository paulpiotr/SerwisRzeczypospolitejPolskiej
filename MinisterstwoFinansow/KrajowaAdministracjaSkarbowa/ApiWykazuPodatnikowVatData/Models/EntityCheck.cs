using Newtonsoft.Json;
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
        [JsonProperty(nameof(Id))]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public string UniqueIdentifierOfTheLoggedInUser { get; set; }
        /// <summary>
        /// Jednoznaczny identyfikator zalogowanego użytkownika
        /// Unique identifier of the logged in user
        /// </summary>
        [Column("UniqueIdentifierOfTheLoggedInUser", TypeName = "varchar(512)")]
        [JsonProperty(nameof(UniqueIdentifierOfTheLoggedInUser))]
        [Display(Name = "Identyfikator zalogowanego użytkownika", Prompt = "Wybierz identyfikator zalogowanego użytkownika", Description = "Identyfikator zalogowanego użytkownika")]
        [StringLength(512)]
        [Required]
        public string UniqueIdentifierOfTheLoggedInUser { get; set; }
        #endregion

        #region public string Nip { get; set; }
        /// <summary>
        /// Numer identyfikacji podatkowej NIP jako string [^\d{10}$]
        /// NIP tax identification number as string [^\d{10}$]
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

        #region public string AccountNumber { get; set; }
        /// <summary>
        /// Numer konta bankowego w formacie NRB
        /// </summary>
        [Column("AccountNumber", TypeName = "varchar(32)")]
        [JsonProperty(nameof(AccountNumber))]
        [Display(Name = "Numer konta bankowego w formacie NRB", Prompt = "Wpisz numer konta bankowego w formacie NRB", Description = "Numer konta bankowego w formacie NRB")]
        [StringLength(32)]
        [MinLength(26)]
        [MaxLength(32)]
        [RegularExpression(@"^\d{26}$")]
        public string AccountNumber { get; set; }
        #endregion

        #region public string AccountAssigned { get; set; }
        /// <summary>
        /// Czy rachunek przypisany do podmiotu czynnego
        /// </summary>
        [Column("AccountAssigned", TypeName = "varchar(3)")]
        [JsonProperty(nameof(AccountAssigned))]
        [Display(Name = "Czy rachunek przypisany do podmiotu czynnego", Prompt = "Wybierz, czy rachunek jest przypisany do podmiotu czynnego", Description = "Czy rachunek przypisany do podmiotu czynnego")]
        [Required]
        [StringLength(3)]
        [MinLength(3)]
        [MaxLength(3)]
        public string AccountAssigned { get; set; }
        #endregion

        #region public DateTime RequestDateTime
        /// <summary>
        /// Data wysłania żądania w formacie string
        /// </summary>
        [Column("RequestDateTime", TypeName = "varchar(19)")]
        [JsonProperty(nameof(RequestDateTime))]
        [Display(Name = "Data wysłania żądania", Prompt = "Wpisz lub wybierz datę wysłania żądania", Description = "Data wysłania żądania")]
        [Required]
        [StringLength(19)]
        [MinLength(19)]
        [MaxLength(19)]
        public string RequestDateTime { get; set; }
        #endregion

        #region public DateTime? RequestDateTimeAsDate { get; set; }
        /// <summary>
        /// Data wysłania żądania w formacie datetime
        /// </summary>
        [Column("RequestDateTimeAsDate", TypeName = "datetime")]
        [JsonProperty(nameof(RequestDateTimeAsDate))]
        [Display(Name = "Data wysłania żądania", Prompt = "Wpisz lub wybierz datę wysłania żądania", Description = "Data wysłania żądania")]
        public DateTime? RequestDateTimeAsDate { get; set; }
        #endregion

        #region public string RequestId { get; set; }
        /// <summary>
        /// Numer (id) odpowiedzi jako string
        /// </summary>
        [Column("RequestId", TypeName = "varchar(18)")]
        [JsonProperty(nameof(RequestId))]
        [Display(Name = "Numer (id) odpowiedzi", Prompt = "Wpisz numer (id) odpowiedzi", Description = "Numer (id) odpowiedzi")]
        [Required]
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
