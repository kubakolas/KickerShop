using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KickerShop.Models
{
    public class ClientOrdersViewModel
    {
        public Client client{ get; set; }
        public IEnumerable<Order> orders { get; set; }
    }
}