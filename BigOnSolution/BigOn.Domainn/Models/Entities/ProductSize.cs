using BigOn.Domain.AppCode.Infrastructure;
using System.Threading.Tasks.Dataflow;

namespace BigOn.Domain.Models.Entities
{
    public class ProductSize:BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
