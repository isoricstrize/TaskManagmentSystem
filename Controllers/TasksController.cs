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
                .ToList();

            return View(tasks);
        }

        // Called with "Create new task" button
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_db.Users,"Id", "Name"); // for drop down list of users
            ViewBag.Tags = _db.Tags.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return View();
        }

        [HttpPost] // on form submit in Create.cshtml 
        public IActionResult Create(Task obj, List<int>? tagsIdList = null) // tagsIdList same as name attribute in select!
        {   
            if (obj != null)
            {
                if (obj.UserId.HasValue) // If UserId is provided, load the associated user
                {
                    obj.User = _db.Users.FirstOrDefault(u => u.Id == obj.UserId.Value);
                }

                if (tagsIdList != null)
                {
                    obj.Tags = _db.Tags.Where(t => tagsIdList.Contains(t.Id)).ToList();
                }
            }


            if (ModelState.IsValid)
            {
                _db.Tasks.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }    

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Log error messages
            }

            ViewBag.Users = new SelectList(_db.Users,"Id", "Name"); // Fix for bug: After User validation error ViewBag is empty
            ViewBag.Tags = _db.Tags.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(obj);
        }

        // Called with task delete symbol
        /*[HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Task? task= _db.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(Task obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }    

            return View(obj);
        }*/


        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(task);
            _db.SaveChanges();
            
            return RedirectToAction("Index"); // Redirect after deletion
        }


        
    }
}