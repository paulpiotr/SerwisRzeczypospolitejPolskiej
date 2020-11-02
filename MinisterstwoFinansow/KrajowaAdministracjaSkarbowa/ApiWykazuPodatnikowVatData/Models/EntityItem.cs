using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class EntityItem
    /// <summary>
    /// Klasa zawierająca odpowiedź żądania dotyczącego wyszukiwania podmiotu Entity
    /// Class containing the response of the Entity lookup request
    /// </summary>
    [NotMapped]
    public partial class EntityItem
    {
        #region public EntityItem()
        /// <summary>
        /// Konstruktor
        /// Constructor
        /// </summary>
        public EntityItem()
        {
        }
        #endregion

        #region public Entity Subject { get; set; }
        /// <summary>
        /// Obiekt Subject zawierający dane Entity
        /// The Subject object containing the Entity data
        /// </summary>
        [NotMapped]
        public Entity Subject { get; set; }
        #endregion

        #region public string RequestDateTime { get; set; }
        /// <summary>
        /// Data wysłania żądania
        /// Date the request was sent
        /// </summary>
        [JsonProperty(nameof(RequestDateTime))]
        [Display(Name = "Data wysłania żądania", Prompt = "Wpisz lub wybierz datę wysłania żądania", Description = "Data wysłania żądania")]
        [StringLength(32)]
        [Required]
        public string RequestDateTime { get; set; }
        #endregion

        #region public string RequestId { get; set; }
        /// <summary>
        /// Identyfikator żądania
        /// Identyfikator żądania
        /// </summary>
        [JsonProperty(nameof(RequestId))]
        [Display(Name = "Identyfikator żądania", Prompt = "Wpisz identyfikator żądania", Description = "Identyfikator żądania")]
        [StringLength(32)]
        [Required]
        public string RequestId { get; set; }
        #endregion
    }
    #endregion
}
