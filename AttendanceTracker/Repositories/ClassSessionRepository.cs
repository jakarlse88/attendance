using AttendanceTracker.Data;
using AttendanceTracker.Models;

namespace AttendanceTracker.Repositories
{
    public class ClassSessionRepository : RepositoryBase<ClassSession>, IClassSessionRepository
    {
        public ClassSessionRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}