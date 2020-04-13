using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyCustomerService.Models;

namespace MyCustomerService.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
    }
}
