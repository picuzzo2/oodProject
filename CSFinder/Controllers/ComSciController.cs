using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSFinder.Models;
using Microsoft.AspNetCore.Http;

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