using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBAPIDB.DesignPatterns.SingletonPattern;
using WEBAPIDB.DTOClasses;
using WEBAPIDB.Models;

namespace WEBAPIDB.Controllers
{
    public class CustomerController : ApiController
    {
        NorthwindEntities _db;
        public CustomerController()
        {
            _db = DBTool.DBInstance;
        }
        [HttpGet]
        public List<CustomerDTO> ListCustomers()
        {
            return _db.Customers.Select(x => new CustomerDTO
            {
                ID = x.CustomerID,
                CompanyName=x.CompanyName,
                ContactName=x.ContactName

            }).ToList();
        }
        [HttpGet]
        public CustomerDTO BringCustomer(string id)
        {
            return _db.Customers.Where(x => x.CustomerID == id).Select(x => new CustomerDTO
            {
                ID=x.CustomerID,
                CompanyName=x.CompanyName,
                ContactName=x.ContactName


            }).FirstOrDefault();
        }
        [HttpPost]
        public List<CustomerDTO>AddCustomer(Customer item)
        {
            _db.Customers.Add(item);
            _db.SaveChanges();
            return ListCustomers();
        }
        [HttpPut]
        public List<CustomerDTO>UpdatedCustomer(Customer item)
        {
            Customer toBeUpdated = _db.Customers.Find(item.CustomerID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            _db.SaveChanges();
            return ListCustomers();
        }
        [HttpDelete]
        public List<CustomerDTO> DeleteCustomer(string id)
        {
            _db.Customers.Remove(_db.Customers.Find(id));
            _db.SaveChanges();
            return ListCustomers();

        }
        [HttpGet]
        public List<CustomerDTO>SearchCustomer(string item)
        {
            return _db.Customers.Where(x => x.CompanyName.Contains(item)).Select(x => new CustomerDTO
            {
                ID=x.CustomerID,
                CompanyName=x.CompanyName,
                ContactName=x.ContactName

            }).ToList();
        }
    }
}
