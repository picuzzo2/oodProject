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
        private Student user;
        private string userEmail;
        public StudentController(CSFinderContext _db)
        {
            db = _db;

        }
        private bool setUser()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return false;
            }
            else if (user == null)
            {
                
                user = db.Students.Where(u => u.ID.Equals((HttpContext.Session.GetString("UserID")))).FirstOrDefault();
                userEmail = db.Accounts.Where(u => u.ID.Equals(user.ID)).FirstOrDefault().Email;
                return true;
            }
            else
            {
                return true;
            }
            
        }

        public IActionResult StudentDashBoard()
        {
            if (!setUser() || HttpContext.Session.GetString("IDType") != "Student") { return RedirectToAction("Login", "RegisLogin"); }
            Debug.WriteLine(HttpContext.Session.GetString("UserID"));
            Debug.WriteLine(user.ID);
            ViewBag.UserID = user.ID;
            ViewBag.Email =userEmail;
            ViewBag.SID = user.SID;
            ViewBag.Name = user.Name;
            ViewBag.Status = user.Status;
            return View();
        }

        public IActionResult History()
        {
            if (!setUser() || HttpContext.Session.GetString("IDType") != "Student") { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.studentName = "Anicha Harnpa";
            ViewBag.rankFist = "บริษัท ปูนซีเมนต์ไทย จำกัด (มหาชน)";
            ViewBag.rankSec = "บริษัท ไอแอนด์ไอ กรุ๊ป จำกัด (มหาชน)";
            ViewBag.rankLast = "บริษัท ซีพี ออล์ จำกัด (มหาชน)";
            ViewBag.rankComplete = "บริษัท ปูน...";
            ViewBag.studentStatus = "รอนัดสัมภาษณ์";
            return View();
        }


        public IActionResult Profile()
        {
            if (!setUser() || HttpContext.Session.GetString("IDType") != "Student") { return RedirectToAction("Login", "RegisLogin"); }
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
            if (!setUser() || HttpContext.Session.GetString("IDType") != "Student") { return RedirectToAction("Login", "RegisLogin"); }
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
            if (!setUser() || HttpContext.Session.GetString("IDType") != "Student") { return RedirectToAction("Login", "RegisLogin"); }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }

    }
}
