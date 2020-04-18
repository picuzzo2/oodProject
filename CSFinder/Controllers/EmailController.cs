using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CSFinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSFinder.Controllers
{
    public class EmailController : Controller
    {
        CSFinderContext db;
        string username,password;
        public EmailController(CSFinderContext _db)
        {
            db = _db;
            username = "csfinder.ood@gmail.com";
            password = "csfinder204";
        }
        public IActionResult SendEmailTo(string from, string to,string fromName, string toName,  string detail, string subject)
        {
            string result = "";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(username, password);

            string emailBody = "<p> Dear " + toName + ": <br> This is an automated email from CSFinder project, Here is the Message from "+fromName+"<br> <br>" + detail + "<br> <br> Regards CSFinder Team </p> ";
            try
            {
                MailMessage mail = new MailMessage(username, to, subject, emailBody);
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mail);
                result = "Email had been send";
            }
            catch (Exception ex)
            {
                result = "Can't sent email to : " + to;
            }
            return Json(result);
        }

        public List<string> SendEmailToCompany(string detail)
        {
            List<string> errorList = new List<string>();
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(username,password);

                string subject = "Computer Science Chiangmai University Announcement";
                foreach (var acc in db.Companies.ToList())
                {
                    string emailBody = "<p> Dear " + acc.Name + ": <br> This is an automated email from CSFinder project, Here is the Message <br> <br>" + detail + "<br> <br> Regards CSFinder Team </p> ";
                    string comEmail = db.Accounts.Where(x => x.ID == acc.ID).FirstOrDefault().Email;
                    try
                    {
                        MailMessage mail = new MailMessage(username, comEmail, subject, emailBody);
                        mail.IsBodyHtml = true;
                        mail.BodyEncoding = UTF8Encoding.UTF8;
                        client.Send(mail);
                    }
                    catch(Exception ex)
                    {
                        errorList.Add("Can't sent email to user : "+comEmail);
                    }
                }
            }
            catch(Exception ex)
            {
                errorList.Add(ex.ToString());
            }
            return errorList;
        }
    }
}