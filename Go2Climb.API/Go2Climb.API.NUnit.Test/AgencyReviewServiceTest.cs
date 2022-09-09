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
    public class AgencyReviewServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoAgencyReviewReturnsEmptyCollection()
        {
            //Arrange
            var mockAgencyReviewRepository = GetDefaultIAgencyReviewRepositoryInstance();
            mockAgencyReviewRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<AgencyReview>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            
            var service = new AgencyReviewService(mockAgencyReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockAgencyRepository.Object);
            
            //Act
            List<AgencyReview> result = (List<AgencyReview>) await service.ListAsync();
            var agencyReviewCount = result.Count;
            
            //Assert
            agencyReviewCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncInvalidIdReturnsAgencyReviewNotFoundResponse()
        {
            //Arrange
            var mockAgencyReviewRepository = GetDefaultIAgencyReviewRepositoryInstance();
            var agencyReviewId = 1;
            mockAgencyReviewRepository.Setup(r => r.FindByIdAsync(agencyReviewId))
                .Returns(Task.FromResult<AgencyReview>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            
            var service = new AgencyReviewService(mockAgencyReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockAgencyRepository.Object);
            
            //Act
            AgencyReviewResponse result = await service.GetByIdAsync(agencyReviewId);
            var message = result.Message;
            
            //Assert
            message.Should().Be("The agency review does not exist.");
        }

        [Test]
        public async Task SavingWhenErrorReturnException()
        {
            //Arrange
            AgencyReview agencyReview = new AgencyReview() { };
            var mockAgencyReviewRepository = GetDefaultIAgencyReviewRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            mockAgencyReviewRepository.Setup(u => u.AddAsync(agencyReview))
                .Throws(new Exception());
            var service = new AgencyReviewService(mockAgencyReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockAgencyRepository.Object);

            //Act
            AgencyReviewResponse response = await service.SaveAsync(agencyReview);
            var message = response.Message;
            
            //Assert
            message.Should().Contain("Agency does not exist.");
        }

        [Test]
        public async Task DeleteAsyncWhenErrorReturnMessage()
        {
            //Arrange
            AgencyReview agencyReview = new AgencyReview
                {Id = 100, Date = "August 2021", Comment = "I had a good time", ProfessionalismScore = 5, SecurityScore = 4, QualityScore = 4, CostScore = 5};
            var mockAgencyReviewRepository = GetDefaultIAgencyReviewRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockAgencyReviewRepository.Setup(u => u.Remove(agencyReview))
                .Throws(new Exception());
            var service = new AgencyReviewService(mockAgencyReviewRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockAgencyRepository.Object);

            //Act
            AgencyReviewResponse response = await service.DeleteAsync(100);
            var message = response.Message;
            
            //Assert
            message.Should().Be("Agency review not found.");
        }
        private Mock<IAgencyReviewRepository> GetDefaultIAgencyReviewRepositoryInstance()
        {
            return new Mock<IAgencyReviewRepository>();
        }
        private Mock<IAgencyRepository> GetDefaultIAgencyRepositoryInstance()
        {
            return new Mock<IAgencyRepository>();
        }
        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}