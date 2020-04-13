using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyCustomerService.Models;

namespace MyCustomerService.Services.Interfaces
{
    public interface IStatesService
    {
        Task<List<State>> GetStatesAsync();
    }
}
