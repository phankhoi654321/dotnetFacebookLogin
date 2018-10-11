using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWithAuthentication.Models;
using WebWithAuthentication.ViewModels;

namespace WebWithAuthentication.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }


        public ActionResult CustomerForm()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()       //this will make default value like Id = 0;

            };

            return View(viewModel);
        }

        [HttpPost] //Only post
        [ValidateAntiForgeryToken]  //Check must be post with value token
        public ActionResult Save(Customer customer)  //model binding
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);   //This save on memory not on database, after that mus be SaveChange()
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                /*
                TryUpdateModel(customerInDb);   //this way will open the security hall and check all properties of Model
                TryUpdateModel(customerInDb, "", new string[]{"Name", "Email"}); 
                //this way change properties Name and Email, but later if you change the properties on model class must be manual fix in here
                */

                //Mapper.Map(customer, customerInDb); //Install-Package AutoMapper
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();             //Must be use it to save on database


            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Detail(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
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

        public ActionResult Delete(int id)
        {
            var customerDelete = _context.Customers.SingleOrDefault(c => c.Id == id);
            _context.Customers.Remove(customerDelete);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }


    }
}