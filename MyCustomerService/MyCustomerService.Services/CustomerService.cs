using MyCustomerService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCustomerService.Models;
using MyCustomerService.Services.Interfaces;

namespace MyCustomerService.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly CustomerServiceDbContext _context;

        public CustomerService(CustomerServiceDbContext context)
        {
            this._context = context;
        }

        public async Task <Customer> CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Customer searchedCustomer = await GetCustomer(customer.Id);
            searchedCustomer = customer;
            _context.Customers.Update(searchedCustomer);
           await  _context.SaveChangesAsync();
            return searchedCustomer;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            Customer customer =await  GetCustomer(id);
            _context.Customers.Remove(customer);
            return true;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.OrderBy(c=>c.FirstName). Include(c => c.State).ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            Customer customer = await _context.Customers.Include(c=>c.State).SingleOrDefaultAsync(c => c.Id == id);
            return customer;
        }
    }
}
