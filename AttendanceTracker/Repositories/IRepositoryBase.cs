using System.Threading.Tasks;
using AttendanceTracker.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AttendanceTracker.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        // IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        EntityEntry<T> GetEntityEntry(T entity);
        void Add(T entity);
        // void Update(T entity);
        // void Delete(T entity);
    }
}