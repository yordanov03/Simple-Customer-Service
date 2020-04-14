using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCustomerService.Models;
using SimpleCustomerService.Services.interfaces;

namespace SimpleCustomerService.Web.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService, ILoggerFactory loggerFactory)
        {
            _customerService = customerService;
            _logger = loggerFactory.CreateLogger(nameof(CustomerController));
        }

        [HttpGet]
        public async Task<IActionResult> Customers()
        {
            try
            {
                var customers = await _customerService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Customers(int id)
        {
            try
            {
                var customers = await _customerService.GetCustomer(id);
                return Ok(customers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newCustomer = await _customerService.CreateCustomer(customer);
                    return Ok(newCustomer);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newCustomer = await _customerService.UpdateCustomer(customer);
                    return Ok(newCustomer);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var deletedSuccessfully = await _customerService.DeleteCustomer(id);

                if (deletedSuccessfully)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
