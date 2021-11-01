using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [Authorize]
    public class TalentCardController : Controller
    {
        TalentManager tm = new TalentManager(new EfTalentDal());
        // GET: TalentCard
        public ActionResult Index()
        {
            var talents = tm.GetAll();
            return View(talents);
        }
        [HttpGet]
        public ActionResult AddTalent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTalent(TalentCard p)
        {
            p.Name = "Olcay Kaymaz";
            p.About = "Yazılım yapmayı seviyorum";
            tm.Add(p);
            return RedirectToAction("Index");
        }
    }
}