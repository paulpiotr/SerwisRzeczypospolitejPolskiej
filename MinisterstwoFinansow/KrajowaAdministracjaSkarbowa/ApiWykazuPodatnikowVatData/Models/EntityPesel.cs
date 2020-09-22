using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class EntityPesel, Model danych EntityPesel, oryginalnie Entity
    /// <summary>
    /// Model danych EntityPesel, oryginalnie Pesel
    /// </summary>
    [Table("EntityPesel", Schema = "ApiWykazuPodatnikowVat")]
    public partial class EntityPesel
    {
        #region public EntityPesel()
        /// <summary>
        /// 
        /// </summary>
        public EntityPesel()
        {
            Entity = new HashSet<Entity>();
            EntityPerson = new HashSet<EntityPerson>();
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

        #region public virtual ICollection<Entity> Entity { get; set; }
        /// <summary>
        /// public virtual ICollection<Entity> Entity { get; set; }
        /// </summary>
        [InverseProperty("Pesel")]
        public virtual ICollection<Entity> Entity { get; set; }
        #endregion

        #region public virtual ICollection<EntityPerson> EntityPerson { get; set; }
        /// <summary>
        /// public virtual ICollection<Entity> EntityPerson { get; set; }
        /// </summary>
        [InverseProperty("Pesel")]
        public virtual ICollection<EntityPerson> EntityPerson { get; set; }
        #endregion
    }
    #endregion
}
