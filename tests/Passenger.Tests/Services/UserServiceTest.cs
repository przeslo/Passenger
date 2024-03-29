﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;


namespace Passenger.Tests.Services
{
    public class UserServiceTest
    {
        [Test]
        public async Task UserServiceGetAsync_ShouldReturnUserDto_WhenUserExists()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);

            var user = new User(Guid.NewGuid(),"user@gmail.com", "user", "secret", "user", "salt");
            userRepositoryMock.Setup(x => x.GetAsync(user.Email))
                .ReturnsAsync(user);

            var userDto = new UserDto()
            {
                Email = user.Email,
                Username = user.Username,
                Fullname = user.Fullname,
                Id = user.Id
            };
            mapperMock.Setup(x => x.Map<User, UserDto>(user))
                .Returns(userDto);

            // Act
            userDto = await sut.GetAsync(user.Email);

            // Assert
            user.Email.Should().Be(userDto.Email);
            user.Username.Should().Be(userDto.Username);
            user.Fullname.Should().Be(userDto.Fullname);
            user.Id.Should().Be(userDto.Id);
        }

        [Test]
        public async Task UserServiceGetAsync_ShouldReturnNothing_WhenUserDoesNotExists()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync("invalid@user.com"))
                .ReturnsAsync(() => null);

            // Act
            var user = await sut.GetAsync("invalid@user.com");

            // Assert
            user.Should().BeNull();
        }

        [Test]
        public async Task UserServiceGetAsync_ShouldInvoke_UserRepositoryGetAsync()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);

            // Act
            await sut.GetAsync("email@email.com");

            // Assert
            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task UserServiceRegisterAsync_ShouldInvoke_UserRepositoryGetAsync()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);

            // Act
            await sut.RegisterAsync(Guid.NewGuid(),"email@email.com", "user", "secret", "user");

            // Assert
            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserServiceRegisterAsync_ShouldThrowException_WhenEmailAlreadyExist()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            var user = new User(Guid.NewGuid(),"email@email.com", "user", "secret", "user", "salt");
            userRepositoryMock.Setup(x => x.GetAsync(user.Email))
                .ReturnsAsync(user);

            // Act & Assert
            sut.Invoking(x => x.RegisterAsync(Guid.NewGuid(),user.Email, user.Username, user.Password, "user")).Should()
                .Throw<Exception>().WithMessage($"User with email '{user.Email}' already exist.");
        }

        [Test]
        public async Task UserServiceRegisterAsync_ShouldInvoke_UserRepositoryAddAsync()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);

            // Act
            await sut.RegisterAsync(Guid.NewGuid(),"email@email.com", "user", "secret", "user");

            // Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }

}
