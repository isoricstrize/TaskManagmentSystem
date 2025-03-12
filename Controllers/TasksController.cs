using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            List<Task> tasks = _db.Tasks
                .Include(d => d.TaskDetail)
                .Include(u => u.User)
                .Include(t => t.Tags)
                    //.ThenInclude(t => t.Name)
                .ToList();

            return View(tasks);
        }

        // Called with "Create new task" button
        public IActionResult Create()
        {
            //ViewBag.Users = new SelectList(_db.Users,"Id", "Name");
            ViewBag.Users = _db.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),  // This is the value submitted
                    Text = u.Name              // This is displayed in the dropdown
                })
                .ToList();

            return View();
        }

        [HttpPost] // on form submit in Create.cshtml
        public IActionResult Create(Task obj)
        {
            if (!ModelState.IsValid) // Fix for bug: After User validation error ViewBag is empty
            {
                ViewBag.Users = _db.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),  
                    Text = u.Name             
                })
                .ToList();

                return View(obj);
            }  
            
            if (obj != null)
            {
                obj.Tags = obj.Tags.Where(t => !string.IsNullOrEmpty(t.Name)).ToList(); // removing empty tags from tags list
            }

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