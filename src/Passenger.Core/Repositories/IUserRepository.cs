﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Core.Domain;

namespace Passenger.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(User user);

    }
}
