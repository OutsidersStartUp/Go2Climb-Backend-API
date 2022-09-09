using System.Collections;
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
    [Route("api/v1/agencies/{agencyId}/services")]
    public class AgencyServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public AgencyServiceController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Services By Agency",
            Description = "Get All Services for a given AgencyId",
            Tags = new[] {"Agencies"})]
        public async Task<IEnumerable<ServiceResource>>GetAllByAgencyId(int agencyId)
        {
            var services = await _serviceService.ListByAgencyIdAsync(agencyId);
            var resources = _mapper
                .Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;
        }

        /*[HttpGet({"i"})]
        [SwaggerOperation(
            Summary = "Get Services with Offer By Agency",
            Description = "Get All Services with Offer for a given AgencyId",
            Tags = new[] {"Agencies"})]
        public async Task<IEnumerable<ServiceResource>> GetServiceWithOffByAgencyId(int agencyId)
        {
            var services = await _serviceService.ListServiceWithOfferByAgencyId(agencyId);
        }*/
    }
}