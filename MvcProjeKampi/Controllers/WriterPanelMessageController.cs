using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        MessageValidator messagevalidator = new MessageValidator();
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactManager contactManager = new ContactManager(new EfContactDal());
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }
        public PartialViewResult MessageListMenu()
        {
            string parameter = (string)Session["WriterMail"];
            var contacts = contactManager.GetList().Count();
            ViewBag.contact = contacts;

            var result = mm.GetListSendbox(parameter).Count();
            ViewBag.result = result;

            var result2 = mm.GetListInbox(parameter).Count();
            ViewBag.result2 = result2;

            var draft = mm.GetAllDraft(parameter).Where(x => x.IsDraft == true).Count();
            ViewBag.draft = draft;

            var readMessage = mm.GetAllRead(parameter).Where(x => x.IsRead == true).Count();
            ViewBag.readMessage = readMessage;

            var unReadMessage = mm.GetAllUnRead(parameter).Count();
            ViewBag.unReadMessage = unReadMessage;
            return PartialView();
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
        public ActionResult NewMessage(Message message, string parameter)
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult result = messagevalidator.Validate(message);
            if (parameter == "send")
            {
                if (result.IsValid)
                {
                    message.SenderMail = sender;
                    message.IsDraft = false;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAddBL(message);
                    return RedirectToAction("SendBox");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (parameter == "draft")
            {
                if (result.IsValid)
                {
                    message.SenderMail = sender;
                    message.IsDraft = true;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAddBL(message);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }
        public ActionResult UnReadMessagePanel()
        {
            string parameter = (string)Session["WriterMail"];
            var unReadMessage = mm.GetAllUnRead(parameter);
            return View(unReadMessage);
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

        public ActionResult ReadMessagePanel()
        {
            string parameter = (string)Session["WriterMail"];
            var readMessage = mm.GetAllRead(parameter).Where(x => x.IsRead == true).ToList();
            return View(readMessage);
        }

        public ActionResult Draft()
        {
            string parameter = (string)Session["WriterMail"];
            var result = mm.GetAllDraft(parameter).Where(x => x.IsDraft == true).ToList();
            return View(result);
        }
    }
}