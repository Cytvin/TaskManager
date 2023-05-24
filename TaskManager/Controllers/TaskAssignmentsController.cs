using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.EFModels;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly TaskManagerDBContext _context;

        public TaskAssignmentsController(TaskManagerDBContext context)
        {
            _context = context;
        }

        // GET: api/TaskAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskAssignment>>> GetTaskAssignments()
        {
          if (_context.TaskAssignments == null)
          {
              return NotFound();
          }
            return await _context.TaskAssignments.ToListAsync();
        }

        // GET: api/TaskAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTaskAssignment(int id)
        {
            if (_context.TaskAssignments == null)
            {
                return NotFound();
            }

            var taskAssignments = _context.TaskAssignments.Where(t => t.UserId == id).Join(_context.Tasks, 
                ta => ta.TaskId, 
                t => t.Id, 
                (ta, t) => new
                {
                    t.Id,
                    t.Name,
                    t.Description,
                    t.Location,
                    ta.IsDone
                }).ToList();

            if (taskAssignments == null)
            {
                return NotFound();
            }

            return taskAssignments;
        }

        // PUT: api/TaskAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAssignment(int id, TaskAssignment taskAssignment)
        {
            if (id != taskAssignment.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(taskAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskAssignment>> PostTaskAssignment(TaskAssignment taskAssignment)
        {
          if (_context.TaskAssignments == null)
          {
              return Problem("Entity set 'TaskManagerDBContext.TaskAssignments'  is null.");
          }
            _context.TaskAssignments.Add(taskAssignment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskAssignmentExists(taskAssignment.TaskId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTaskAssignment", new { id = taskAssignment.TaskId }, taskAssignment);
        }

        // DELETE: api/TaskAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id)
        {
            if (_context.TaskAssignments == null)
            {
                return NotFound();
            }
            var taskAssignment = await _context.TaskAssignments.FindAsync(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            _context.TaskAssignments.Remove(taskAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskAssignmentExists(int id)
        {
            return (_context.TaskAssignments?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
