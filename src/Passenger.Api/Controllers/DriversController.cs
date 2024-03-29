﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Logger.Info("Fetching drivers.");
            var drivers = await _driverService.GetAllAsync();

            return Json(drivers);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var driver = await _driverService.GetAsync(userId);
            if (driver == null)
            {
                return NotFound();
            }

            return Json(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDriver command)
        {
            await DispatchAsync(command);

            return Created($"drivers/{command.UserId}", null);
        }

        [HttpPut("me")]
        public async Task<IActionResult> Put(UpdateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new DeleteDriver());

            return NoContent();
        }
    }
}