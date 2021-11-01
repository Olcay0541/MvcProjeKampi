using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MessageValidator messagevalidator = new MessageValidator();
        MessageManager mm = new MessageManager(new EfMessageDal());

        [Authorize]
        public ActionResult Inbox(string p)
        {
            p = (string)Session["AdminUserName"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult Sendbox(string p)
        {
            p = (string)Session["AdminUserName"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult result = messagevalidator.Validate(p);
            if (result.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAddBL(p);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult Draft()
        {
            string parameter = (string)Session["AdminUserName"];
            var result = mm.GetListInbox(parameter).Where(x => x.IsDraft == true).ToList();
            return View(result);
        }
        public ActionResult IsRead(int id)
        {
            var result = mm.GetByID(id);
            if (result.IsRead == false)
            {
                result.IsRead = true;
            }
            mm.MessageUpdate(result);
            return RedirectToAction("ReadMessage");
        }

        public ActionResult ReadMessage()
        {
            string parameter = (string)Session["AdminUserName"];
            var readMessage = mm.GetAllRead(parameter).Where(x => x.IsRead == true).ToList();
            return View(readMessage);
        }

        public ActionResult UnReadMessage()
        {
            string parameter = (string)Session["AdminUserName"];
            var unReadMessage = mm.GetAllUnRead(parameter);
            return View(unReadMessage);
        }
    }
}