using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Security.Authorization.Handlers.Interfaces;
using Go2Climb.API.Services;
using Moq;
using NUnit.Framework;

namespace Go2Climb.API.NUnit.Test
{
    public class AgencyServiceTest
    {
        [Test]
        public async Task ListAsyncWhenNoAgencyReturnsEmptyCollection()
        {
            //Arrange
            var mockAgencyRepository = GetDefaultIAgencyRepositoryInstance();
            mockAgencyRepository.Setup(u => u.ListAsync())
                .ReturnsAsync(new List<Agency>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockJwtHandler = GetDefaultIJwtHandlerInstance();
            var mockMapper = GetDefaultIMapperInstance();
            var service = new AgencyService(mockAgencyRepository.Object, mockUnitOfWork.Object, mockJwtHandler.Object, mockMapper.Object);
            
            //Act
            List<Agency> result = (List<Agency>) await service.ListAsync();
            var agencyCount = result.Count;
            
            //Assert
            agencyCount.Should().Equals(0);
        }
        private Mock<IAgencyRepository> GetDefaultIAgencyRepositoryInstance()
        {
            return new Mock<IAgencyRepository>();
        }
        private Mock<IJwtHandler> GetDefaultIJwtHandlerInstance()
        {
            return new Mock<IJwtHandler>();
        }
        private Mock<IMapper> GetDefaultIMapperInstance()
        {
            return new Mock<IMapper>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}