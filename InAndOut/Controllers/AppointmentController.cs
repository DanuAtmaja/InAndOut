using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InAndOut.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: /appointment/
        public IActionResult Index()
        {
            //return View();
            string todaysDate = DateTime.Now.ToShortDateString();
            return Ok(todaysDate);
        }

        // GET: /appointment/details/
        public IActionResult Details(int id)
        {
            return Ok("You have entered id = {id}" + id);
        }
    }
}
