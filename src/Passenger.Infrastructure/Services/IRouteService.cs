﻿using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IRouteService : IService
    {
        Task<string> GetAddressAsync(double latitude, double longitude);
        double CalculateDistance(double startLatitude, double startLongitude,
            double endLatitude, double endLongitude);
    }
}