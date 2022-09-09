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
    [Route("api/v1/services/{serviceId}/servicereviews")]
    public class ServiceServiceReviewsController : ControllerBase
    {
        private readonly IServiceReviewService _serviceReviewService;
        private readonly IMapper _mapper;

        public ServiceServiceReviewsController(IServiceReviewService serviceReviewService, IMapper mapper)
        {
            _serviceReviewService = serviceReviewService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All ServiceReviews By Service",
            Description = "Get All Reviews for a given ServiceId",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ServiceReviewResource>>GetAllByServiceId(int serviceId)
        {
            var servicereviews = await _serviceReviewService.ListByServiceIdAsync(serviceId);
            var resources = _mapper
                .Map<IEnumerable<ServiceReview>, IEnumerable<ServiceReviewResource>>(servicereviews);
            return resources;
        }
    }
}