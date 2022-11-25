using BigOn.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BigOn.Models.Entities
{
    public class Brand:BaseEntity
    {
        [Required(ErrorMessage ="{0} bos buraxila bilmez")]
        public string Name { get; set; }
    }
}
