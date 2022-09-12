using Microsoft.EntityFrameworkCore;
using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RetailShopWebAPI.Repositories.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().Property(p => p.MSRP).HasColumnType("decimal(10,2)");
            
            builder.Entity<Order>().Property(p => p.Paid).HasColumnType("decimal(10,2)");
            
            builder.Entity<OrderDetail>().Property(p => p.Price).HasColumnType("decimal(10,2)");
            builder.Entity<OrderDetail>().Property(p => p.Discount).HasColumnType("decimal(10,2)");
            builder.Entity<OrderDetail>().Property(p => p.TotalAmount).HasColumnType("decimal(10,2)");

            builder.Entity<Order>().HasOne(p => p.Address).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<Order>().HasOne(p => p.Customer).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<OrderDetail>().HasOne(od => od.Product).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
