using System.Linq;
using AttendanceTracker.Data;

namespace AttendanceTracker.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // public IQueryable<T> GetAll()
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public IQueryable<T> Search()
        // {
        //     throw new System.NotImplementedException();
        // }

        public void Create(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Add(entity);
            }
        }

        // public void Update(T entity)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void Delete(T entity)
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}