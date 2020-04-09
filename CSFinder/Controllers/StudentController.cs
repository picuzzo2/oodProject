using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSFinder.Models;
using Microsoft.AspNetCore.Http;

namespace CSFinder.Controllers
{
    
    public class StudentController : Controller
    {
        private CSFinderContext db;
        public StudentController(CSFinderContext _db)
        {
            db = _db;
        }

        public IActionResult StudentDashBoard()
        {
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.IDType = HttpContext.Session.GetString("IDType");
            ViewBag.SID = HttpContext.Session.GetString("SID");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Status = HttpContext.Session.GetString("Status");
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        
        public IActionResult Profile()
        {
            ViewBag.studentName = "Anicha Harnpa";
            ViewBag.studentStatus = "รอนัดสัมภาษณ์";
            ViewBag.studentFirstname = "อณิชา";
            ViewBag.studentLastname = "หารป่า";
            ViewBag.studentAddress = "4 หมู่9 ต.หางดง อ.หางดง จ.เชียงใหม่ 50230";
            ViewBag.studentPhone = "0903186625";
            ViewBag.studentFacebook = "Anicha Harnpa";
            ViewBag.studentEmail = "anicha_h@gmail.com";
            ViewBag.studentRanking = "1. บริษัท ปูนซีเมนต์ไทย จำกัด (มหาชน)";
            ViewBag.studentResume = "";
            return View();
        }

        
        public IActionResult EditProfile()
        {
            ViewBag.studentName = "Anicha Harnpa";
            ViewBag.studentStatus = "รอนัดสัมภาษณ์";
            ViewBag.studentFirstname = "อณิชา";
            ViewBag.studentLastname = "หารป่า";
            ViewBag.studentAddress = "4 หมู่9 ต.หางดง อ.หางดง จ.เชียงใหม่ 50230";
            ViewBag.studentPhone = "0903186625";
            ViewBag.studentFacebook = "Anicha Harnpa";
            ViewBag.studentEmail = "anicha_h@gmail.com";
            ViewBag.studentRanking = "1. บริษัท ปูนซีเมนต์ไทย จำกัด (มหาชน)";
            ViewBag.studentResume = "";
            ViewBag.studentTranscript = "";
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }
        public IActionResult Register()
        {
            return View();
        }



    }
}
