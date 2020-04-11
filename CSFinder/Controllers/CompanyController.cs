
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Home()
        {
            ViewBag.companyName = "บริษัท เอ็ม เอฟ อี ซี จำกัด (มหาชน)";
            ViewBag.companyAddress = "699 อาคารโมเดอร์นฟอร์มทาวเวอร์ ชั้น 27 ถนนศรีนครินทร์ แขวงพัฒนาการ เขตสวนหลวง กรุงเทพมหานคร 10250";
            ViewBag.companyPhone = "+66 (0) 2821-7999";
            ViewBag.companyEmail = "sales@mfec.com";
            ViewBag.companyPostName = "บริษัท HandyWings";
            ViewBag.companyPostDetail = "บริษัท HandyWings เปิดรับสมัคร เพื่อนร่วมทีม เพิ่มเติมหลายอัตราเพื่อรองรับการขยายงาน";
            ViewBag.ProfileOtherCompanyPost = "https://sv1.picz.in.th/images/2020/04/10/QMv1uZ.jpg";
            ViewBag.Post1Img = "https://sv1.picz.in.th/images/2020/04/11/UWg8eR.jpg";
            ViewBag.PhotoOfOtherCompanyPost = "https://sv1.picz.in.th/images/2020/04/10/QMvKII.jpg";
            return View();
        }
        public IActionResult Notification()

        {
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

