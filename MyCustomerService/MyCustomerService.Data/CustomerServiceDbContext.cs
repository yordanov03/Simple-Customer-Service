using System;
using Microsoft.EntityFrameworkCore;
using MyCustomerService.Models;

namespace MyCustomerService.Data
{
    public class CustomerServiceDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Order> Orders { get; set; }


        public CustomerServiceDbContext (DbContextOptions<CustomerServiceDbContext> options) : base(options) { }
    }
}
