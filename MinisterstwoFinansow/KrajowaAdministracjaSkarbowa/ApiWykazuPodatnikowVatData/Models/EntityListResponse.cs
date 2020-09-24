using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public partial class EntityListResponse
    {
        public EntityList Result { get; set; }
    }
}
