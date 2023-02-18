using BigOn.Domain.AppCode.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigOn.Domain.Models.Entities
{
    public class Category :BaseEntity
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }

        [NotMapped]
        public string ParentName { get { return Parent?.Name; } }
    }
}
