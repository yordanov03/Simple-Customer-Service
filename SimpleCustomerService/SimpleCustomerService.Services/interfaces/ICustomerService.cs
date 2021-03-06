﻿using SimpleCustomerService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCustomerService.Services.interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<PagingResult<Customer>> GetCustomersPaged(int take, int skip);
    }
}
