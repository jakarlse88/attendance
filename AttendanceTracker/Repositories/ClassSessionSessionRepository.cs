using AttendanceTracker.Data;
using AttendanceTracker.Models;

namespace AttendanceTracker.Repositories
{
    public class ClassSessionSessionRepository : RepositoryBase<ClassSession>, IClassSessionRepository
    {
        public ClassSessionSessionRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}