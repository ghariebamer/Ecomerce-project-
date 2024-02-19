using AutoMapper;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;
using Microsoft.Extensions.FileProviders;
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
        private readonly IFileProvider fileProvider;
        private readonly IMapper mapper;

        public ICategory Category { get; set; }

        public IProduct Product { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext,IFileProvider fileProvider ,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.fileProvider = fileProvider;
            this.mapper = mapper;
            Category = new CategoryRepo(dbContext);
            Product= new ProductRepo(dbContext, fileProvider, mapper);
        }
    }
}
