using DemoDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing.Text;
using System.Xml.Linq;

namespace DemoDB2.Controllers
{
    public class LoginCustomerController : Controller
    {
        // GET: LoginCustomer
        DBSportStoreEntities database = new DBSportStoreEntities();
        public ActionResult Show()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                database.Customers.Add(customer);
                database.SaveChanges();
                return RedirectToAction("Show");
            }
            catch
            {
                return Content("Lỗi tạo thông tin khách hàng - Xin thử lại...Cảm ơn!");
            }
        }
    }
}