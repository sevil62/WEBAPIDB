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
    public class OrderController : ApiController
    {
        NorthwindEntities _db;
        public OrderController()
        {
            _db = DBTool.DBInstance;
        }
        [HttpGet]
        public List<OrderDTO> ListOrders()
        {
            return _db.Orders.Select(x => new OrderDTO
            {
                ID = x.OrderID,
                ShipName = x.ShipName,
                ShipAddress = x.ShipAddress
            }).ToList();
        }
        [HttpGet]
        public OrderDTO BringOrder(int id)
        {
            return _db.Orders.Where(x => x.OrderID == id).Select(x => new OrderDTO
            {
                ID=x.OrderID,
                ShipName=x.ShipName,
                ShipAddress=x.ShipAddress


            }).FirstOrDefault();
        }
        [HttpPost]
        public List<OrderDTO>AddOrder(Order item)
        {
            _db.Orders.Add(item);
            _db.SaveChanges();
            return ListOrders();
        }
        [HttpPut]
        public List<OrderDTO>UpdateOrder(Order item)
        {
            Order toBeUpdated = _db.Orders.Find(item.OrderID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            _db.SaveChanges();
            return ListOrders();

        }
        [HttpDelete]
        public List<OrderDTO>DeleteOrder(int id)
        {
            _db.Orders.Remove(_db.Orders.Find(id));
            _db.SaveChanges();
            return ListOrders();
        }
        [HttpGet]
        public List<OrderDTO>SearchOrder(string item)
        {
            return _db.Orders.Where(x => x.ShipName.Contains(item)).Select(x => new OrderDTO
            {
                ID = x.OrderID,
                ShipName = x.ShipName,
                ShipAddress = x.ShipAddress
            }).ToList();
        }
    }

}
