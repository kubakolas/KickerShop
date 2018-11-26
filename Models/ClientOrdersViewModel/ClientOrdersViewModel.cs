using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KickerShop.Models
{
     public class OrderPayment
     {
        public Orders order { get; set; }
        public Payments payment { get; set; }
     }
    public class ClientOrdersViewModel
    {
        public Clients client{ get; set; }
        public IEnumerable<OrderPayment> orderPayment { get; set; }
    }
}