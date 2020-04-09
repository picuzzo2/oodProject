using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    [Route("Company")]
    public class CompanyController : Controller
    {
        [Route("Details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}