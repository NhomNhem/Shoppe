using DemoDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DemoDB2.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        DBSportStoreEntities database = new DBSportStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(AdminUser info)
        {
            var check = database.AdminUsers.Where(s => s.ID == info.ID && s.PasswordUser == info.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = info.NameUser;
                return RedirectToAction("Index", "Product");
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(AdminUser info)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.AdminUsers.Where(s => s.ID == info.ID).FirstOrDefault();
                if (check_ID == null) // Chưa có ID
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.AdminUsers.Add(info);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "ID này đã tồn tại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }
    }
}