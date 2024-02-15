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
    public class CategoryRepo : GenericRepo<Category>, ICategory
    {
        private readonly ApplicationDbContext context;

        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
