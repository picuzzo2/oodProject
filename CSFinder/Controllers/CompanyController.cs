using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Home()
        {
            ViewBag.companyName = "บริษัท ปูนซีเมนต์ไทย จำกัด (มหาชน)";
            ViewBag.companyAddress = "เลขที่ 1 ถนนปูนซีเมนต์ไทย แขวงบางซื่อ เขตบางซื่อ กรุงเทพฯ 10800";
            ViewBag.companyPhone = "053-222180";
            ViewBag.companyLine = "@scg.contact.center";
            ViewBag.companyPostName = "บริษัท HandyWings";
            ViewBag.companyPostDetail = "บริษัท HandyWings เปิดรับสมัคร เพื่อนร่วมทีม เพิ่มเติมหลายอัตราเพื่อรองรับการขยายงาน";
            ViewBag.Post1Img = "https://sv1.picz.in.th/images/2020/04/10/QMlEgD.jpg";
            return View();
        }
        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin"); ;
        }
    }
}

