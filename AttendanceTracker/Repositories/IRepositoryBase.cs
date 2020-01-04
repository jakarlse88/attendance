using System.Threading.Tasks;

namespace AttendanceTracker.Repositories
{
    public interface IRepositoryBase<T>
    {
        // IQueryable<T> GetAll();
        Task<T> SearchByIdAsync(int id);
        void Add(T entity);
        // void Update(T entity);
        // void Delete(T entity);
    }
}