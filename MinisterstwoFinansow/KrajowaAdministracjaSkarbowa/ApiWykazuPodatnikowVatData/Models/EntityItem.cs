using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public partial class EntityItem
    {
        public Entity Subject { get; set; }

        public string RequestDateTime { get; set; }

        public string RequestId { get; set; }
    }
}
