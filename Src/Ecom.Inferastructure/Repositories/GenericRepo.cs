using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Inferastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity<int>

    {
 
        private readonly ApplicationDbContext context;
        public GenericRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<T> AddElement(T el)
        {
            await context.Set<T>().AddAsync(el);
            await context.SaveChangesAsync();
            return el;
        }
        public async Task DeleteElement(T el)
        {
             context.Set<T>().Remove(el);
            await context.SaveChangesAsync();
        }
        public IEnumerable<T> GetAll() => context.Set<T>().AsNoTracking().ToList();
        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] Includes)
        {
            var query = context.Set<T>().AsQueryable();
            if (Includes != null && Includes.Length > 0)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()=> await context.Set<T>().AsNoTracking().ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] Includes)
        {
            var query=context.Set<T>().AsQueryable();
            if(Includes != null&&Includes.Length>0)
            {
                foreach(var include in Includes)
                {
                    query=query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
          T result= await context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] Includes)
        {
            var query= context.Set<T>().AsQueryable().Where(e=>e.Id==id);
            if(Includes !=null && Includes.Length > 0)
            {
                foreach (var item in Includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();  // casting to DBSet for find 
        }

        public async Task UpdateElement(T el,int id)
        {
            var currentEntity=context.Set<T>().Find(id);
            if(currentEntity != null)
            {
                context.Update(currentEntity);
                await context.SaveChangesAsync();
            }
        }
    
         public int GetTotalCount (IReadOnlyList<T> values)
        {
            return values.Count();
        }
    }
}
