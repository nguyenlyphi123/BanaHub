using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanaHubWeb.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public string cusName { get; set; }
        public string proName { get; set; }
        public int proNum { get; set; }
        public long Price { get; set; }
    }
}