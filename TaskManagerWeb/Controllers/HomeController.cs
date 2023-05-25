using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagerWeb.Models;
using TaskManagerWeb.EFModels;

namespace TaskManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskManagerDBContext _context;

        public HomeController(ILogger<HomeController> logger, TaskManagerDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            UserTaskList userTaskList = new UserTaskList()
            {
                Users = _context.Users.ToList(),
                UserTask = _context.Tasks.Where(t => _context.TaskAssignments.Any(ta => t.Id == ta.TaskId) == false).ToList()
            };

            return View(userTaskList);
        }

        [HttpPost]
        public ActionResult<TaskAssignment> SaveTaskAssigment(int taskId, int userId, bool isDone)
        {
            if (taskId == 0 || userId == 0)
            {
                return RedirectToAction("Index");
            }

            TaskAssignment taskAssignment = new TaskAssignment()
            {
                TaskId = taskId,
                UserId = userId,
                IsDone = isDone
            };

            _context.TaskAssignments.Add(taskAssignment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}