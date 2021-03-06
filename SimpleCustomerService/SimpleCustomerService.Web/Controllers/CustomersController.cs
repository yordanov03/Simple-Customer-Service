﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCustomerService.Models;
using SimpleCustomerService.Services.interfaces;
using SimpleCustomerService.Web.Infrastructure;

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
        [NoCache]
        [ProducesResponseType(typeof(List<Customer>), 200)]
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

        [HttpGet("{id}", Name = "GetCustomerRoute")]
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
                    return CreatedAtRoute("GetCustomerRoute", new {id = newCustomer.Id});
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpGet]
        [Route("page/{skip}/{take}")]
        public async Task<IActionResult> GetCustomersPaged(int take, int skip)
        {
            try
            {
                var result = await _customerService.GetCustomersPaged(take, skip);
                Response.Headers.Add("X-InlineCount", result.TotalPages.ToString());
                return Ok(result.Customers);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
