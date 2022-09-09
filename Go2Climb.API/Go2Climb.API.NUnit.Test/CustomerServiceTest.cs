using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Security.Authorization.Handlers.Interfaces;
using Go2Climb.API.Services;
using Moq;
using NUnit.Framework;

namespace Go2Climb.API.NUnit.Test
{
    public class CustomerServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoCustomerReturnsEmptyCollection()
        {
            //Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<Customer>());
            var mockJwtHandler = GetDefaultIJwtHandlerInstance();
            var mockMapper = GetDefaultIMapperInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object, mockJwtHandler.Object, mockMapper.Object);
            
            //Act
            List<Customer> result = (List<Customer>) await service.ListAsync();
            var customerCount = result.Count;
            
            //Assert
            customerCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsCustomerNotFoundResponse()
        {
            //Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var customerId = 1;
            mockCustomerRepository.Setup(r => r.FindByIdAsync(customerId))
                .Returns(Task.FromResult<Customer>(null));
            var mockJwtHandler = GetDefaultIJwtHandlerInstance();
            var mockMapper = GetDefaultIMapperInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object, mockJwtHandler.Object, mockMapper.Object);

            //Act
            CustomerResponse result = await service.FindById(customerId);
            var message = result.Message;
            
            //Assert
            message.Should().Be("Customer not found.");
        }
        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IJwtHandler> GetDefaultIJwtHandlerInstance()
        {
            return new Mock<IJwtHandler>();
        }

        private Mock<IMapper> GetDefaultIMapperInstance()
        {
            return new Mock<IMapper>();
        }
    }
}