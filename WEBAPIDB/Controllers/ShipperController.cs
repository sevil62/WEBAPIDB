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
    public class ShipperController : ApiController
    {
        NorthwindEntities _db;
        public ShipperController()
        {
            _db = DBTool.DBInstance;
        }
        [HttpGet]
        public List<ShipperDTO> ListShippers()
        {
            return _db.Shippers.Select(x => new ShipperDTO
            {
                ShipperID=x.ShipperID,
                CompanyName=x.CompanyName,
                Phone=x.Phone


            }).ToList();
        }
        [HttpGet]
        public ShipperDTO BringShipper(int id)
        {
            return _db.Shippers.Where(x => x.ShipperID == id).Select(x => new ShipperDTO
            {
                ShipperID = x.ShipperID,
                CompanyName = x.CompanyName,
                Phone = x.Phone

            }).FirstOrDefault();
        }

        [HttpPost]
        public List<ShipperDTO>AddShipper(Shipper item)
        {
            _db.Shippers.Add(item);
            _db.SaveChanges();
            return ListShippers();
        }
        [HttpPut]
        public List<ShipperDTO>UpdateShipper(Shipper item)
        {
            Shipper toBeUpdated = _db.Shippers.Find(item.ShipperID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            _db.SaveChanges();
            return ListShippers();
        }
        

        [HttpDelete]
        public List<ShipperDTO>DeleteShipper(int id)
        {
            _db.Shippers.Remove(_db.Shippers.Find(id));
            _db.SaveChanges();
            return ListShippers();
        }
        [HttpGet]
        public List<ShipperDTO>SearchShipper(string item)
        {
            return _db.Shippers.Where(x => x.CompanyName.Contains(item)).Select(x => new ShipperDTO
            {
                ShipperID=x.ShipperID,
                CompanyName=x.CompanyName,
                Phone=x.Phone
            }).ToList();
        }
    }
}
