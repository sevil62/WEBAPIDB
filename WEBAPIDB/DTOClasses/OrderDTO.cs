using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPIDB.DTOClasses
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
    }
}