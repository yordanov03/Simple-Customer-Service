using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCustomerService.Models
{
    public class PagingResult<T>
    {

        public IEnumerable<T> Customers { get; set; }
        public int TotalPages { get; set; }

        public PagingResult(IEnumerable<T> customers, int totalPages)
        {
            this.Customers = customers;
            this.TotalPages = totalPages;
        }
    }
}
