using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using DHX.Gantt.Models;

namespace DHX.Gantt.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : Controller
    {
        private readonly GanttContext _context;
        public DataController(GanttContext context)
        {
            _context = context;
        }

        // GET api/data
        [HttpGet]
        public object Get()
        {
            return new
            {
                data = _context.Tasks
                    .OrderBy(t => t.SortOrder)
                    .ToList()
                    .Select(t => (WebApiTask)t),
                links = _context.Links
                    .ToList()
                    .Select(l => (WebApiLink)l)

            };
        }
        
    }
}