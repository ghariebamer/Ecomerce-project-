using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Inferastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProduct
    {
        private readonly ApplicationDbContext context;

        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
