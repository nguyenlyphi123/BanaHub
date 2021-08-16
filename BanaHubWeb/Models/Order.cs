using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanaHubWeb.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime DayOrder { get; set; }
        public int cusID { get; set; }
        public Nullable<long> Sum { get; set; }
        public Nullable<int> Count { get; set; }
        public long Price { get; set; }

   
    }
}