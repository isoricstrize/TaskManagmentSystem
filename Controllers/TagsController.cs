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
        /*public IActionResult Create()
        {
            return View();
        }*/

        [HttpPost]
        public IActionResult Create(Tag newTag)
        {
            if (ModelState.IsValid)
            {
                // Check if the tag already exists based on name
                var existingTag = _db.Tags.FirstOrDefault(t => t.Name == newTag.Name);
                if (existingTag != null)
                {
                    TempData["ErrorMessage"] = newTag.Name + " already exists. \n Please enter new tag name.";
                    return RedirectToAction("Index");
                }

                _db.Tags.Add(newTag);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            if (newTag.Name == null)
            {
                TempData["ErrorMessage"] = "Tag name is required.";
            } 
            else if (newTag.Name.Length < 3 || newTag.Name.Length > 30) 
            {
                TempData["ErrorMessage"] = "Tag name must be between 3 and 30 Characters.";
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

            var tag = _db.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            _db.Tags.Remove(tag);
            _db.SaveChanges();
            
            return RedirectToAction("Index"); // Redirect after deletion
        }


    }
}