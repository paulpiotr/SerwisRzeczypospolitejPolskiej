#region using

using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ApiWykazuPodatnikowVatData.Models
{
    [NotMapped]
    public class EntityResponse
    {
        public EntityItem Result { get; set; }
    }
}
