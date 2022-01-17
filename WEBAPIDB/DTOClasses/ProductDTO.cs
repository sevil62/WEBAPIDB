using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPIDB.DTOClasses
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}