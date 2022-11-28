using BigOn.AppCode.Infrastructure;
using System;

namespace BigOn.Models.Entities
{
    public class Subscribe:BaseEntity
    {
        public string Email { get; set; }
        public DateTime? AprovedDate { get; set; }

    }
}
