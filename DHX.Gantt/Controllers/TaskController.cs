using Microsoft.AspNetCore.Mvc;
using DHX.Gantt.Models;
using Microsoft.EntityFrameworkCore;

namespace DHX.Gantt.Controllers
{
    [Produces("application/json")]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly GanttContext _context;
        public TaskController(GanttContext context)
        {
            _context = context;
        }

        // GET api/task
        [HttpGet]
        public async Task<IEnumerable<WebApiTask>> Get()
        {
            return await _context.Tasks
                .Select(t => (WebApiTask)t)
                .ToListAsync();
        }

        // GET api/task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> Get(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST api/task
        [HttpPost]
        public async Task<IActionResult> Post(WebApiTask apiTask)
        {
            var newTask = (Models.Task)apiTask;

            newTask.SortOrder = await _context.Tasks.MaxAsync(t => t.SortOrder) + 1;
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                tid = newTask.Id,
                action = "inserted"
            });
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public async Task<IActionResult?> Put(int id, WebApiTask apiTask)
        {
            var updatedTask = (Models.Task)apiTask;
            var dbTask = await _context.Tasks.FindAsync(id);

            if (dbTask == null)
            {
                return NotFound();
            }

            dbTask.Text = updatedTask.Text;
            dbTask.StartDate = updatedTask.StartDate;
            dbTask.Duration = updatedTask.Duration;
            dbTask.ParentId = updatedTask.ParentId;
            dbTask.Progress = updatedTask.Progress;
            dbTask.Type = updatedTask.Type;

            if (!string.IsNullOrEmpty(apiTask.target))
            {
                // reordering occurred                         
                await this.UpdateOrdersAsync(dbTask, apiTask.target);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }

        private async Task<IActionResult> UpdateOrdersAsync(Models.Task updatedTask, string orderTarget)
        {
            int adjacentTaskId;
            var nextSibling = false;

            var targetId = orderTarget;

            // adjacent task id is sent either as '{id}' or as 'next:{id}' depending 
            // on whether it's the next or the previous sibling
            if (targetId.StartsWith("next:"))
            {
                targetId = targetId.Replace("next:", "");
                nextSibling = true;
            }

            if (!int.TryParse(targetId, out adjacentTaskId))
            {
                return NotFound();
            }

            var adjacentTask = await _context.Tasks.FindAsync(adjacentTaskId);
            if (adjacentTask == null)
            {
                return NotFound();
            }
            var startOrder = adjacentTask.SortOrder;

            if (nextSibling)
                startOrder++;

            updatedTask.SortOrder = startOrder;

            var updateOrders = await _context.Tasks
                .Where(t => t.Id != updatedTask.Id)
                .Where(t => t.SortOrder >= startOrder)
                .OrderBy(t => t.SortOrder)
                .ToListAsync();

            var taskList = updateOrders.ToList();
            taskList.ForEach(t => t.SortOrder++);

            return Ok(new
            {
                action = "updated"
            });
        }
    }
}