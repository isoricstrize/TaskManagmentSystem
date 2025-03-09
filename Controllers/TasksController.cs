using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Data;
using TaskManagmentSystem.Models;
using Task = TaskManagmentSystem.Models.Task;

namespace TaskManagmentSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskManagmentContext _db;

        public TasksController(TaskManagmentContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Task> tasks = _db.Tasks.ToList();

            return View(tasks);
        }

        // Called with "Create new task" button
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // on form submit in Create.cshtml
        public IActionResult Create(Task obj)
        {
            /*if (obj != null && obj.Name.Length < 3)
            {
                ModelState.AddModelError("Title","Title too short");
            }*/

            if (ModelState.IsValid)
            {
                _db.Tasks.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }    

            return View(obj);
        }
        
    }
}