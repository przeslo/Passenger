﻿using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService : IService
    {
        Task<DriverDetailsDto> GetAsync(Guid userId);
        Task<IEnumerable<DriverDto>> GetAllAsync();
        Task CreateAsync(Guid userId);
        Task SetVehicleAsync(Guid userId, string brand, string name);
        Task DeleteAsync(Guid userId);
    }
}