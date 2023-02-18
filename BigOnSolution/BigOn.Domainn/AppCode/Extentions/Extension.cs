using BigOn.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.AppCode.Extentions
{
    public static partial class Extension
    {
        public static IEnumerable<Category> GetAllChildren(this Category parent)
        {
            if(parent.ParentId!= null)
                yield return parent;
            foreach (var item in parent.Children.SelectMany(c=>c.GetAllChildren()))
            {
                yield return item;
            }
        }
    }
}
