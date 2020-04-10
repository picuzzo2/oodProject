using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    [Route("ComSci")]
    public class ComSciController : Controller
    {
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
            return View();
        }
        [Route("Student")]
        public IActionResult Student()
        {
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
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "RegisLogin");
        }
    }
}