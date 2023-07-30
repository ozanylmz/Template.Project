using System.Linq.Expressions;
using Template.Project.Domain.SeedWork;

namespace Template.Project.Domain.Interfaces
{
    public interface IBaseRepository<T>  where T : MongoBaseEntity
    {
        Task<List<T>> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(string id, T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(string id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
