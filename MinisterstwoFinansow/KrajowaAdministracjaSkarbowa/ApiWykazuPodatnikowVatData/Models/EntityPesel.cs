#region using

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public class EntityPesel

    /// <summary>
    ///     Model danych EntityPesel, oryginalnie Pesel
    /// </summary>
    [NotMapped]
    [Table("EntityPesel", Schema = "awpv")]
    public class EntityPesel
    {
        #region public Guid Id { get; set; }

        /// <summary>
        ///     Guid Id identyfikator, klucz główny
        /// </summary>
        [Key]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator", Description = "Identyfikator klucz główny")]
        public Guid Id { get; set; }

        #endregion

        #region public string UniqueIdentifierOfTheLoggedInUser { get; set; }

        /// <summary>
        ///     Jednoznaczny identyfikator zalogowanego użytkownika
        ///     Unique identifier of the logged in user
        /// </summary>
        [Column("UniqueIdentifierOfTheLoggedInUser", TypeName = "varchar(512)")]
        [Display(Name = "Identyfikator zalogowanego użytkownika",
            Prompt = "Wybierz identyfikator zalogowanego użytkownika",
            Description = "Identyfikator zalogowanego użytkownika")]
        [StringLength(512)]
        [Required]
        public string UniqueIdentifierOfTheLoggedInUser { get; set; }

        #endregion

        #region public string Pesel { get; set; }

        /// <summary>
        ///     Numer pesel
        /// </summary>
        [Column("Pesel", TypeName = "varchar(11)")]
        [Display(Name = "Numer pesel", Prompt = "Wpisz numer pesel", Description = "Numer pesel")]
        [Required]
        [StringLength(11)]
        [MinLength(11)]
        [MaxLength(11)]
        public string Pesel { get; set; }

        #endregion

        #region public DateTime DateOfCreate { get; set; }

        /// <summary>
        ///     Data utworzenia
        /// </summary>
        [Column("DateOfCreate", TypeName = "datetime")]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia",
            Description = "Data utworzenia")]
        public DateTime DateOfCreate { get; set; }

        #endregion

        #region public DateTime? DateOfModification { get; set; }

        /// <summary>
        ///     Data modyfikacji
        /// </summary>
        [Column("DateOfModification", TypeName = "datetime")]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji",
            Description = "Data modyfikacji")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfModification { get; set; }

        #endregion

        #region public EntityPesel()

        #endregion

        //#region public virtual ICollection<Entity> Entity { get; set; }
        ///// <summary>
        ///// public virtual ICollection<Entity> Entity { get; set; }
        ///// </summary>
        //[InverseProperty("Pesel")]
        //public virtual ICollection<Entity> Entity { get; set; }
        //#endregion

        //#region public virtual ICollection<EntityPerson> EntityPerson { get; set; }
        ///// <summary>
        ///// public virtual ICollection<Entity> EntityPerson { get; set; }
        ///// </summary>
        //[InverseProperty("Pesel")]
        //public virtual ICollection<EntityPerson> EntityPerson { get; set; }
        //#endregion
    }

    #endregion
}
