using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DHX.Gantt.Models;

namespace DHX.Gantt.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly GanttContext _context;
        public DataController(GanttContext context)
        {
            _context = context;
        }

        // GET api/data
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tasks = await _context.Tasks
                .OrderBy(t => t.SortOrder)
                .Select(t => (WebApiTask)t)
                .ToListAsync();

            var links = await _context.Links
                .Select(l => (WebApiLink)l)
                .ToListAsync();

            return Ok(new
            {
                tasks,
                links
            });
        }

    }
}