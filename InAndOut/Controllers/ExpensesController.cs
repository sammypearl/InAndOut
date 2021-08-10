using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get List
        public IActionResult Index()
        {
            IEnumerable<Expense> list = _context.Expenses;

            foreach(var model in list)
            {
                model.ExpenseType = _context.ExpenseTypes
                    .FirstOrDefault(x => x.Id == model.ExpenseTypeId);
            }
            return View(list);
        }

        // Get Create
        [HttpGet]
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown =
            //   _context.ExpenseTypes.Select(x => new SelectListItem
            //   {
            //       Text = x.Name,
            //       Value = x.Id.ToString()
            //   });

            //ViewBag.TypeDropDown = TypeDropDown;
            ExpenseVm expenseVm = new ExpenseVm()
            {
                Expense = new Expense(),
                TypeDropDown = _context.ExpenseTypes
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(expenseVm);
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVm expenses)
        {
            if (ModelState.IsValid)
            {
                // expenses.ExpenseTypeId = 1;
                _context.Expenses.Add(expenses.Expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenses);
        }

        // Get-Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            ExpenseVm expenseVm = new ExpenseVm()
            {
                Expense = new Expense(),
                TypeDropDown = _context.ExpenseTypes
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseVm.Expense = _context.Expenses.Find(id);
            if (expenseVm.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVm);
        }

        // Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVm model)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Update(model.Expense);
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
            var obj = _context.Expenses.Find(id);
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
            var exp = _context.Expenses.Find(id);
            if (exp == null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(exp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
