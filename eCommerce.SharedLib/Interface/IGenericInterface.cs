
using System.Linq.Expressions;
using eCommerce.SharedLib.ResponseT;
namespace eCommerce.SharedLib.Interface
{
    public  interface IGenericInterface<T> where T : class
    {
        Task<Response> CreateAsync(T entity);
        Task<Response> UpdateAsync(T entity);
        Task<Response> DeleteAsync(T entity);
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByAsync(Expression<Func<T,bool>> predicate);
    }
}
