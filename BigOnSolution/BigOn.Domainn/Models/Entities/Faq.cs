﻿using BigOn.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.Entities
{
    public class Faq:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
