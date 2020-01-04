using System.Threading.Tasks;

namespace AttendanceTracker.Repositories
{
    public interface IRepositoryWrapper
    {
        public IClassSessionRepository ClassSession { get; }
        public IStudentClassRepository StudentClass { get; }
        public IStudentRepository Student { get; }
        Task SaveAsync();
    }
}