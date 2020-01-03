using System.Linq;

namespace AttendanceTracker.Repositories
{
    public interface IRepositoryBase<T>
    {
        // IQueryable<T> GetAll();
        // IQueryable<T> Search();
        void Create(T entity);
        // void Update(T entity);
        // void Delete(T entity);
    }
}