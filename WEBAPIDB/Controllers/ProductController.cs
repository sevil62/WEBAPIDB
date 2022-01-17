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
    public class ProductController : ApiController
    {
        NorthwindEntities _db;
        public ProductController()
        {
            _db = DBTool.DBInstance;
        }

        [HttpGet]
        public List<ProductDTO> ListProducts()
        {
            return _db.Products.Select(x => new ProductDTO{

                ID=x.ProductID,
                ProductName=x.ProductName,
                UnitPrice=(decimal)x.UnitPrice


            }).ToList();
            
        }

        [HttpGet]
        public ProductDTO BringProduct(int id)
        {
            return _db.Products.Where(x => x.ProductID == id).Select(x => new ProductDTO
            {
                ID = x.ProductID,
                ProductName = x.ProductName,
                UnitPrice = (decimal)(x.UnitPrice)
            }).FirstOrDefault();
        }
        [HttpPost]
        public List<ProductDTO>AddProduct(Product item)
        {
            _db.Products.Add(item);
            _db.SaveChanges();
            return ListProducts();
        }
        [HttpPut]
        public List<ProductDTO> UpdateProduct(Product item)
        {
            Product toBeUpdeted = _db.Products.Find(item.ProductID);
            _db.Entry(toBeUpdeted).CurrentValues.SetValues(item);
            _db.SaveChanges();
            return ListProducts();
        }

        [HttpDelete]
        public List<ProductDTO>DeleteProduct(int id)
        {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return ListProducts();
        }
        [HttpGet]
        public List<ProductDTO>SearchProduct(string item)
        {
            return _db.Products.Where(x => x.ProductName.Contains(item)).Select(x => new ProductDTO
            {
                ID=x.ProductID,
                ProductName=x.ProductName,
                UnitPrice=Convert.ToDecimal(x.UnitPrice)


            }).ToList();
        }
    }
}
