using Microsoft.EntityFrameworkCore;
using SimpleCustomerService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCustomerService.Data
{
    public class SimpleCustomerServiceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Order> Orders { get; set; }


        public SimpleCustomerServiceDbContext(DbContextOptions<SimpleCustomerServiceDbContext> options) : base(options) { }
    }
}
