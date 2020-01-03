using AttendanceTracker.Data;

namespace AttendanceTracker.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;
        private ClassSessionSessionRepository _classSessionSessionRepository;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IClassSessionRepository ClassSession
        {
            get
            {
                if (_classSessionSessionRepository == null)
                {
                    _classSessionSessionRepository = new ClassSessionSessionRepository(_context);
                }

                return _classSessionSessionRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}