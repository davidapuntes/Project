﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presupuestea.Data.Model
{
    public class Category
    {
        
        [Required]
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public virtual ICollection<Contractor> Freelancers { get; set; }

    }
}
