using System.Threading.Tasks;
using AttendanceTracker.Models;
using AttendanceTracker.Models.DTO;

namespace AttendanceTracker.Services
{
    public interface IClassSessionService
    {
        Task<ClassSession> CreateAsync(ClassSessionDto dto);
        Task<ClassSession> GetByIdAsync(int id);
        Task<ClassSession> EditAsync(int id, ClassSessionDto dto);
    }
}