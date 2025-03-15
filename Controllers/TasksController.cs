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

                if (obj.TaskDetail.Description == null)
                {
                    obj.TaskDetail.Description = "";
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

        // Called on View button
        [HttpGet]
        public IActionResult Edit(int? id)
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

            if (task.UserId.HasValue) // If UserId is provided, load the associated user
            {
                task.User = _db.Users.FirstOrDefault(u => u.Id == task.UserId.Value);
            }

            task.Tags = _db.Tasks
                .Where(t => t.Id == id)
                .SelectMany(t => t.Tags)
                .ToList();

            task.TaskDetail = _db.TaskDetails.FirstOrDefault(t => t.TaskId == id);

            ViewBag.Users = new SelectList(_db.Users,"Id", "Name"); // for drop down list of users
            ViewBag.Tags = new MultiSelectList(_db.Tags, "Id", "Name");
            ViewBag.SelectedTags = task.Tags.Select(tt => tt.Id).ToList(); // Preselected Tags

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(Task obj, List<int>? tagsIdList = null)
        {
            if (obj != null)
            {
                if (obj.UserId.HasValue) // If UserId is provided, load the associated user
                {
                    obj.User = _db.Users.FirstOrDefault(u => u.Id == obj.UserId.Value);
                }

                if (obj.TaskDetail.Description == null)
                {
                    obj.TaskDetail.Description = "";
                }


                // Get the list of selected tags
                var selectedTags = _db.Tags
                    .Where(t => tagsIdList.Contains(t.Id))
                    .ToList();

                /*
                    This existingTask is a tracked entity because you retrieved it using EF Core. 
                    Since weused _db.Tasks.Include(...), EF Core automatically tracks changes made to existingTask 
                    and calling SaveChanges() will persist those changes to the db without needing Update() or Add() call. 
                */
                var existingTask = _db.Tasks
                    .Include(t => t.Tags)
                    .Include(t => t.TaskDetail)
                    .Include(u => u.User)
                    .FirstOrDefault(t => t.Id == obj.Id);

                if (existingTask == null)
                {
                    return NotFound("Task not found.");
                }

                existingTask.Name = obj.Name;
                existingTask.Status = obj.Status;
                existingTask.TaskDetail = obj.TaskDetail;
                existingTask.User = obj.User;
                existingTask.Tags = selectedTags;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

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