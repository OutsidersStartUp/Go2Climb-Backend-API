using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Go2Climb.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/customers/{customerId}/agencyreviews")]
    public class CustomerAgencyReviewsControllers : ControllerBase
    {
        private readonly IAgencyReviewService _agencyReviewService;
        private readonly IMapper _mapper;

        public CustomerAgencyReviewsControllers(IAgencyReviewService agencyReviewService, IMapper mapper)
        {
            _agencyReviewService = agencyReviewService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All AgencyReviews By Customer",
            Description = "Get All Reviews for a given CustomerId",
            Tags = new[] {"Customers"})]
        public async Task<IEnumerable<AgencyReviewResource>> GetAllByCustomerId(int customerId)
        {
            var agencyReviews = await _agencyReviewService.ListByCustomerIdAsync(customerId);
            var resources = _mapper
                .Map<IEnumerable<AgencyReview>, IEnumerable<AgencyReviewResource>>(agencyReviews);
            return resources;
        }
    }
}