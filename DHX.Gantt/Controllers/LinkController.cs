using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DHX.Gantt.Models;

namespace DHX.Gantt.Controllers
{
    [Produces("application/json")]
    [Route("api/link")]
    public class LinkController : ControllerBase
    {
        private readonly GanttContext _context;
        public LinkController(GanttContext context)
        {
            _context = context;
        }

        // GET api/Link
        [HttpGet]
        public async Task<IEnumerable<WebApiLink>> Get()
        {
            return await _context.Links
                .Select(t => (WebApiLink)t)
                .ToListAsync();
        }

        // GET api/Link/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Link>> Get(int id)
        {
            var link = await _context.Links.FindAsync(id);

            if (link == null)
                return NotFound();

            return Ok(link);
        }

        // POST api/Link
        [HttpPost]
        public async Task<IActionResult> Post(WebApiLink apiLink)
        {
            var newLink = (Link)apiLink;

            _context.Links.Add(newLink);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                tid = newLink.Id,
                action = "inserted"
            });
        }

        // PUT api/Link/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, WebApiLink apiLink)
        {
            var updatedLink = (Link)apiLink;
            updatedLink.Id = id;
            _context.Entry(updatedLink).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/Link/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var link = await _context.Links.FindAsync(id);
            if (link != null)
            {
                _context.Links.Remove(link);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}