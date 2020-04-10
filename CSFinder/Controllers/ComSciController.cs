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
    [Route("ComSci")]
    public class ComSciController : Controller
    {
        private CSFinderContext db;
        public ComSciController(CSFinderContext _db)
        {
            db = _db;
        }


        [Route("Home")]
        public IActionResult Home()
        {
            ViewBag.studentName = "Earn";
            ViewBag.kuy = "kuy";
            return View();
        }
        [Route("Company")]
        public IActionResult Company()
        {
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
        [Route("Student")]
        public IActionResult Student()
        {
            return View();
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }

    }
}