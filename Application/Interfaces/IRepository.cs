using System.Linq.Expressions;

namespace CleaHub.Application.Interfaces;

public interface IRepository<T, TId> where T : class
{
    Task<T?> GetByIdAsync(TId id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
/*
GetByIdAsync, GetAllAsync, FindAsync: okuma işlemleri.

AddAsync, UpdateAsync, DeleteAsync: yazma işlemleri.

Expression<Func<T, bool>> predicate: LINQ ile filtreleme yapabilmek için.

Task kullanarak async/await desteği sağladık.
*/