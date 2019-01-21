
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

    public partial class Clients
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Clients()
    {

        this.Orders = new HashSet<Orders>();

    }

        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        [MinLength(2, ErrorMessage = "Minimum name length is 3")]
        [MaxLength(40, ErrorMessage = "Maximum name length is 40")]
        [RegularExpression(@"[a-zA-Z]{2}[a-zA-Z ]*", ErrorMessage = "Invalid name")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Minimum street length is 3")]
        [MaxLength(40, ErrorMessage = "Maximum street length is 40")]
        [RegularExpression(@"[a-zA-Z]{2}[a-zA-Z ]*", ErrorMessage = "Invalid street name")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [MinLength(2, ErrorMessage = "Minimum city length is 3")]
        [MaxLength(40, ErrorMessage = "Maximum city length is 40")]
        public string City { get; set; }
        [RegularExpression(@"[0-9]{5}", ErrorMessage = "Invalid ZIP code")]
        public string Zip { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Orders> Orders { get; set; }

}

}
