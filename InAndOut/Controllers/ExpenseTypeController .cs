using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get List
        public IActionResult Index()
        {
            var list = _context.ExpenseTypes.ToList();
            return View(list);
        }

        // Get Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseType expenses)
        {
            if (ModelState.IsValid)
            {
                
                _context.ExpenseTypes.Add(expenses);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenses);
        }

        // Get-Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.ExpenseTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseType model)
        {
            if (ModelState.IsValid)
            {
                _context.ExpenseTypes.Update(model);
                _context.SaveChanges();

                return RedirectToAction();
            }

            return View(model);
        }

        // Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.ExpenseTypes.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost (int? id)
        {
            var exp = _context.ExpenseTypes.Find(id);
            if (exp == null)
            {
                return NotFound();
            }
            _context.ExpenseTypes.Remove(exp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
