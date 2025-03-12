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
        public IActionResult Create()
        {
            return View();
        }
    }
}