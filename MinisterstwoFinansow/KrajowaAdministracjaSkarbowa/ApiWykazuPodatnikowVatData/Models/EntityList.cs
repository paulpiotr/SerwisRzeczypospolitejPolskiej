using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public partial class EntityList
    {
        public IEnumerable<Entity> Subjects { get; set; }

        public string RequestDateTime { get; set; }

        public string RequestId { get; set; }
    }
}
