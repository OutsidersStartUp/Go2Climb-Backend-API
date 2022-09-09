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
    [Route("/api/v1/agencies/{agencyId}/hiredservices")]
    public class AgencyHiredServicesController : ControllerBase
    {
        private readonly IHiredServiceService _hiredServiceService;
        private readonly IMapper _mapper;
        
        public AgencyHiredServicesController(IHiredServiceService hiredServiceService, IMapper mapper)
        {
            _hiredServiceService = hiredServiceService;
            _mapper = mapper;
        }
        
        [SwaggerOperation(
            Summary = "Get All Hired Services by Agency Id with Customer and Service Information",
            Description = "Get All Hired Services by Agency Id with Customer and Service Information if this exist in database",
            Tags = new[] {"Agencies"})]
        [HttpGet]
        public async Task<IEnumerable<HiredServiceResource>> GetHiredServicesByAgencyIdWithCustomerAndServiceInformation(
            int agencyId, string expand)
        {
            var hiredService = await _hiredServiceService.FindByAgencyIdAsync(agencyId);
            var resources = _mapper.Map<IEnumerable<HiredService>, IEnumerable<HiredServiceResource>>(hiredService);
            return resources;
        }
    }
}