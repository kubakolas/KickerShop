//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KickerShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {
        [Required(ErrorMessage = "Id can't be empty")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Client id can't be empty")]
        public int Cli_id { get; set; }
        [Required(ErrorMessage = "Delivery id can't be empty")]
        public int Del_id { get; set; }
        [Required(ErrorMessage = "Payment id can't be empty")]
        public int Pay_id { get; set; }
        [Required(ErrorMessage = "Product id can't be empty")]
        public int Pro_id { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Delivery_type Delivery_type { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Product Product { get; set; }
    }
}
