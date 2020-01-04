using System.Threading.Tasks;
using AttendanceTracker.Data;

namespace AttendanceTracker.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;
        private IClassSessionRepository _classSessionRepository;
        private IStudentClassRepository _studentClassRepository;
        private IStudentRepository _studentRepository;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IClassSessionRepository ClassSession =>
            _classSessionRepository ??=
                new ClassSessionRepository(_context);

        public IStudentClassRepository StudentClass =>
            _studentClassRepository ??=
                new StudentClassRepository(_context);

        public IStudentRepository Student =>
            _studentRepository ??=
                new StudentRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}