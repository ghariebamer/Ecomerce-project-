using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public  class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        // Navagition property for categories Table
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
