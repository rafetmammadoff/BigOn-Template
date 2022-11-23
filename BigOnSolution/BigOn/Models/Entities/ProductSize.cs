using BigOn.AppCode.Infrastructure;
using System.Threading.Tasks.Dataflow;

namespace BigOn.Models.Entities
{
    public class ProductSize:BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
