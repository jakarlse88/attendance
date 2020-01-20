using System;
using System.Threading.Tasks;
using AttendanceTracker.Models.DTO;
using AttendanceTracker.Services;
using Microsoft.AspNetCore.Http;
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

        // GET: api/class_session
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var result = 
                    await _classSessionService
                        .GetByIdAsync(id.GetValueOrDefault());

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // POST: api/class_session
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([Bind]ClassSessionDto dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = 
                    await _classSessionService
                        .CreateAsync(dto);
                
                return CreatedAtAction(
                    "Get",
                    new {id = result.Id},
                    result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}