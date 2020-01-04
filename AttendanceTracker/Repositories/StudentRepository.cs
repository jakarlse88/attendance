using AttendanceTracker.Data;
using AttendanceTracker.Models;

namespace AttendanceTracker.Repositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context)
            : base(context)
        {
        }   
    }
}