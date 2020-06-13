﻿using AutoMapper;
using Passenger.Core.Models;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserDto>(user);

            //return new UserDto()
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    Username = user.Username,
            //    Fullname = user.Fullname
            //};
        }

        public async Task RegisterAsync(string email, string username, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email '{email}' already exists.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, username, password, salt);
            await _userRepository.AddAsync(user);
        }
    }
}
