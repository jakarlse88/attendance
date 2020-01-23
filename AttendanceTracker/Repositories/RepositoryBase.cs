using System;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracker.Data;
using AttendanceTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AttendanceTracker.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly ApplicationDbContext _context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // public IEnumerable<T> GetAll()
        // {
        //     throw new System.NotImplementedException();
        // }
        
        public async Task<T> GetByIdAsync(int id)
        {
            var result = 
                await _context
                    .Set<T>()
                    .Where(
                        x => x.Id == id)
                    .FirstOrDefaultAsync();

            return result;
        }

        public EntityEntry<T> GetEntityEntry(T entity)
        {
            if (entity == null)
                throw new NullReferenceException("Cannot get EntityEntry of a null entity.");

            var result = _context.Entry(entity);

            return result;
        }

        public void Add(T entity)
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
        
        // public void Delete(T entity)
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}