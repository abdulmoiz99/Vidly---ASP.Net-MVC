using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private dbContext _context;

        public CustomersController()
        {
            _context = new dbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ActionResult Index()
        {
            var customer = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customer);
        }
        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();
            var ViewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipType
            };
            return View("CustomerForm", ViewModel);
        }
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Details(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == Id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }
        public ActionResult Edit(int Id)
        {
            var customer = _context.Customers.Single(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}