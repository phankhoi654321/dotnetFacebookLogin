using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebWithAuthentication.Models;
using System.Data.Entity;
using AutoMapper;
using WebWithAuthentication.Dtos;

namespace WebWithAuthentication.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // Get api/custormers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }

            var customerDtos = customersQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }
    }
}
