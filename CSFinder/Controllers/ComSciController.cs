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
        [Route("home")]
        public IActionResult Home()
        {
            ViewBag.studentName = "Earn";
            ViewBag.kuy = "kuy";
            return View();
        }
    }
}