﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navagition property for Product Table    
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();   // ICollection to use and dataStructure (List // hashset// Dictionary)
    }
}