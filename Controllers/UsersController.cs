using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManagmentSystem.Data;
using TaskManagmentSystem.Models;
using Task = TaskManagmentSystem.Models.Task;

namespace TaskManagmentSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly TaskManagmentContext _db;

        public UsersController(TaskManagmentContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<User> users = _db.Users.ToList();

            return View(users);
        }

        // Called with "Create new task" button
        /*public IActionResult Create()
        {
            return View();
        }*/

        [HttpPost]
        public IActionResult Create(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists based on name
                var existingUser = _db.Users.FirstOrDefault(u => u.Name == newUser.Name);
                if (existingUser != null)
                {
                    // Add error to ModelState if user exists
                    ModelState.AddModelError("Name", "This user already exists.");
                    TempData["ErrorMessage"] = newUser.Name + " already exists. \n Please enter new user name.";
                    //return View(newUser); // Return the view with error
                    return RedirectToAction("Index");
                }

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            if (newUser.Name == null)
            {
                TempData["ErrorMessage"] = "User name is required.";
            } 
            else if (newUser.Name.Length < 3 || newUser.Name.Length > 30) 
            {
                TempData["ErrorMessage"] = "User name must be between 3 and 30 Characters.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var user = _db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _db.Users.Remove(user);
            _db.SaveChanges();
            
            return RedirectToAction("Index"); // Redirect after deletion
        }
    }
}