using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class ApiWykazuPodatnikowVatDataEntityPesel, Model danych ApiWykazuPodatnikowVatDataEntityPesel, oryginalnie Entity
    /// <summary>
    /// Model danych ApiWykazuPodatnikowVatDataEntityPesel, oryginalnie Pesel
    /// </summary>
    [Table("ApiWykazuPodatnikowVatDataEntityPesel", Schema = "ApiWykazuPodatnikowVat")]
    public partial class ApiWykazuPodatnikowVatDataEntityPesel
    {
        #region public ApiWykazuPodatnikowVatDataEntityPesel()
        /// <summary>
        /// 
        /// </summary>
        public ApiWykazuPodatnikowVatDataEntityPesel()
        {
            ApiWykazuPodatnikowVatDataEntity = new HashSet<ApiWykazuPodatnikowVatDataEntity>();
            ApiWykazuPodatnikowVatDataEntityPerson = new HashSet<ApiWykazuPodatnikowVatDataEntityPerson>();
        }
        #endregion

        #region public Guid Id { get; set; }, identyfikator, klucz główny
        /// <summary>
        /// Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public string Pesel { get; set; }, Numer pesel
        /// <summary>
        /// Numer pesel
        /// </summary>
        [Column("Pesel", TypeName = "varchar(11)")]
        [Display(Name = "Numer pesel", Prompt = "Wpisz numer pesel", Description = "Numer pesel")]
        [Required]
        [StringLength(11)]
        [MinLength(11)]
        [MaxLength(11)]
        public string Pesel { get; set; }
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

        #region public virtual ICollection<ApiWykazuPodatnikowVatDataEntity> ApiWykazuPodatnikowVatDataEntity { get; set; }
        /// <summary>
        /// public virtual ICollection<ApiWykazuPodatnikowVatDataEntity> ApiWykazuPodatnikowVatDataEntity { get; set; }
        /// </summary>
        [InverseProperty("Pesel")]
        public virtual ICollection<ApiWykazuPodatnikowVatDataEntity> ApiWykazuPodatnikowVatDataEntity { get; set; }
        #endregion

        #region public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> ApiWykazuPodatnikowVatDataEntityPerson { get; set; }
        /// <summary>
        /// public virtual ICollection<ApiWykazuPodatnikowVatDataEntity> ApiWykazuPodatnikowVatDataEntityPerson { get; set; }
        /// </summary>
        [InverseProperty("Pesel")]
        public virtual ICollection<ApiWykazuPodatnikowVatDataEntityPerson> ApiWykazuPodatnikowVatDataEntityPerson { get; set; }
        #endregion
    }
    #endregion
}
