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
    public class ServiceServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoServiceReturnsEmptyCollection()
        {
            //Arrange
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            mockServiceRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<Service>());

            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            
            var service = new ServiceService(mockServiceRepository.Object, mockUnitOfWork.Object, mockAgencyRepository.Object);
            
            //Act
            List<Service> result = (List<Service>) await service.ListAsync();
            var serviceCount = result.Count;
            
            //Assert
            serviceCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsServiceNotFoundResponse()
        {
            //Arrange
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            var serviceId = 1;
            mockServiceRepository.Setup(r => r.FindById(serviceId))
                .Returns(Task.FromResult<Service>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new ServiceService(mockServiceRepository.Object, mockUnitOfWork.Object, mockAgencyRepository.Object);
            //Act
            ServiceResponse result = await service.GetById(serviceId);
            var message = result.Message;
            //Assert
            message.Should().Be("The service does not exist.");
        }

        [Test]
        public async Task SavingWhenErrorReturnException()
        {
            //Arrange
            Service service = new Service { };
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            mockServiceRepository.Setup(u => u.AddAsync(service))
                .Throws(new Exception());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var _service = new ServiceService(mockServiceRepository.Object, mockUnitOfWork.Object, mockAgencyRepository.Object);
            
            //Act
            ServiceResponse response = await _service.SaveAsync(service);
            var message = response.Message;
            
            //Assert
            message.Should().Contain("An error occurred while saving the Service");
        }

        [Test]
        public async Task DeleteAsyncWhenErrorReturnMessage()
        {
            //Arrange
            Service service = new Service { Id = 100, Name = "Service", Description = "Description", Price = 50, Location = "Location"};
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockAgencyServiceRepository = GetDefaultIAgencyRepositoryInstance();
            mockServiceRepository.Setup(u => u.Remove(service))
                .Throws(new Exception());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var _service = new ServiceService(mockServiceRepository.Object, mockUnitOfWork.Object, mockAgencyServiceRepository.Object);
            //Act
            ServiceResponse response = await _service.DeleteAsync(100);
            var message = response.Message;
            
            //Assert
            message.Should().Be("Service not found");
        }
        private Mock<IServiceRepository> GetDefaultIServiceRepositoryInstance()
        {
            return new Mock<IServiceRepository>();
        }

        private Mock<IAgencyRepository> GetDefaultIAgencyRepositoryInstance()
        {
            return new Mock<IAgencyRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}