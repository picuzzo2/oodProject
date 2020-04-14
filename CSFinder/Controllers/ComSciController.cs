﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSFinder.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Hangfire;
using Hangfire.Storage;

namespace CSFinder.Controllers
{
    public class ComSciController : Controller
    {
        private Admin user;
        private CSFinderContext db;
        private string userEmail;
        public ComSciController(CSFinderContext _db)
        {
            db = _db;
        }
        private bool setUser()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return false;
            }
            else if (HttpContext.Session.GetString("IDType") != "Admin")
            {
                return false;
            }
            else
            {
                Debug.WriteLine("Set User");
                Debug.WriteLine(user == null);
                user = db.Admins.Where(u => u.ID.Equals(HttpContext.Session.GetString("UserID"))).FirstOrDefault();
                userEmail = db.Accounts.Where(u => u.ID.Equals(user.ID)).FirstOrDefault().Email;
                return true;
            }

        }


        public IActionResult Home()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.studentName = "Earn";
            ViewBag.kuy = "kuy";
            return View();
        }
        private DateTime? GetNextExecutionTime(string id)
        {
            try
            {
                var job = JobStorage.Current.GetConnection().GetRecurringJobs().Single(x => x.Id == id);
                return job == null ? null : job.NextExecution;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IActionResult Matching()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            DateTime? next = GetNextExecutionTime("Matching");
            ViewBag.next = next;
            return View();
        }

        public void StartMatch()
        {
            var studentList = db.Students.OrderBy(x => Guid.NewGuid()).ToList();
            List<StudentMatching> stuMatch = new List<StudentMatching>();
            foreach(Student stu in studentList)
            {
                if(stu.Status == "Waiting for matching")
                {
                    StudentMatching newAddStu = new StudentMatching();
                    newAddStu.SID = stu.SID;
                    newAddStu.Type = stu.Type;
                    newAddStu.Rank1 = stu.Rank1;
                    newAddStu.Rank2 = stu.Rank2;
                    newAddStu.Rank3 = stu.Rank3;
                    stuMatch.Add(newAddStu);
                }
            }
            var companyList = db.Companies.ToList();
            List<CompanyMatching> comMatch = new List<CompanyMatching>();
            foreach(Company com in companyList)
            {
                CompanyMatching newAddCom = new CompanyMatching();
                newAddCom.CID = com.CID;
                newAddCom.TrainneeNeed = com.TrainneeNeed;
                newAddCom.TrainneeGot = com.TrainneeGot;
                newAddCom.CoopNeed = com.CoopNeed;
                newAddCom.CoopGot = com.CoopGot;
                comMatch.Add(newAddCom);
            }

            //loop for rank 1,2,3
            //type 0 = trainnee
            //type 1 = coop
            string LastMatch = db.Matchings.Max(x => x.MID);
            
            if(LastMatch == null)
            {
                LastMatch = "1";
            }
            else
            {
                LastMatch = (int.Parse(LastMatch) + 1).ToString();
            }

            for (int i = 1; i <= 3; i++)
            {
                Debug.WriteLine("");
                Debug.WriteLine("");
                Debug.WriteLine("start Match Rank:" + i);
                foreach (StudentMatching stu in stuMatch.ToList())
                {
                    if (i == 1)
                    {
                        if(stu.Type == 1)
                        {
                            //trainnee
                            var com = comMatch.Where(x => x.CID == stu.Rank1).FirstOrDefault();
                            if(com.TrainneeNeed-com.TrainneeGot > 0)
                            {
                                comMatch.Where(x => x.CID == com.CID).FirstOrDefault().TrainneeGot++;
                                Debug.WriteLine("Student:" + stu.SID + " Got Company:" + com.CID + " On Type:" + stu.Type + " On MID:" + LastMatch + " On Rank:" + i) ;
                                Debug.WriteLine("Company TrainneeNeed: " + com.TrainneeNeed + " TrainneeGot:" + com.TrainneeGot);
                                Matching addMatch = new Matching();
                                addMatch.MID = LastMatch;
                                addMatch.Date = DateTime.UtcNow;
                                addMatch.CID = com.CID;
                                addMatch.SID = stu.SID;
                                addMatch.Type = stu.Type;
                                db.Matchings.Add(addMatch);
                                db.SaveChanges();
                                stuMatch.Remove(stu);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if(stu.Type==0)
                        {
                            //coop
                            var com = comMatch.Where(x => x.CID == stu.Rank1).FirstOrDefault();
                            if (com.CoopNeed - com.CoopGot > 0)
                            {
                                comMatch.Where(x => x.CID == com.CID).FirstOrDefault().CoopGot++;
                                Debug.WriteLine("Student:" + stu.SID + " Got Company:" + com.CID + " On Type:" + stu.Type + " On MID:" + LastMatch + " On Rank:" +i);
                                Debug.WriteLine("Company Coopneed:" + com.CoopNeed + "CoopGot:" + com.CoopGot);
                                Matching addMatch = new Matching();
                                addMatch.MID = LastMatch;
                                addMatch.Date = DateTime.UtcNow;
                                addMatch.CID = com.CID;
                                addMatch.SID = stu.SID;
                                addMatch.Type = stu.Type;
                                db.Matchings.Add(addMatch);
                                db.SaveChanges();
                                stuMatch.Remove(stu);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        if (stu.Type == 1)
                        {
                            //trainnee
                            var com = comMatch.Where(x => x.CID == stu.Rank2).FirstOrDefault();
                            if (com.TrainneeNeed - com.TrainneeGot > 0)
                            {
                                
                                comMatch.Where(x => x.CID == com.CID).FirstOrDefault().TrainneeGot++;
                                Debug.WriteLine("Student:" + stu.SID + " Got Company:" + com.CID + " On Type:" + stu.Type + " On MID:" + LastMatch + " On Rank:" + i);
                                Debug.WriteLine("Company TrainneeNeed: " + com.TrainneeNeed + " TrainneeGot:" + com.TrainneeGot);
                                Matching addMatch = new Matching();
                                addMatch.MID = LastMatch;
                                addMatch.Date = DateTime.UtcNow;
                                addMatch.CID = com.CID;
                                addMatch.SID = stu.SID;
                                addMatch.Type = stu.Type;
                                db.Matchings.Add(addMatch);
                                db.SaveChanges();
                                stuMatch.Remove(stu);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (stu.Type == 0)
                        {
                            //coop
                            var com = comMatch.Where(x => x.CID == stu.Rank2).FirstOrDefault();
                            if (com.CoopNeed - com.CoopGot > 0)
                            {
                                
                                comMatch.Where(x => x.CID == com.CID).FirstOrDefault().CoopGot++;
                                Debug.WriteLine("Student:" + stu.SID + " Got Company:" + com.CID + " On Type:" + stu.Type + " On MID:" + LastMatch + " On Rank:" + i);
                                Debug.WriteLine("Company CoopNeed: " + com.CoopNeed + " CoopGot:" + com.CoopGot);
                                Matching addMatch = new Matching();
                                addMatch.MID = LastMatch;
                                addMatch.Date = DateTime.UtcNow;
                                addMatch.CID = com.CID;
                                addMatch.SID = stu.SID;
                                addMatch.Type = stu.Type;
                                db.Matchings.Add(addMatch);
                                db.SaveChanges();
                                stuMatch.Remove(stu);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if(i==3)
                    {
                        if (stu.Type == 1)
                        {
                            //trainnee
                            var com = comMatch.Where(x => x.CID == stu.Rank3).FirstOrDefault();
                            if (com.TrainneeNeed - com.TrainneeGot > 0)
                            {
                                
                                comMatch.Where(x => x.CID == com.CID).FirstOrDefault().TrainneeGot++;
                                Debug.WriteLine("Student:" + stu.SID + " Got Company:" + com.CID + " On Type:" + stu.Type + " On MID:" + LastMatch + " On Rank:" + i);
                                Debug.WriteLine("Company TrainneeNeed: " + com.TrainneeNeed + " TrainneeGot:" + com.TrainneeGot);
                                Matching addMatch = new Matching();
                                addMatch.MID = LastMatch;
                                addMatch.Date = DateTime.UtcNow;
                                addMatch.CID = com.CID;
                                addMatch.SID = stu.SID;
                                addMatch.Type = stu.Type;
                                db.Matchings.Add(addMatch);
                                db.SaveChanges();
                                stuMatch.Remove(stu);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (stu.Type == 0)
                        {
                            //coop
                            var com = comMatch.Where(x => x.CID == stu.Rank3).FirstOrDefault();
                            if (com.CoopNeed - com.CoopGot > 0)
                            {
                                
                                comMatch.Where(x => x.CID == com.CID).FirstOrDefault().CoopGot++;
                                Debug.WriteLine("Student:" + stu.SID + " Got Company:" + com.CID + " On Type:" + stu.Type + " On MID:" + LastMatch + " On Rank:" + i);
                                Debug.WriteLine("Company CoopNeed: " + com.CoopNeed + " CoopGot:" + com.CoopGot);
                                Matching addMatch = new Matching();
                                addMatch.MID = LastMatch;
                                addMatch.Date = DateTime.UtcNow;
                                addMatch.CID = com.CID;
                                addMatch.SID = stu.SID;
                                addMatch.Type = stu.Type;
                                db.Matchings.Add(addMatch);
                                db.SaveChanges();
                                stuMatch.Remove(stu);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public IActionResult AddMatchSchedule()
        {
            RecurringJob.AddOrUpdate("Matching", () => StartMatch(), Cron.Weekly, TimeZoneInfo.Local);
            return RedirectToAction("Matching", "ComSci");
        }
        public IActionResult Company()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.Com1 = "บริษัท ซีเอสไอ ประเทศไทย จำกัด";
            ViewBag.Com2 = "บริษัท ซีเอสลอกอินโฟ จำกัด";
            ViewBag.Com3 = "บริษัท ลานนาคอม จำกัด";
            ViewBag.Com4 = "บริษัท ซีพีออลล์ จำกัด";
            ViewBag.Com5 = "บริษัท ไอยา";
            ViewBag.Com6 = "คณะพยาบาลศาสตร์ มหาวิทยาลัยเชียงใหม่";
            ViewBag.Com7 = "ฝ้ายเบเกอรี่";
            ViewBag.Com8 = "บริษัท เทคมูฟ จำกัด";
            ViewBag.Com9 = "ศูนย์เครือข่ายห้องสมุดดิจิทัล จุฬา สำนักวิทยาทรัพยากร";
            ViewBag.Com10 = "ธนาคารไทยพาณิชย์(มหาชน) จำกัด";
            return View();
        }
        
        public IActionResult Student()
        {
            if (!setUser()) { return RedirectToAction("Login", "RegisLogin"); }
            ViewBag.student = db.Students;
            ViewBag.comsci = user;
            ViewBag.userEmail = userEmail;
            ViewBag.comSciAddress = "239 ถนนห้วยแก้ว ต.สุเทพ อ.เมือง จ.เชียงใหม่ 50200";
            ViewBag.comSciTel = "053-222180";
            ViewBag.comSciEmail = "Compsci@cmu.ac.th";

            ViewBag.companyName1 = "บริษัท ซีเอสไอ ประเทศไทย จำกัด";
            ViewBag.companyName2 = "บริษัท ซีเอสลอกอินโฟ จำกัด";
            ViewBag.companyName3 = "บริษัท ลานนาคอม จำกัด";
            ViewBag.companyName4 = "บริษัท ซีพีออลล์ จำกัด";
            ViewBag.companyName5 = "คณะพยาบาลศาสตร์ มหาวิทยาลัยเชียงใหม่";
            ViewBag.numberOfStudent1 = "6";
            ViewBag.numberOfStudent2 = "6";
            ViewBag.numberOfStudent3 = "6";
            ViewBag.numberOfStudent4 = "5";
            ViewBag.numberOfStudent5 = "5";

            ViewBag.studentName1 = "จักรฤษณ์ บุญเตร";
            ViewBag.studentName2 = "ไปไหน ไม่รู้";
            ViewBag.studentName3 = "ไม่รู้ ไปไหน";
            ViewBag.studentName4 = "ไปรู้ ไหนไม่";
            ViewBag.studentStatus1 = "สหกิจ บริษัทเทรดิชัน จำกัด";
            ViewBag.studentStatus2 = "-";
            ViewBag.studentStatus3 = "รอสัมภาษณ์";
            ViewBag.studentStatus4 = "-";
            return View();
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }

    }
}