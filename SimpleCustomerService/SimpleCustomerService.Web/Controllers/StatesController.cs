﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCustomerService.Data;
using SimpleCustomerService.Services;
using SimpleCustomerService.Services.interfaces;

namespace SimpleCustomerService.Web.Controllers
{
    public class StatesController: Controller
    {
        private readonly IStatesService stateService;
        private ILogger logger;

        public StatesController(IStatesService stateService, SimpleCustomerServiceDbContext context, ILogger logger)
        {
            this.logger = logger;
            this.stateService = stateService;
        }

        [HttpGet]
        public async Task<ActionResult> States()
        {
            try
            {
                var states = await this.stateService.GetStatesAsync();
                return Ok(states);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
