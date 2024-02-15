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
    public class UnitOfWork : IUnitOfWork
    {
        //unit of work is main class for project 

        private readonly ApplicationDbContext dbContext;

        public ICategory Category { get; set; }

        public IProduct Product { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Category = new CategoryRepo(dbContext);
            Product= new ProductRepo(dbContext);
        }
    }
}
