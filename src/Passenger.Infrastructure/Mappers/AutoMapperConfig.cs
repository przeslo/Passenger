﻿using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<User, UserDto>();
                 cfg.CreateMap<Route, RouteDto>();
                 cfg.CreateMap<Node, NodeDto>();
                 cfg.CreateMap<Driver, DriverDto>();
                 cfg.CreateMap<Driver, DriverDetailsDto>();
                 cfg.CreateMap<Vehicle, VehicleDto>();
             }).CreateMapper();

            return configuration;
        }
    }
}
