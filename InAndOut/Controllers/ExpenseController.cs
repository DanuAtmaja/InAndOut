using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses;
            foreach (var obj in objList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }
            return View(objList);
        }

//        /**
//         * Using old model mvc
//         */
//        public IActionResult Create()
//        {
//            IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem()
//            {
//                Text = i.Name,
//                Value = i.Id.ToString()
//            });
//            
//            ViewBag.TypeDropDown = TypeDropDown;
//            
//            
//            return View();
//        }

        /**
         * Using new MVVM Architecture
         */
        public IActionResult Create()
        {
            ExpenseVM expenseVm = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(expenseVm);
        }
        
//        /**
//         * Using Old Model MVC Create
//         */
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Expense obj)
//        {
//            if (ModelState.IsValid)
//            {
//                _db.Expenses.Add(obj);
//                _db.SaveChanges();
//
//                return RedirectToAction("Index");
//            }
//            return View(obj);
//        }

        /**
         * New Model Using MVVM
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }
            

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View();
        }

        //POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
//        /**
//         * Old Model MVC Updates
//         */
//        public IActionResult Update(int? id)
//        {
//            if(id == null || id == 0)
//            {
//                return NotFound();
//            }
//            var obj = _db.Expenses.Find(id);
//            if(obj == null)
//            {
//                return NotFound();
//            }
//            return View(obj);
//        }
//
//        /**
//         * Old model MVC Update
//         */
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Update(Expense obj)
//        {
//            if (ModelState.IsValid)
//            {
//                _db.Expenses.Update(obj);
//                _db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(obj);
//        }
        
        public IActionResult Update(int? id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if(id == null || id == 0)
            {
                return NotFound();
            }
            expenseVM.Expense  = _db.Expenses.Find(id);
            if(expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
