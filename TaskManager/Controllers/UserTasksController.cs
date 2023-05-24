using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.EFModels;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTasksController : ControllerBase
    {
        private readonly TaskManagerDBContext _context;

        public UserTasksController(TaskManagerDBContext context)
        {
            _context = context;
        }

        // GET: api/UserTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetTasks()
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }

            return await _context.Tasks.ToListAsync();
        }

        // GET: api/UserTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTask>> GetUserTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            
            var userTask = await _context.Tasks.FindAsync(id);

            if (userTask == null)
            {
                return NotFound();
            }

            return userTask;
        }

        // PUT: api/UserTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTask(int id, UserTask userTask)
        {
            if (id != userTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(userTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTaskExists(id))
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

        // POST: api/UserTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTask>> PostUserTask(UserTask userTask)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'TaskManagerDBContext.Tasks'  is null.");
            }
            
            _context.Tasks.Add(userTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTask", new { id = userTask.Id }, userTask);
        }

        // DELETE: api/UserTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }

            var userTask = await _context.Tasks.FindAsync(id);
            if (userTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(userTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTaskExists(int id)
        {
            return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
