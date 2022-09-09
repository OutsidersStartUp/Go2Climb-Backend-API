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
    public class ActivityServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoActivityReturnsEmptyCollection()
        {
            //Arrange
            var mockActivityRepository = GetDefaultIActivityRepositoryInstance();
            mockActivityRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<Activity>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            
            var service = new ActivityService(mockServiceRepository.Object, mockUnitOfWork.Object, mockActivityRepository.Object);
            
            //Act
            List<Activity> result = (List<Activity>) await service.ListAsync();
            var activityCount = result.Count;
            
            //Assert
            activityCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsActivityNotFoundResponse()
        {
            //Arrange
            var mockActivityRepository = GetDefaultIActivityRepositoryInstance();
            var activityId = 1;
            mockActivityRepository.Setup(r => r.FindById(activityId))
                .Returns(Task.FromResult<Activity>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            
            var service = new ActivityService(mockServiceRepository.Object, mockUnitOfWork.Object, mockActivityRepository.Object);
            
            //Act
            ActivityResponse result = await service.GetById(activityId);
            var message = result.Message;
            
            //Assert
            message.Should().Be("The activity does not exist.");
        }

        [Test]
        public async Task SavingWhenErrorReturnException()
        {
            //Arrange
            Activity activity = new Activity() { };
            var mockActivityRepository = GetDefaultIActivityRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            mockActivityRepository.Setup(u => u.AddAsync(activity))
                .Throws(new Exception());
            var service = new ActivityService(mockServiceRepository.Object, mockUnitOfWork.Object, mockActivityRepository.Object);

            //Act
            ActivityResponse response = await service.SaveAsync(activity);
            var message = response.Message;
            
            //Assert
            message.Should().Contain("An error occurred while saving the activity");
        }

        [Test]
        public async Task DeleteAsyncWhenErrorReturnMessage()
        {
            //Arrange
            Activity activity = new Activity
                {Id = 100, Name = "Activity Name", Description = "Activity Description"};
            var mockActivityRepository = GetDefaultIActivityRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockServiceRepository = GetDefaultIServiceRepositoryInstance();
            mockActivityRepository.Setup(u => u.Remove(activity))
                .Throws(new Exception());
            var service = new ActivityService(mockServiceRepository.Object, mockUnitOfWork.Object, mockActivityRepository.Object);

            //Act
            ActivityResponse response = await service.DeleteAsync(100);
            var message = response.Message;
            
            //Assert
            message.Should().Be("Activity not found");
        }
        private Mock<IServiceRepository> GetDefaultIServiceRepositoryInstance()
        {
            return new Mock<IServiceRepository>();
        }
        private Mock<IActivityRepository> GetDefaultIActivityRepositoryInstance()
        {
            return new Mock<IActivityRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}