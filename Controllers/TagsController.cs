using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Data;
using TaskManagmentSystem.Models;

namespace TaskManagmentSystem.Controllers
{
    public class TagsController: Controller
    {
        private readonly TaskManagmentContext _db;

        public TagsController(TaskManagmentContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Tag> tags = _db.Tags.ToList();

            return View(tags);
        }

        // Called with "Create new task" button
        public IActionResult Create()
        {
            return View();
        }
    }
}