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
                    TempData["ErrorMsgNewUser"] = newUser.Name + " already exists. \n Please enter new user name.";
                    return RedirectToAction("Index");
                }

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            if (newUser.Name == null)
            {
                TempData["ErrorMsgNewUser"] = "User name is required.";
            } 
            else if (newUser.Name.Length < 3 || newUser.Name.Length > 30) 
            {
                TempData["ErrorMsgNewUser"] = "User name must be between 3 and 30 Characters.";
            }

            return RedirectToAction("Index");
        }
        

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
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


        [HttpPost]
        public IActionResult Edit(int id, string newUserName)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var user = _db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            if (newUserName == null)
            {
                TempData["ErrorMsgEditUser"] = "User name is required.";
                return RedirectToAction("Index");
            } 
            else if (newUserName.Length < 3 || newUserName.Length > 30) 
            {
                TempData["ErrorMsgEditUser"] = "User name must be between 3 and 30 Characters.";
                return RedirectToAction("Index");
            }

            // Check if the user already exists based on name
            var existingUser = _db.Users.FirstOrDefault(u => u.Name == newUserName && u.Id != id);
            if (existingUser != null)
            {
                TempData["ErrorMsgEditUser"] = newUserName + " already exists. \n Please enter new user name.";
                return RedirectToAction("Index");
            }

            user.Name = newUserName;
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}