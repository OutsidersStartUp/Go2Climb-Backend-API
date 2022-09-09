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
    [Route("api/v1/services/{serviceId}/activities")]
    public class ServiceActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ServiceActivityController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Activities By Service",
            Description = "Get All Activities for a given ServiceId",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ActivityResource>>GetAllByServiceId(int serviceId)
        {
            var activities = await _activityService.ListByServiceIdAsync(serviceId);
            var resources = _mapper
                .Map<IEnumerable<Activity>, IEnumerable<ActivityResource>>(activities);
            return resources;
        }
    }
}