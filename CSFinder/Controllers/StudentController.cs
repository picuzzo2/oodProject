﻿using System;
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
            int maxRound;
            if (db.Matchings.Count() == 0) maxRound = 0;
            else maxRound= int.Parse(db.Matchings.Max(p => p.MID));
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
            if (user.Rank1 == null) ViewBag.rank1Name = "Not choose yet";
            else ViewBag.rank1Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            if (user.Rank2 == null) ViewBag.rank2Name = "Not choose yet";
            else ViewBag.rank2Name = db.Companies.Where(x => x.CID == user.Rank2).FirstOrDefault().Name;
            if (user.Rank3 == null) ViewBag.rank3Name = "Not choose yet";
            else ViewBag.rank3Name = db.Companies.Where(x => x.CID == user.Rank3).FirstOrDefault().Name;


            return View();
        }

        public IActionResult Profile()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
            if (user.Rank1 == null) ViewBag.rank1Name = "Not choose yet";
            else ViewBag.rank1Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            if (user.Rank2 == null) ViewBag.rank2Name = "Not choose yet";
            else ViewBag.rank2Name = db.Companies.Where(x => x.CID == user.Rank2).FirstOrDefault().Name;
            if (user.Rank3 == null) ViewBag.rank3Name = "Not choose yet";
            else ViewBag.rank3Name = db.Companies.Where(x => x.CID == user.Rank3).FirstOrDefault().Name;

            return View();
        }
        public IActionResult EditProfile()
        {
            if (!setUser()) { Debug.WriteLine("Redirecting");  return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.user = user;
            Student model = user;
            if (user.Rank1 == null) ViewBag.rank1Name = "Not choose yet";
            else ViewBag.rank1Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            if (user.Rank2 == null) ViewBag.rank2Name = "Not choose yet";
            else ViewBag.rank2Name = db.Companies.Where(x => x.CID == user.Rank2).FirstOrDefault().Name;
            if (user.Rank3 == null) ViewBag.rank3Name = "Not choose yet";
            else ViewBag.rank3Name = db.Companies.Where(x => x.CID == user.Rank3).FirstOrDefault().Name;
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProfile(Student objUser)
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            Student model = user;
            if (user.Rank1 == null) ViewBag.rank1Name = "Not choose yet";
            else ViewBag.rank1Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            if (user.Rank2 == null) ViewBag.rank2Name = "Not choose yet";
            else ViewBag.rank2Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            if (user.Rank3 == null) ViewBag.rank3Name = "Not choose yet";
            else ViewBag.rank3Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;

            if (ModelState.IsValid)
            {
                user.Name = objUser.Name;
                user.Type = objUser.Type;
                user.Detail = objUser.Detail;
                user.Phone = objUser.Phone;
                user.Address = objUser.Address;
                user.ImgProfile = objUser.ImgProfile;
                user.ImgResume = objUser.ImgResume;
                user.ImgTranscript = objUser.ImgTranscript;
                string msg = "Profile information saved";
                return Json(new { success = true, responseText = msg });
            }
            return View(model);
        }
        public IActionResult Index()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }

            List<PostCompany> pc = new List<PostCompany>();
            foreach (Post p in db.Posts.OrderByDescending(x => x.PID))
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
            ViewBag.postCompanyList = pc;
            ViewBag.user = user;
            ViewBag.userEmail = userEmail;
            if (user.Rank1 == null) ViewBag.rank1Name = "Not choose yet";
            else ViewBag.rank1Name = db.Companies.Where(x => x.CID == user.Rank1).FirstOrDefault().Name;
            if (user.Rank2 == null) ViewBag.rank2Name = "Not choose yet";
            else ViewBag.rank2Name = db.Companies.Where(x => x.CID == user.Rank2).FirstOrDefault().Name;
            if (user.Rank3 == null) ViewBag.rank3Name = "Not choose yet";
            else ViewBag.rank3Name = db.Companies.Where(x => x.CID == user.Rank3).FirstOrDefault().Name;

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

        [HttpPost]
        public IActionResult Answer(string Ans, string CID, string SID, string MID )
        {
            Student student = db.Students.Where(x => x.SID == SID).FirstOrDefault();
            Matching mat = db.Matchings.Where(x => x.MID == MID && x.SID == SID && x.CID == CID).FirstOrDefault();
            Company com = db.Companies.Where(x => x.CID == CID).FirstOrDefault();

            string fromEmail = db.Accounts.Where(x => x.ID == student.ID).FirstOrDefault().Email;
            string fromName = student.Name;
            string toEmail = db.Accounts.Where(x => x.ID == com.ID).FirstOrDefault().Email;
            string subject = "Interview comfirmation from student";


            string msg="";
            if(Ans == "Accept")
            {
                
                if(mat.Type==0)
                {
                    if(com.TrainneeNeed - com.TrainneeGot < 1)
                    {
                        msg = "Number of the company's trainnee exceeded";
                    }
                    else
                    {
                        com.TrainneeGot++;
                        student.Status = "Confirmed to be an intern at "+db.Companies.Where(x=>x.CID==CID).FirstOrDefault().Name;
                        mat.Result = "Student accepted";
                        msg = "You have been accepted to be company's trainnee";
                        
                        foreach(Matching m in db.Matchings.Where(x=>x.SID==SID && !(x.CID==CID && x.MID==MID)))
                        {
                            m.Result = "Employed";
                        }
                        string detail ="Student accepted to be your Intern";
                        string result = new EmailController(db).SendEmailTo(fromEmail, toEmail, student.Name, com.Name, subject, detail);
                        msg += "\n" + result;

                        db.SaveChanges();
                    }
                }
                else if(mat.Type==1)
                {
                    if (com.CoopNeed - com.CoopGot < 1)
                    {
                        msg = "Number of the company's cooperative students exceeded";
                    }
                    else
                    {
                        com.CoopGot++;
                        student.Status = "Confirmed to be a coorperative student at " + db.Companies.Where(x => x.CID == CID).FirstOrDefault().Name;
                        mat.Result = "Student accepted";
                        msg = "You have been accepted to be company's cooperative student";

                        foreach (Matching m in db.Matchings.Where(x => x.SID == SID && !(x.CID == CID && x.MID == MID)))
                        {
                            m.Result = "Employed";
                        }
                        string detail = "Student accepted to be your cooperative student";
                        string result = new EmailController(db).SendEmailTo(fromEmail, toEmail, student.Name, com.Name, subject, detail);
                        msg += "\n" + result;
                        db.SaveChanges();
                    }
                }

            }
            else if(Ans == "Reject")
            {
                mat.Result = "Student rejected";
                msg = "Rejection completed";
                string detail = "Student rejected";
                string result = new EmailController(db).SendEmailTo(fromEmail, toEmail, student.Name, com.Name, subject, detail);
                msg += "\n" + result;
                db.SaveChanges();
            }

            return Json(msg);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }

    }
}
