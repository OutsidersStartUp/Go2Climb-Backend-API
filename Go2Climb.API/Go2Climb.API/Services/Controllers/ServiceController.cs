using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Extensions;
using Go2Climb.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Go2Climb.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServicesController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Get All Services",
            Description = "Get All Services already stored",
            Tags = new[] {"Services"})]
        [HttpGet]
        public async Task<IEnumerable<ServiceResource>> GetAllAsync()
        {
            var services = await _serviceService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get All Services",
            Description = "Get All Services already stored",
            Tags = new[] {"Services"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _serviceService.GetById(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        [HttpGet("[controller]/text/{text}")]
        [SwaggerOperation(
            Summary = "Get All Services By Text",
            Description = "Get All Services Of Coincided By Text",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ServiceResource>> ListByText(string text, int start, int limit)
        {
            var services = await _serviceService.ListByText(text, start, limit);
            var resources = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;
        }

        [HttpGet("[controller]/text/{text}/money")]
        [SwaggerOperation(
            Summary = "Get All Services By Text",
            Description = "Get All Services Of Coincided By Text",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ServiceResource>> ListByTextAndFilterMoney(string text, int min, int max,
            int start, int limit)
        {
            var services = await _serviceService.ListByTextAndFilterMoney(text, min, max, start, limit);
            var resources = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;
        }

        [HttpGet("[controller]/text/{text}/score")]
        [SwaggerOperation(
            Summary = "Get All Services By Text",
            Description = "Get All Services Of Coincided By Text",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ServiceResource>> ListByTextAndFilterScore(string text, int score, int start,
            int limit)
        {
            var services = await _serviceService.ListByTextAndFilterScore(text, score, start, limit);
            var resources = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;
        }

        [HttpGet("[controller]/text/{text}/score/{score}/money")]
        [SwaggerOperation(
            Summary = "Get All Services By Text",
            Description = "Get All Services Of Coincided By Text",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ServiceResource>> ListByTextAndAllFilter(string text, int min, int max, int score,
            int start, int limit)
        {
            var services = await _serviceService.ListByTextAndAllFilter(text, score, min, max, start, limit);
            var resources = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;
        }
        
        [HttpGet("[controller]/category")]
        [SwaggerOperation(
            Summary = "Get All Services",
            Description = "Get All Services already stored",
            Tags = new[] {"Services"})]
        public async Task<IEnumerable<ServiceResource>> FilterByCategories(string name, int start, int limit)
        {
            var services = await _serviceService.FilterByCategory(name, start, limit);
            var resources = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return resources;

        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Register a new service from an agency",
            Description = "Register a new service",
            Tags = new[] {"Services"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveServiceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var service = _mapper.Map<SaveServiceResource, Service>(resource);
            var result = await _serviceService.SaveAsync(service);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var serviceResource = _mapper.Map<Service, ServiceResource>(result.Resource);

            return Ok(serviceResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update a service from an agency",
            Description = "Update a service",
            Tags = new[] {"Services"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveServiceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var service = _mapper.Map<SaveServiceResource, Service>(resource);

            var result = await _serviceService.UpdateAsync(id, service);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var serviceResource = _mapper.Map<Service, ServiceResource>(result.Resource);

            return Ok(serviceResource);
        }
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Unregister a service from an agency",
            Description = "Delete a service",
            Tags = new[] {"Services"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _serviceService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var serviceResource = _mapper.Map<Service, ServiceResource>(result.Resource);
            
            return Ok(serviceResource);
        }
    }
}