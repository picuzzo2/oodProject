using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            else if(HttpContext.Session.GetString("IDType") != "Student")
            {
                return false;
            }
            else 
            {
                Debug.WriteLine("Set User");
                Debug.WriteLine(user == null);
                user = db.Students.Where(u => u.ID.Equals((HttpContext.Session.GetString("UserID")))).FirstOrDefault();
                userEmail = db.Accounts.Where(u => u.ID.Equals(user.ID)).FirstOrDefault().Email;
                return true;
            }
            
        }

        public IActionResult StudentDashBoard()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
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
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.Matchs = db.Matchings;
            ViewBag.rankComplete = "บริษัท ปูน...";
            return View();
        }


        public IActionResult Profile()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
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
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            return View();
        }
        public IActionResult Index()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }

            List<PostCompany> pc = new List<PostCompany>();
            foreach(Post p in db.Posts)
            {
                Company c = db.Companies.Where(a => a.CID.Equals(p.CID)).FirstOrDefault();
                pc.Add(new PostCompany (c,  p ));
            }
            ViewBag.postCompanyList = pc;
            //ViewBag.Posts = db.Posts;
            foreach(PostCompany p in pc)
            {
                Debug.WriteLine(p.post.PID);
                Debug.WriteLine(p.company.Name);
            }
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }

    }
}
