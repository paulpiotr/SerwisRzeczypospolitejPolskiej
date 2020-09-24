using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public partial class EntityResponse
    {
        public EntityItem Result { get; set; }
    }
}
