using AttendanceTracker.Data;
using AttendanceTracker.Models;

namespace AttendanceTracker.Repositories
{
    public class StudentClassRepository : RepositoryBase<StudentClass>, IStudentClassRepository
    {
        public StudentClassRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}