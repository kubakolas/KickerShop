using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KickerShop.Models
{
    public class ClientOrdersViewModel
    {
        public Clients client{ get; set; }
        public IEnumerable<Orders> orders { get; set; }
        public Payments payment { get; set; }
    }
}