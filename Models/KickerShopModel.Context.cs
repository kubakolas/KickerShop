﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KickerShopEntities : DbContext
    {
        public KickerShopEntities()
            : base("name=KickerShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> ClientSet { get; set; }
        public virtual DbSet<Delivery_type> Delivery_typeSet { get; set; }
        public virtual DbSet<Order> OrderSet { get; set; }
        public virtual DbSet<Payment> PaymentSet { get; set; }
        public virtual DbSet<Producer> ProducerSet { get; set; }
        public virtual DbSet<Product> ProductSet { get; set; }
        public virtual DbSet<Warehouse> WarehouseSet { get; set; }
    }
}
