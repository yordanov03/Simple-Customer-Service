using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCustomerService.Data;
using SimpleCustomerService.Models;
using SimpleCustomerService.Services.interfaces;

namespace SimpleCustomerService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly SimpleCustomerServiceDbContext _context;

        public CustomerService(SimpleCustomerServiceDbContext context)
        {
            this._context = context;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                .SingleOrDefaultAsync(c => c.Id == id);
                _context.Remove(customer);

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.OrderBy(c => c.FirstName).Include(c => c.State).ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            Customer customer = await _context.Customers.Include(c => c.State).SingleOrDefaultAsync(c => c.Id == id);
            return customer;
        }
    }
}
