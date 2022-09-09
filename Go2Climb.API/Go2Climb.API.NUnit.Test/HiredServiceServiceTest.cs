using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using FluentAssertions;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Services;
using Moq;
using NUnit.Framework;

namespace Go2Climb.API.NUnit.Test
{
    public class HiredServiceServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoHiredServiceReturnsEmptyCollection()
        {
            //Arrange
            var mockHiredServiceRepository = GetDefaultIHiredServiceRepositoryInstance();
            mockHiredServiceRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<HiredService>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new HiredServiceService(mockHiredServiceRepository.Object, mockUnitOfWork.Object);
            
            //Act
            List<HiredService> result = (List<HiredService>) await service.ListAsync();
            var hiredServiceCount = result.Count;
            
            //Assert
            hiredServiceCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsHiredServiceNotFoundResponse()
        {
            //Arrange
            var mockHiredServiceRepository = GetDefaultIHiredServiceRepositoryInstance();
            var hiredServiceId = 1;
            mockHiredServiceRepository.Setup(r => r.FindByIdAsync(hiredServiceId))
                .Returns(Task.FromResult<HiredService>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new HiredServiceService(mockHiredServiceRepository.Object, mockUnitOfWork.Object);
            
            //Act
            HideServiceResponse result = await service.FindById(hiredServiceId);
            var message = result.Message;
            
            //Assert
            message.Should().Be("Hired service not found.");
        }

        [Test]
        public async Task SavingWhenErrorReturnException()
        {
            //Arrange
            HiredService hiredService = new HiredService() { };
            var mockHiredServiceRepository = GetDefaultIHiredServiceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            mockHiredServiceRepository.Setup(u => u.AddAsync(hiredService))
                .Throws(new Exception());
            var service = new HiredServiceService(mockHiredServiceRepository.Object, mockUnitOfWork.Object);
            
            //Act
            HideServiceResponse response = await service.SaveAsync(hiredService);
            var message = response.Message;
            
            //Assert
            message.Should().Contain("An error occurred while registering the hired service");
        }

        [Test]
        public async Task DeleteAsyncWhenErrorReturnMessage()
        {
            //Arrange
            HiredService hiredService = new HiredService
                {Id = 100, Amount = 3, Price = 500, ScheduledDate = "20/11/2021", Status = "pending"};
            var mockHiredServiceRepository = GetDefaultIHiredServiceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            mockHiredServiceRepository.Setup(u => u.Remove(hiredService))
                .Throws(new Exception());
            var service = new HiredServiceService(mockHiredServiceRepository.Object, mockUnitOfWork.Object);
            
            //Act
            HideServiceResponse response = await service.DeleteAsync(100);
            var message = response.Message;
            
            //Assert
            message.Should().Be("Hired service not found.");
        }
        private Mock<IHiredServiceRepository> GetDefaultIHiredServiceRepositoryInstance()
        {
            return new Mock<IHiredServiceRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}