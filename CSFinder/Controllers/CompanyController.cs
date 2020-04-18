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
        private Student stu;
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
                user = db.Companies.Where(u => u.ID.Equals((HttpContext.Session.GetString("UserID")))).FirstOrDefault();
                userEmail = db.Accounts.Where(u => u.ID.Equals(user.ID)).FirstOrDefault().Email;
                return true;
            }

        }
        public IActionResult Home()
        {
            Debug.WriteLine("homepost5555555555");
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            List<PostCompany> pc = new List<PostCompany>();
            foreach (Post p in db.Posts.OrderByDescending(x=>x.PID))
            {
                if (p.CID == "1000000")
                {
                    Admin a = db.Admins.Where(x => x.ID == "cscmu").FirstOrDefault();
                    Company c = new Company();
                    c.CID = "1000000";
                    c.ImgProfile = a.ImgProfile;
                    c.Name = a.Department;
                    pc.Add(new PostCompany(c, p));
                }
                else
                {
                    Company c = db.Companies.Where(a => a.CID.Equals(p.CID)).FirstOrDefault();
                    pc.Add(new PostCompany(c, p));
                }
            }
            Post newPost = new Post();
            ViewBag.postCompanyList = pc;
            ViewBag.company = user;
            ViewBag.userEmail = userEmail;
            return View(newPost);
        }
        [HttpPost]
        public IActionResult Home(Post objPost)
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
           

            if (ModelState.IsValid)
            {
                List<PostCompany> pc = new List<PostCompany>();
                foreach (Post p in db.Posts)
                {
                    Company c = db.Companies.Where(a => a.CID.Equals(p.CID)).FirstOrDefault();
                    pc.Add(new PostCompany(c, p));
                }
                ViewBag.postCompanyList = pc;
                ViewBag.company = user;
                ViewBag.userEmail = userEmail;
                String msg = "";

                Post addpost = new Post();
                Post userP = new Post();
                int LastPID = LastPID = db.Posts.Max(p => p.PID);

                userP.CID = user.CID;
                if (LastPID == 0)
                {
                    userP.PID = 1;
                }
                else
                {
                    userP.PID = LastPID + 1;
                }
                Debug.WriteLine(objPost.Detail);
                if (msg == "")                   
                {
                    addpost.CID = user.CID;
                    addpost.PID = userP.PID;
                    addpost.Detail = objPost.Detail;
                    addpost.ImgLink = objPost.ImgLink;

                    db.Posts.Add(addpost);
                    db.SaveChanges();

                    msg = "Post Success";

                    return Json(new { success = true, responseText = msg });
                }
                else
                {
                    return Json(new { success = false, responseText = msg });
                }
            }
                    return View(objPost);
        }
     
        public IActionResult Notification()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;

            List<StudentNameMatching> sm = new List<StudentNameMatching>();
            foreach (Matching m in db.Matchings)
            {
                if (m.CID == user.CID)
                {
                    Student s = db.Students.Where(b => b.SID.Equals(m.SID)).FirstOrDefault();
                    sm.Add(new StudentNameMatching(s, m));
                }
            }
            ViewBag.StudentNameMatchingList = sm;

            return View();

        }
        [HttpPost]
        public IActionResult InterviewAppointment(Message message, string MID)
        {
            Debug.WriteLine("############################################################");
            Debug.WriteLine(message.Date);
            Debug.WriteLine(message.Detail);
            Debug.WriteLine(message.from);
            Debug.WriteLine(message.to);
            Debug.WriteLine(MID);
            Matching m = db.Matchings.Where(x => x.MID == MID && x.SID == message.to && x.CID == message.from).FirstOrDefault();
            m.Result = "Waiting for interview result";
            db.SaveChanges();
            return Json("The Email has send");
        }

        [HttpPost]
        public IActionResult InterviewConfirmation(Message message, string MID)
        {
            string msg ="";
            Matching m = db.Matchings.Where(x => x.MID == MID && x.SID == message.to && x.CID == message.from).FirstOrDefault();
            if (message.Detail== "Pass_Status")
            {
                m.Result = "Company accepted";
                Student s = db.Students.Where(x => x.SID == m.SID).FirstOrDefault();
                s.Status = m.CID;
                db.SaveChanges();
                msg += "Student Accepted";
            }
            else if(message.Detail == "Failed_Status")
            {
                m.Result = "Company rejected";
                db.SaveChanges();
                msg += "Student Rejected";
            }
            return Json(msg);
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

        public IActionResult Profile()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
            List<PostCompany> userPosts = new List<PostCompany>();
            foreach(Post p in db.Posts.Where(x => x.CID == user.CID).OrderByDescending(x => x.PID).ToList())
            {
                userPosts.Add(new PostCompany(user, p));
            }
            ViewBag.posts = userPosts;
           

            return View();
        }

        public IActionResult EditProfile()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            var model = user;
            ViewBag.userEmail = userEmail;
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(Company objUser)
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            Company model = user;
            ViewBag.userEmail = userEmail;
            if (ModelState.IsValid)
            {
                user.Name = objUser.Name;
                user.TrainneeNeed = objUser.TrainneeNeed;
                user.CoopNeed = objUser.CoopNeed;
                user.Phone = objUser.Phone;
                user.Detail = objUser.Detail;
                user.Address = objUser.Address;
                db.SaveChanges();
                string msg = "Profile information saved";
                return Json(new { success = true, responseText = msg });
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin"); ;
        }
    }
}
