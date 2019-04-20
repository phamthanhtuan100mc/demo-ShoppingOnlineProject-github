using Microsoft.AspNet.Identity.EntityFramework;
using OnlineShoppingProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShoppingProject.DAO
{
    //public class DbShoppingContext : DbContext
    public class DbShoppingContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        //public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrdersDetail> OrdersDetails { get; set; }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbShoppingContext() : base("name=shoppingConnectionString")
        {

        }

        public System.Data.Entity.DbSet<OnlineShoppingProject.Areas.AdministratorCP.Models.SPThongKe> SPThongKes { get; set; }
    }
}