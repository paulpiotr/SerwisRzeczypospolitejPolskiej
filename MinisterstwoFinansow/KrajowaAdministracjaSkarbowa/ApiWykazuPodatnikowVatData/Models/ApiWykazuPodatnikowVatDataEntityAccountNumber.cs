using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class ApiWykazuPodatnikowVatDataEntityAccountNumber
    /// <summary>
    /// Model danych ApiWykazuPodatnikowVatDataEntityAccountNumber
    /// </summary>
    [Table("ApiWykazuPodatnikowVatDataEntityAccountNumber", Schema = "ApiWykazuPodatnikowVat")]
    public partial class ApiWykazuPodatnikowVatDataEntityAccountNumber
    {
        #region public Guid Id { get; set; }
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public Guid? ApiWykazuPodatnikowVatDataEntityId { get; set; }
        /// <summary>
        /// Odniesienie (klucz obcy) do tabeli ApiWykazuPodatnikowVatDataEntity jako Guid?
        /// </summary>
        public Guid? ApiWykazuPodatnikowVatDataEntityId { get; set; }
        #endregion

        #region public virtual ApiWykazuPodatnikowVatDataEntity ApiWykazuPodatnikowVatDataEntity { get; set; }
        /// <summary>
        /// Kolekcja objektów tabeli ApiWykazuPodatnikowVatDataEntity
        /// </summary>
        [ForeignKey(nameof(ApiWykazuPodatnikowVatDataEntityId))]
        [InverseProperty("ApiWykazuPodatnikowVatDataEntityAccountNumber")]
        public virtual ApiWykazuPodatnikowVatDataEntity ApiWykazuPodatnikowVatDataEntity { get; set; }
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
