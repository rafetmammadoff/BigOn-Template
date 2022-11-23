using BigOn.AppCode.Infrastructure;
using System.Collections;
using System.Collections.Generic;

namespace BigOn.Models.Entities
{
    public class Category :BaseEntity
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }
}
