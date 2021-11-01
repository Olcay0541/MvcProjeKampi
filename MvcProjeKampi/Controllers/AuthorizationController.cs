using BusinessLayer.Abstract;
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
    public class AuthorizationController : Controller
    {
        IAuthorizationService authService = new AuthorizationManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        RoleManager roleManager = new RoleManager(new EfRoleDal());
        // GET: Authorization
        public ActionResult Index()
        {
            var adminvalues = adminManager.GetList();
            return View(adminvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> valueadminrole = (from c in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();
            ViewBag.valueadmin = valueadminrole;
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            authService.Register(p.AdminUserName, p.AdminPassword);
            return RedirectToAction("Index");
            //adminManager.AdminAddBL(p);
            //return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> valueadminrole = (from c in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();

            ViewBag.valueadmin = valueadminrole;
            var result = adminManager.GetByID(id);
            return View(result);
            //var adminvalue = adminManager.GetByID(id);
            //return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            adminManager.AdminUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAdmin(int id)
        {
            var result = adminManager.GetByID(id);
            if (result.AdminStatus == true)
            {
                result.AdminStatus = false;
            }
            else
            {
                result.AdminStatus = true;
            }
            adminManager.AdminUpdate(result);
            return RedirectToAction("Index");
        }
    }
}