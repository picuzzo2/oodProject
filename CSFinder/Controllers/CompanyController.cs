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
        [Route("Home")]
        public IActionResult Home()
        {
            ViewBag.companyName = "บริษัท ปูนซีเมนต์ไทย จำกัด (มหาชน)";
            ViewBag.companyAddress = "เลขที่ 1 ถนนปูนซีเมนต์ไทย แขวงบางซื่อ เขตบางซื่อ กรุงเทพฯ 10800";
            ViewBag.companyPhone = "053-222180";
            ViewBag.companyLine = "@scg.contact.center";
            ViewBag.companyPostName = "บริษัท HandyWings";
            ViewBag.companyPostDetail = "บริษัท HandyWings เปิดรับสมัคร เพื่อนร่วมทีม เพิ่มเติมหลายอัตราเพื่อรองรับการขยายงาน";
            return View();
        }
        [Route("Notification")]
        public IActionResult Notification()
        {
            return View();
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "RegisLogin"); ;
        }
    }
}

