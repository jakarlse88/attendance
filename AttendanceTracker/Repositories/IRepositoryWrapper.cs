namespace AttendanceTracker.Repositories
{
    public interface IRepositoryWrapper
    {
        public IClassSessionRepository ClassSession { get; }
        void Save();
    }
}