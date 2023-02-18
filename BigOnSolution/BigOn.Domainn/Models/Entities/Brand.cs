using BigOn.Domain.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BigOn.Domain.Models.Entities
{
    public class Brand:BaseEntity
    {
        [Required(ErrorMessage ="{0} bos buraxila bilmez")]
        public string Name { get; set; }
    }
}
