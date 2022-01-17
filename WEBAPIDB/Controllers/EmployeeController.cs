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
    public class EmployeeController : ApiController
    {
        NorthwindEntities _db;
        public EmployeeController()
        {
            _db = DBTool.DBInstance;
        }

        [HttpGet]
        public List<EmployeeDTO> ListEmployees()
        {
            return _db.Employees.Select(x => new EmployeeDTO
            {
                ID=x.EmployeeID,
                FirstName=x.FirstName,
                LastName=x.LastName

            }).ToList();
        }
        [HttpGet]
        public EmployeeDTO BringEmployee(int id)
        {
            return _db.Employees.Where(x => x.EmployeeID == id).Select(x => new EmployeeDTO
            {
                ID=x.EmployeeID,
                FirstName=x.FirstName,
                LastName=x.LastName

            }).FirstOrDefault();
        }
        [HttpPost]
        public List<EmployeeDTO>AddEmployee(Employee item)
        {
            _db.Employees.Add(item);
            _db.SaveChanges();
            return ListEmployees();
        }
        [HttpPut]
        public List<EmployeeDTO>UpdateEmployee(Employee item)
        {
            Employee toBeUpdated = _db.Employees.Find(item.EmployeeID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            _db.SaveChanges();
            return ListEmployees();
        }
        [HttpDelete]
        public List<EmployeeDTO>DeleteEmployee(int id)
        {
            _db.Employees.Remove(_db.Employees.Find(id));
            _db.SaveChanges();
            return ListEmployees();
        }
        [HttpGet]
        public List<EmployeeDTO>SearchEmployee(string item)
        {
            return _db.Employees.Where(x => x.FirstName.Contains(item)).Select(x => new EmployeeDTO
            {
                ID=x.EmployeeID,
                FirstName=x.FirstName,
                LastName=x.LastName
            }).ToList();
        }

    }
}
