using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCustomerService.Data;
using MyCustomerService.Models;
using MyCustomerService.Services.Interfaces;

namespace MyCustomerService.Services
{
    public class StatesService:IStatesService
    {

         private readonly CustomerServiceDbContext _context;

         public StatesService(CustomerServiceDbContext context)
         {
             _context = context;
         }

         public async Task<List<State>> GetStatesAsync()
         {
            return await _context.States.OrderBy(s => s.Abbreviation).ToListAsync();
        }
    }
}
