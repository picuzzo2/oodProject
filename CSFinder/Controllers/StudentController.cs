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
                Debug.WriteLine(user.SID);
                return true;

            }
            
        }

        public IActionResult History()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.Matchs = db.Matchings;
            ViewBag.Status = user.Status;

            int maxRound = int.Parse(db.Matchings.Max(p => p.MID));
            ViewBag.maxRound = maxRound;

            List<CompanyNameMatching> cm = new List<CompanyNameMatching>();
            foreach(Matching m in db.Matchings)
            {           
                if(m.SID == user.SID)
                {
                    string r1 = db.Companies.Where(b => b.CID.Equals(m.sRank1)).FirstOrDefault().Name;
                    string r2 = db.Companies.Where(b => b.CID.Equals(m.sRank2)).FirstOrDefault().Name;
                    string r3 = db.Companies.Where(b => b.CID.Equals(m.sRank3)).FirstOrDefault().Name;
                    string resultName = db.Companies.Where(b => b.CID.Equals(m.CID)).FirstOrDefault().Name;
                    cm.Add(new CompanyNameMatching(m, r1, r2, r3, resultName));
                }
            }
            ViewBag.CompanyNameMatchingList = cm;

            return View();
        }
        public IActionResult ReplyHistory()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.Dates = "(12/12/2562)";
            ViewBag.RankComplete = "บริษัท ปูน...";
            ViewBag.Status = user.Status;

            return View();
        }
        public IActionResult CompleteHistory()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.Dates = "(12/12/2562)";
            ViewBag.RankComplete = "บริษัท ปูน...";
            ViewBag.Status = user.Status;

            return View();
        }

        public IActionResult Profile()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
            return View();
        }
        public IActionResult EditProfile()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            return View();
        }
        [HttpPost]
        public IActionResult EditProfile(StudentAccount objStu)
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            Debug.WriteLine("------------------------");
            Debug.WriteLine(objStu.Email);
            Debug.WriteLine(objStu.Name);
            Debug.WriteLine(objStu.Address);
            Debug.WriteLine(objStu.Phone);
            Debug.WriteLine("------------------------");

            if (ModelState.IsValid)
            {
                String msg = "";
                //check dup acc
                //check dup sid
                //check dup email
              
                if (msg == "")
                {
                    foreach (Account acc in db.Accounts)
                    {
                        if (acc.Email.Equals(objStu.Email))
                        {
                            msg = "Email: " + objStu.Email + " already exist";
                            break;
                        }
                    }
                }

                if (msg == "")
                {
                    Account addacc = new Account();
                    Student addstu = new Student();
                    addacc.Email = objStu.Email;
                    db.Accounts.Add(addacc);
                    db.SaveChanges();

                    addstu.Name = objStu.Name;
                    addstu.Phone = objStu.Phone;
                    addstu.Address = objStu.Address;
                    db.Students.Add(addstu);
                    db.SaveChanges();

                    msg = "Edit profile complete!";

                    return Json(new { success = true, responseText = msg });
                }
                else
                {
                    return Json(new { success = false, responseText = msg });
                }
            }
            return View();
        }
        public IActionResult Index()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }

            List<PostCompany> pc = new List<PostCompany>();
            foreach(Post p in db.Posts.OrderByDescending(x=>x.PID))
            {
                Company c = db.Companies.Where(a => a.CID.Equals(p.CID)).FirstOrDefault();
                pc.Add(new PostCompany (c,  p ));
            }
            ViewBag.postCompanyList = pc;
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
            ViewBag.rank1Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            ViewBag.rank2Name = db.Companies.Where(x => x.CID == user.Rank2).FirstOrDefault().Name;
            ViewBag.rank3Name = db.Companies.Where(x => x.CID == user.Rank3).FirstOrDefault().Name;
            return View();
        }

        [HttpPost]
        public IActionResult changeRank(string change, string to)
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            if (change == "1")
            {
                if(user.Rank2 == to) user.Rank2 = user.Rank1;
                else if (user.Rank3 == to) user.Rank3 = user.Rank1;
                user.Rank1 = to; 
            }
            else if(change =="2")
            {
                if (user.Rank1 == to) user.Rank1 = user.Rank2;
                else if (user.Rank3 == to) user.Rank3 = user.Rank2;
                user.Rank2 = to;
            }
            else
            {
                if (user.Rank1 == to) user.Rank1 = user.Rank3;
                else if (user.Rank2 == to) user.Rank2 = user.Rank3;
                user.Rank3 = to;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Student") ;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }

    }
}
