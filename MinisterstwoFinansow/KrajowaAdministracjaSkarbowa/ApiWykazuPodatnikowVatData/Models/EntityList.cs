#region using

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public class EntityList
    {
        public IEnumerable<Entity> Subjects { get; set; }

        public string RequestDateTime { get; set; }

        public string RequestId { get; set; }
    }
}
