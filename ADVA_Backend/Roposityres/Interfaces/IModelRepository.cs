
using System.Linq.Expressions;


namespace Roposityres.Interfaces
{
    public interface IModelRepository<T>
    {
        //This method if i want to include data from another table
        Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        //Search with Query 
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        //This method if i want to include data from another table like including 
        Task<T> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includeProperties);

        Task<IQueryable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> RemoveAsync(T entity);


    }
}
