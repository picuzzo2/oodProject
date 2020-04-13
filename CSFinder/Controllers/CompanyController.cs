
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    public class CompanyController : Controller
    {
        private Company user;
        private CSFinderContext db;
        private string userEmail;
        public CompanyController(CSFinderContext _db)
        {
            db = _db;

        }
        private bool setUser()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return false;
            }
            else if (HttpContext.Session.GetString("IDType") != "Company")
            {
                return false;
            }
            else
            {
                Debug.WriteLine("Set User");
                Debug.WriteLine(user == null);
                user = db.Companies.Where(u => u.ID.Equals((HttpContext.Session.GetString("UserID")))).FirstOrDefault();
                userEmail = db.Accounts.Where(u => u.ID.Equals(user.ID)).FirstOrDefault().Email;
                return true;
            }

        }
        public IActionResult Home(CompanyAccount objUser)
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            List<PostCompany> pc = new List<PostCompany>();
            foreach (Post p in db.Posts)
            {
                Company c = db.Companies.Where(a => a.CID.Equals(p.CID)).FirstOrDefault();
                pc.Add(new PostCompany(c, p));
            }
            ViewBag.postCompanyList = pc;
            ViewBag.company = user;
            ViewBag.userEmail = userEmail;


            ViewBag.AllStudent = "18";
            ViewBag.CooStudent = "8";
            ViewBag.TrainStudent = "10";
            return View();
        }
        public IActionResult Notification()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.companyName = "บริษัท เอ็ม เอฟ อี ซี จำกัด (มหาชน)";
            ViewBag.companyAddress = "699 อาคารโมเดอร์นฟอร์มทาวเวอร์ ชั้น 27 ถนนศรีนครินทร์ แขวงพัฒนาการ เขตสวนหลวง กรุงเทพมหานคร 10250";
            ViewBag.companyPhone = "+66 (0) 2821-7999";
            ViewBag.companyEmail = "sales@mfec.com";
            ViewBag.Post1Img = "https://sv1.picz.in.th/images/2020/04/11/UWg8eR.jpg";
            ViewBag.studentPhotoexample = "https://www.w3schools.com/howto/img_avatar.png";
            ViewBag.studentInterestName1 = "นางสาวอณิชา หารป่า";
            ViewBag.studentInterestName2 = "นางสาวปานระวี ไชยสิทธิ์";
            ViewBag.studentInterestName3 = "นายภรัญยู วงศ์แสง";
            return View();
        }

        public IActionResult Notification_Announcement()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.companyName = "บริษัท เอ็ม เอฟ อี ซี จำกัด (มหาชน)";
            ViewBag.companyAddress = "699 อาคารโมเดอร์นฟอร์มทาวเวอร์ ชั้น 27 ถนนศรีนครินทร์ แขวงพัฒนาการ เขตสวนหลวง กรุงเทพมหานคร 10250";
            ViewBag.companyPhone = "+66 (0) 2821-7999";
            ViewBag.companyEmail = "sales@mfec.com";
            ViewBag.Post1Img = "https://sv1.picz.in.th/images/2020/04/11/UWg8eR.jpg";
            ViewBag.studentPhotoexample = "https://www.w3schools.com/howto/img_avatar.png";
            ViewBag.studentInterestName1 = "นางสาวอณิชา หารป่า";
            ViewBag.studentInterestName2 = "นางสาวปานระวี ไชยสิทธิ์";
            ViewBag.studentInterestName3 = "นายภรัญยู วงศ์แสง";
            ViewBag.studentInterestStatus1 = "รอสัมภาษณ์";
            ViewBag.studentInterestStatus2 = "ผ่าน";
            ViewBag.studentInterestStatus3 = "ไม่ผ่าน";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin"); ;
        }
    }
}

