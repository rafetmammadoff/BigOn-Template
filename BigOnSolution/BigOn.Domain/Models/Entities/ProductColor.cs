using BigOn.Domain.AppCode.Infrastructure;

namespace BigOn.Domain.Models.Entities
{
    public class ProductColor: BaseEntity
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
