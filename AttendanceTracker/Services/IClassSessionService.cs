using System.Threading.Tasks;
using AttendanceTracker.Models;
using AttendanceTracker.Models.DTO;

namespace AttendanceTracker.Services
{
    public interface IClassSessionService
    {
        Task<ClassSession> CreateAsync(ClassSessionDto dto);
        Task<ClassSession> GetByIdAsync(int id);
        Task<ClassSession> UpdateAsync(int id, ClassSessionDto dto);
    }
}