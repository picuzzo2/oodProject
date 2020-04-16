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
    public class RegisLoginController : Controller
    {
        private CSFinderContext db;
        public RegisLoginController(CSFinderContext _db)
        {
            db = _db;
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserID") != null) return RedirectToAction("UserDashBoard", "RegisLogin");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account objUser)
        {

            if (ModelState.IsValid)
            {

                var obj = db.Accounts.Where(a => a.ID.Equals(objUser.ID) && a.Password.Equals(objUser.Password)).FirstOrDefault();

                if (obj != null)
                {
                    HttpContext.Session.SetString("UserID", obj.ID.ToString());
                    HttpContext.Session.SetString("IDType", obj.IDtype.ToString());
                    return RedirectToAction("UserDashBoard");
                }

            }
            return View(objUser);
        }
        public IActionResult UserDashBoard()
        {
            string IDType = HttpContext.Session.GetString("IDType");
            if (IDType != null)
            {

                if (IDType == "Student")
                { 
                    return RedirectToAction("Index", "Student");
                }
                else if (IDType == "Company")
                {
                    return RedirectToAction("Home", "Company");

                }
                else if(IDType == "Admin")
                {
                    return RedirectToAction("Home", "ComSci");
                }

                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult RegisStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisStudent(StudentAccount objUser)
        {
            Debug.WriteLine(ModelState.IsValid);

            if (ModelState.IsValid)
            {
                String msg = "";
                //check dup acc
                //check dup sid
                //check dup email

                foreach (Student stu in db.Students)
                {
                    if (stu.ID.Equals(objUser.ID))
                    {
                        msg = "Username: " + objUser.ID + " already exist";
                        break;
                    }
                    else if (stu.SID.Equals(objUser.SID))
                    {
                        msg = "Student ID: " + objUser.SID + " already exist";
                        break;
                    }
                }
                if(msg =="")
                {
                    foreach(Account acc in db.Accounts)
                    {
                        if (acc.Email.Equals(objUser.Email))
                        {
                            msg = "Email: " + objUser.Email + " already exist";
                            break;
                        }
                    }
                }
                
                
                if(msg == "")
                {
                    Account addacc = new Account();
                    Student addstu = new Student();
                    addacc.ID = objUser.ID;
                    addacc.Password = objUser.Password;
                    addacc.IDtype = "Student";
                    addacc.Email = objUser.Email;
                    db.Accounts.Add(addacc);
                    db.SaveChanges();
                    addstu.ID = objUser.ID;
                    addstu.Name = objUser.Name;
                    addstu.SID = objUser.SID;
                    addstu.Phone = objUser.Phone;
                    addstu.Detail = objUser.Detail;
                    addstu.Address = objUser.Address;
                    if (objUser.Type.ToString() == "Internship")
                    {
                        addstu.Type = 0;
                    }
                    if (objUser.Type.ToString() == "Cooperative")
                    {
                        addstu.Type = 1;
                    }
                    db.Students.Add(addstu);
                    db.SaveChanges();

                    msg = "Register Success";

                    return Json(new { success = true, responseText = msg });
                }
                else
                {
                    return Json(new { success = false, responseText = msg });
                }
            }
            return View();

        }

        public IActionResult RegisCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisCompany(CompanyAccount objUser)
        {
            Debug.WriteLine(ModelState.IsValid);

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
                        if (acc.ID.Equals(objUser.ID))
                        {
                            msg = "Username: " + objUser.ID + " already exist";
                            break;
                        }
                        if (acc.Email.Equals(objUser.Email))
                        {
                            msg = "Email: " + objUser.Email + " already exist";
                            break;
                        }
                    }
                }


                if (msg == "")
                {
                    Account addacc = new Account();
                    Company addcom = new Company();

                    addacc.ID = objUser.ID;
                    addacc.Password = objUser.Password;
                    addacc.IDtype = "Company";
                    addacc.Email = objUser.Email;
                    

                    string LastCID = db.Companies.Max(p => p.CID); 

                    Debug.WriteLine("LastCid = " + LastCID);
                    if (LastCID == null)
                    {
                        addcom.CID = 1.ToString();
                    }
                    else
                    {
                        addcom.CID = (int.Parse(LastCID) + 1).ToString();
                    }
                    db.Accounts.Add(addacc);
                    db.SaveChanges();
                    addcom.ID = objUser.ID;
                    addcom.Name = objUser.Name;
                    addcom.Phone = objUser.Phone;
                    addcom.Detail = objUser.Detail;
                    addcom.Address = objUser.Address;                   
                    db.Companies.Add(addcom);
                    db.SaveChanges();


                    msg = "Register Success";

                    return Json(new { success = true, responseText = msg });
                }
                else
                {
                    return Json(new { success = false, responseText = msg });
                }
            }
            return View();
        }
    }
}