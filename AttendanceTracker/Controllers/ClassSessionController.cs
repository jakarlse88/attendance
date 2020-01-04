using System;
using System.Threading.Tasks;
using AttendanceTracker.Models.DTO;
using AttendanceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Controllers
{
    [ApiController]
    [Route("api/class_session")]
    public class ClassSessionController : ControllerBase
    {
        private readonly IClassSessionService _classSessionService;

        public ClassSessionController(IClassSessionService classSessionService)
        {
            _classSessionService = classSessionService;
        }
        
        // POST: api/ClassSession
        [HttpPost]
        public async Task<IActionResult> Post([Bind]ClassSessionDto dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _classSessionService.Create(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}