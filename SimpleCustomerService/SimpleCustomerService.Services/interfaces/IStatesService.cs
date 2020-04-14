using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleCustomerService.Models;

namespace SimpleCustomerService.Services.interfaces
{
    public interface IStatesService
    {
        Task<List<State>> GetStatesAsync();
    }
}
