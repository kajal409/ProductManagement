using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataAccess _context = new DataAccess();

        // GET: Product
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                var products = _context.Products.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (DataAccess db = new DataAccess())
                {
                    var obj = db.User.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Id.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Product");
                    }
                }
            }
            return View(objUser);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                return View(product);
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new HttpNotFoundResult();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new HttpNotFoundResult();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            try
            {
                if (product == null)
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }
    }
}
