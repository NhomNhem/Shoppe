using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoDB2.Models;
using Microsoft.Ajax.Utilities;
using PagedList;
using PagedList.Mvc;
namespace DemoDB2.Controllers
{
    public class ProductController : Controller
    {
        DBSportStoreEntities database = new DBSportStoreEntities(); 
        // GET: Product
        public ActionResult Index(string category, int? page,double min=double.MinValue,double max=double.MaxValue)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            if(category == null)
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro);
                return View(productList.ToPagedList(pageNum,pageSize));

            }
            else
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro)
                    .Where(x => x.Category == category);
                return View(productList);
            }
            
        }
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }

        public ActionResult SelectCate()
        {
            Category listItem = new Category();
            listItem.ListCate = database.Categories.ToList<Category>();
            return PartialView(listItem);
        }

        [HttpPost]
        public ActionResult Create(Product item)
        {
            try
            {
                if (item.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.UploadImage.FileName);
                    string extent = Path.GetExtension(item.UploadImage.FileName);
                    filename = filename + extent;
                    item.ImagePro = "~/Content/images/" + filename;
                    item.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.Products.Add(item);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SearchOption(double min=double.MinValue,double max= double.MaxValue)
        {
            var list = database.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
            return View(list);
        }


    }
}