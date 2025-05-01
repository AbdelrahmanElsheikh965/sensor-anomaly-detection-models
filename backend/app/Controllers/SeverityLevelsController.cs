
using Early_warning.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Early_warning
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeverityLevelsController : ControllerBase
    {
        private readonly FloodContext _context;

        public SeverityLevelsController(FloodContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeverityLevel>>> GetSeverityLevels()
        {
            return await _context.SeverityLevels.ToListAsync();
        }
    }
}
