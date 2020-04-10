using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    [Route("ComSci")]
    public class ComSciController : Controller
    {
        [Route("Home")]
        public IActionResult Home()
        {
            ViewBag.studentName = "Earn";
            ViewBag.kuy = "kuy";
            return View();
        }
        [Route("Company")]
        public IActionResult Company()
        {
            return View();
        }
        [Route("Student")]
        public IActionResult Student()
        {
            return View();
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }
    }
}