
using Early_warning.Contexts;
using Early_warning.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Early_warning
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloodEventsController : ControllerBase
    {
        private readonly FloodContext _context;

        public FloodEventsController(FloodContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FloodEvent>>> GetFloodEvents()
        {
            return await _context.FloodEvents.Include(f => f.Location).Include(f => f.Severity).Include(f => f.Cause).ToListAsync();
        }
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<FloodEventDto>>> GetFloodEventSummaries()
        {
            var events = await _context.FloodEvents
                .Select(f => new FloodEventDto
                {
                    City = f.Location.City,
                    Latitude = f.Location.Latitude,
                    Longitude = f.Location.Longitude,
                    Impact = f.Impact,
                    Year = f.Year,
                    Cause = f.Cause.Cause,
                    Level = f.Severity.Level
                })
                .ToListAsync();

            return Ok(events);
        }



        [HttpPost]
        public async Task<ActionResult<FloodEvent>> PostFloodEvent(FloodEvent floodEvent)
        {
            _context.FloodEvents.Add(floodEvent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFloodEvents), new { id = floodEvent.Id }, floodEvent);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FloodEvent>>> SearchFloodEvents([FromQuery] int? locationId, [FromQuery] string city)
        {
            var query = _context.FloodEvents
                .Include(f => f.Location)
                .Include(f => f.Severity)
                .Include(f => f.Cause)
                .AsQueryable();

            //if (locationId.HasValue)
            //{
            //    query = query.Where(f => f.LocationId == locationId);
            //}

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(f => f.Location.City == city);
            }

            return await query.ToListAsync();
        }
    }
}
