using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        OrderContext db = new OrderContext();
        public ActionResult Index()
        {
            return View(db.Orders);
        }
        public ActionResult IndexCustomer()
        {
            return View(db.Customers);
        }
        public ActionResult DetailsOrder(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditOrder(Order order)
        {
            if (string.IsNullOrEmpty(order.Title))
            {
                ModelState.AddModelError(nameof(order.Title), "Введите название!");
            }
            if (order.Price <= 0)
            {
                ModelState.AddModelError(nameof(order.Price), "Нельзя вводить отрицательную цену!");
            }
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(order);
            }

        }
        [HttpGet]
        public ActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            if (order.Customer_Id < 1)
            {
                ModelState.AddModelError(nameof(order.Customer_Id), "id не может быть отрицательным!");
            }
            if (db.Customers.FirstOrDefault(x => x.Customer_Id == order.Customer_Id) == null)
            {
                ModelState.AddModelError(nameof(order.Customer_Id), "Введен несуществующий id!");
            }
            if (string.IsNullOrEmpty(order.Title))
            {
                ModelState.AddModelError(nameof(order.Title), "Введите название!");
            }
            if (order.Price <= 0)
            {
                ModelState.AddModelError(nameof(order.Price), "Нельзя вводить отрицательную цену!");
            }
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(order);
            }
        }
        public ActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            return RedirectToAction("IndexCustomer");
        }
        [HttpGet]
        public ActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.SNP))
            {
                ModelState.AddModelError(nameof(customer.SNP), "Введите ФИО!");
            }
            if (string.IsNullOrEmpty(customer.Address))
            {
                ModelState.AddModelError(nameof(customer.Address), "Введите адрес!");
            }
            if (string.IsNullOrEmpty(customer.Telephone))
            {
                ModelState.AddModelError(nameof(customer.Telephone), "Введите телефон!");
            }
            else if (!string.IsNullOrEmpty(customer.Telephone))
            {
                Regex regex = new Regex(@"\+\d{1}-\d{3}-\d{3}-\d{2}-\d{2}");
                Match match = regex.Match(customer.Telephone);
                if (!match.Success)
                {
                    ModelState.AddModelError(nameof(customer.Telephone), "Неверный формат телефона!");
                }
            }
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("IndexCustomer");
            }
            else
            {
                return View(customer);
            }
        }
        public ActionResult DetailsCustomer(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                return View(customer);
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult EditCustomer(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                return View(customer);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.SNP))
            {
                ModelState.AddModelError(nameof(customer.SNP), "Введите ФИО!");
            }
            if (string.IsNullOrEmpty(customer.Address))
            {
                ModelState.AddModelError(nameof(customer.Address), "Введите адрес!");
            }
            if (string.IsNullOrEmpty(customer.Telephone))
            {
                ModelState.AddModelError(nameof(customer.Telephone), "Введите телефон!");
            }
            else if (!string.IsNullOrEmpty(customer.Telephone))
            {
                Regex regex = new Regex(@"\+\d{1}-\d{3}-\d{3}-\d{2}-\d{2}");
                Match match = regex.Match(customer.Telephone);
                if (!match.Success)
                {
                    ModelState.AddModelError(nameof(customer.Telephone), "Неверный формат телефона!");
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexCustomer");
            }
            else
            {
                return View(customer);
            }
        }
        













        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}