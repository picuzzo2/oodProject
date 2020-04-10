<<<<<<< Updated upstream
﻿using System;
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

=======
﻿using System;
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
            ViewBag.companyName = "บริษัท เอ็ม เอฟ อี ซี จำกัด (มหาชน)";
            ViewBag.companyAddress = "699 อาคารโมเดอร์นฟอร์มทาวเวอร์ ชั้น 27 ถนนศรีนครินทร์ แขวงพัฒนาการ เขตสวนหลวง กรุงเทพมหานคร 10250";
            ViewBag.companyPhone = "+66 (0) 2821-7999";
            ViewBag.companyEmail = "sales@mfec.com";
            ViewBag.companyPostName = "บริษัท HandyWings";
            ViewBag.companyPostDetail = "บริษัท HandyWings เปิดรับสมัคร เพื่อนร่วมทีม เพิ่มเติมหลายอัตราเพื่อรองรับการขยายงาน";
            ViewBag.ProfileOtherCompanyPost = "https://sv1.picz.in.th/images/2020/04/10/QMv1uZ.jpg";
            ViewBag.Post1Img = "https://sv1.picz.in.th/images/2020/04/11/UWg8eR.jpg";
            ViewBag.PhotoOfOtherCompanyPost = "https://sv1.picz.in.th/images/2020/04/10/QMvKII.jpg";
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

>>>>>>> Stashed changes
