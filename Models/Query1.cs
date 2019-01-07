using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KickerShop.Models
{
    public class Query1
    {

       public DateTime OrderDate { get; set; }
        public int Id { get; set; }
        public int OrderDetail_id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Query1(DateTime orderDate, int id, int orderDetail_id, string name, int quantity)
        {
            OrderDate = orderDate;
            Id = id;
            OrderDetail_id = orderDetail_id;
            Name = name;
            Quantity = quantity;
        }
    }
}