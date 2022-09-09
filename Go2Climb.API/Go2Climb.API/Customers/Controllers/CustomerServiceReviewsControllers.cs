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
    [Route("api/v1/customers/{customerId}/servicereviews")]
    public class CustomerServiceReviewsControllers : ControllerBase
    {
        private readonly IServiceReviewService _serviceReviewService;
        private readonly IMapper _mapper;

        public CustomerServiceReviewsControllers(IServiceReviewService serviceReviewService, IMapper mapper)
        {
            _serviceReviewService = serviceReviewService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All ServiceReviews By Customer",
            Description = "Get All ServiceReviews For A Given CustomerId",
            Tags = new[] {"Customers"})]
        public async Task<IEnumerable<ServiceReviewResource>>GetAllByServiceId(int customerId)
        {
            var servicereviews = await _serviceReviewService.ListByCustomerIdAsync(customerId);
            var resources = _mapper
                .Map<IEnumerable<ServiceReview>, IEnumerable<ServiceReviewResource>>(servicereviews);
            return resources;
        }
    }
    
}