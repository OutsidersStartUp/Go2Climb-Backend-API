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
    [Route("api/v1/customers/{customerId}/hiredservices")]
    public class CustomerHiredServicesControllers : ControllerBase
    {
        private readonly IHiredServiceService _hiredServiceService;
        private readonly IMapper _mapper;

        public CustomerHiredServicesControllers(IHiredServiceService hiredServiceService, IMapper mapper)
        {
            _hiredServiceService = hiredServiceService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All HiredServices By Customer",
            Description = "Get All HiredServices For A Given CustomerId",
            Tags = new[] {"Customers"})]
        public async Task<IEnumerable<HiredServiceResource>> GetAllByCustomerId(int customerId)
        {
            var hiredService = await _hiredServiceService.FindByCustomerIdAsync(customerId);
            var resources = _mapper
                .Map<IEnumerable<HiredService>, IEnumerable<HiredServiceResource>>(hiredService);
            return resources;
        }
        
        [HttpGet("information")]
        [SwaggerOperation(
            Summary = "Get All Hired Services by Agency Id with Service Information",
            Description = "Get All Hired Services by Customer Id with Service Information if this exist in database",
            Tags = new[] {"Customers"})]
        public async Task<IEnumerable<HiredServiceResource>> GetByCustomerIdWithServiceInformation(int customerId, string expand)
        {
            var hiredService = await _hiredServiceService.FindByCustomerIdWithServiceInformationAsync(customerId);
            var resources = _mapper
                .Map<IEnumerable<HiredService>, IEnumerable<HiredServiceResource>>(hiredService);
            return resources;
        }
    }
    
}