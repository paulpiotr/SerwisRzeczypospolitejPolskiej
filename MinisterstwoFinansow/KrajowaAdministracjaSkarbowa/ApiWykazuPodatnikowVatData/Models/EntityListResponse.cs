#region using

using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public class EntityListResponse
    {
        public EntityList Result { get; set; }
    }
}
