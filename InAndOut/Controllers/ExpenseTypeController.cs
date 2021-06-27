using System.Collections;
using System.Collections.Generic;
using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET
        public IActionResult Index()
        {
            IEnumerable<ExpenseType> expenseTypes = _db.ExpenseTypes;
            return View(expenseTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(expenseType);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(expenseType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var expensetype = _db.ExpenseTypes.Find(id);
            if (expensetype == null)
            {
                return NotFound();
            }

            return View(expensetype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var expenseType = _db.ExpenseTypes.Find(id);
            if (expenseType == null)
            {
                return NotFound();
            }

            _db.ExpenseTypes.Remove(expenseType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var expenseType = _db.ExpenseTypes.Find(id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Update(expenseType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseType);
        }
    }
}