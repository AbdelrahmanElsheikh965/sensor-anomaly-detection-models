
using Early_warning.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Early_warning
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloodCausesController : ControllerBase
    {
        private readonly FloodContext _context;

        public FloodCausesController(FloodContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FloodCause>>> GetFloodCauses()
        {
            return await _context.FloodCauses.ToListAsync();
        }
    }
}
