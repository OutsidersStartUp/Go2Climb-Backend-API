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
    public class SubscriptionServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoSubscriptionReturnsEmptyCollection()
        {
            //Arrange
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            mockSubscriptionRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<Subscription>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new SubscriptionService(mockSubscriptionRepository.Object, mockUnitOfWork.Object);
            
            //Act
            List<Subscription> result = (List<Subscription>) await service.ListAsync();
            var subscriptionCount = result.Count;
            
            //Assert
            subscriptionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsSubscriptionNotFoundResponse()
        {
            //Arrange
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            var subscriptionId = 1;
            mockSubscriptionRepository.Setup(r => r.FindById(subscriptionId))
                .Returns(Task.FromResult<Subscription>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionService(mockSubscriptionRepository.Object, mockUnitOfWork.Object);
            //Act
            SubscriptionResponse result = await service.GetById(subscriptionId);
            var message = result.Message;
            //Assert
            message.Should().Be("The subscription does not exist");
        }

        [Test]
        public async Task SavingWhenErrorReturnException()
        {
            //Arrange
            Subscription subscription = new Subscription { };
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            mockSubscriptionRepository.Setup(u => u.AddAsync(subscription))
                .Throws(new Exception());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionService(mockSubscriptionRepository.Object, mockUnitOfWork.Object);
            
            //Act
            SubscriptionResponse response = await service.SaveAsync(subscription);
            var message = response.Message;
            
            //Assert
            message.Should().Contain("An error occurred while saving the Subscription");
        }

        [Test]
        public async Task DeleteAsyncWhenErrorReturnMessage()
        {
            //Arrange
            Subscription subscription = new Subscription { Id = 4, Name = "Freemium", Description = "Description", Price = 50};
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            mockSubscriptionRepository.Setup(u => u.Remove(subscription))
                .Throws(new Exception());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionService(mockSubscriptionRepository.Object, mockUnitOfWork.Object);
            //Act
            SubscriptionResponse response = await service.DeleteAsync(4);
            var message = response.Message;
            
            //Assert
            message.Should().Be("Subscription not found");
        }
        private Mock<ISubscriptionRepository> GetDefaultISubscriptionRepositoryInstance()
        {
            return new Mock<ISubscriptionRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}