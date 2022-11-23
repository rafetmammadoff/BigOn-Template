using BigOn.AppCode.Infrastructure;

namespace BigOn.Models.Entities
{
    public class ProductColor: BaseEntity
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
