using System.Threading.Tasks;

namespace AttendanceTracker.Repositories
{
    public interface IRepositoryBase<T>
    {
        // IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        // void Update(T entity);
        // void Delete(T entity);
    }
}