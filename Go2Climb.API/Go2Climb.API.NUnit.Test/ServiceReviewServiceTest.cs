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
    public class ServiceReviewServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoServiceReviewReturnsEmptyCollection()
        {
            //Arrange
            var mockServiceReviewRepository = GetDefaultIServiceReviewRepositoryInstance();
            mockServiceReviewRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<ServiceReview>());

            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            
            var service = new ServiceReviewService(mockServiceReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockServiceRepository.Object);
            
            //Act
            List<ServiceReview> result = (List<ServiceReview>) await service.ListAsync();
            var serviceCount = result.Count;
            
            //Assert
            serviceCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsServiceReviewNotFoundResponse()
        {
            //Arrange
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockServiceReviewRepository = GetDefaultIServiceReviewRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            
            var serviceReviewId = 1;
            mockServiceReviewRepository.Setup(r => r.FindByIdAsync(serviceReviewId))
                .Returns(Task.FromResult<ServiceReview>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new ServiceReviewService(mockServiceReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockServiceRepository.Object);
            //Act
            ServiceReviewResponse result = await service.GetByIdAsync(serviceReviewId);
            var message = result.Message;
            //Assert
            message.Should().Be("The service review does not exist.");
        }

        [Test]
        public async Task SavingWhenErrorReturnException()
        {
            //Arrange
            ServiceReview serviceReview = new ServiceReview() { };
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockServiceReviewRepository = GetDefaultIServiceReviewRepositoryInstance();
            mockServiceReviewRepository.Setup(u => u.AddAsync(serviceReview))
                .Throws(new Exception());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new ServiceReviewService(mockServiceReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockServiceRepository.Object);
            
            //Act
            ServiceReviewResponse response = await service.SaveAsync(serviceReview);
            var message = response.Message;
            
            //Assert
            message.Should().Contain("An error occurred while saving the service review");
        }

        [Test]
        public async Task DeleteAsyncWhenErrorReturnMessage()
        {
            //Arrange
            ServiceReview serviceReview = new ServiceReview { Id = 100, Date = "September 2021", Comment = "Love it", Score = 5};
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            var mockServiceReviewRepository = GetDefaultIServiceReviewRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockServiceReviewRepository.Setup(u => u.Remove(serviceReview))
                .Throws(new Exception());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new ServiceReviewService(mockServiceReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockServiceRepository.Object);
            
            //Act
            ServiceReviewResponse response = await service.DeleteAsync(100);
            var message = response.Message;
            
            //Assert
            message.Should().Be("Service review not found.");
        }
        private Mock<IServiceReviewRepository> GetDefaultIServiceReviewRepositoryInstance()
        {
            return new Mock<IServiceReviewRepository>();
        }

        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }
        private Mock<IServiceRepository> GetDefaultIServiceRepositoryInstance()
        {
            return new Mock<IServiceRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}