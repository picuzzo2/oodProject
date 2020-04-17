using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSFinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    public class PublicController : Controller
    {
        public CSFinderContext db;
        public PublicController(CSFinderContext _db)
        {
            db =_db;
        }
        public IActionResult GetStudent(string id)
        {
            var obj = db.Students.Where(x => x.SID == id).FirstOrDefault();
            ViewBag.obj = obj;
            ViewBag.userEmail = db.Accounts.Where(x => x.ID.Equals(obj.ID)).FirstOrDefault().Email;
            return View();
        }
        public IActionResult GetCompany(string id)
        {
            var obj = db.Companies.Where(x => x.CID == id).FirstOrDefault();
            
            List<PostCompany> cpost = new List<PostCompany>();
            foreach(Post p in db.Posts.OrderByDescending(x=>x.PID).Where(x => x.CID == obj.CID).ToList())
            {
                cpost.Add(new PostCompany( obj, p ));
            }
            ViewBag.obj = obj;
            ViewBag.userEmail = db.Accounts.Where(x => x.ID.Equals(obj.ID)).FirstOrDefault().Email;
            ViewBag.posts = cpost;
            return View();
        }

    }
}