using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCustomerService.Data;
using SimpleCustomerService.Models;
using SimpleCustomerService.Services.interfaces;

namespace SimpleCustomerService.Services
{
    public class StatesService : IStatesService
    {

        private readonly SimpleCustomerServiceDbContext _context;

        public StatesService(SimpleCustomerServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<State>> GetStatesAsync()
        {
            return await _context.States.OrderBy(s => s.Abbreviation).ToListAsync();
        }
    }
}
