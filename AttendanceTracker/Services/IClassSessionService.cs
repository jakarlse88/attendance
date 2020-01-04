using System.Threading.Tasks;
using AttendanceTracker.Models;
using AttendanceTracker.Models.DTO;

namespace AttendanceTracker.Services
{
    public interface IClassSessionService
    {
        Task<ClassSession> Create(ClassSessionDto dto);
    }
}