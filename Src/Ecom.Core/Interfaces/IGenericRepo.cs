using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
         Task<IReadOnlyList<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, Object>> [] Includes);
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, Object>>[] Includes);// params dataType [] 
        Task<T> GetById(int id);
        Task<T> GetByIdAsync(int id,params Expression< Func<T, object> >[] Includes);
        Task<T> AddElement(T el);
        Task UpdateElement(T el,int id);
        Task DeleteElement(T el);
    }
}
