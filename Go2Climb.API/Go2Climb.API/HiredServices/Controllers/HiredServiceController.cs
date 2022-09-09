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
    [Route("api/v1/[controller]")]
    public class HiredServiceController : ControllerBase
    {
        private readonly IHiredServiceService _hiredServiceService;
        private readonly IMapper _mapper;

        public HiredServiceController(IHiredServiceService hiredServiceService, IMapper mapper)
        {
            _hiredServiceService = hiredServiceService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Hired Services",
            Description = "Get All Hired Services",
            Tags = new[] {"Hired-Services"})]
        public async Task<IEnumerable<HiredServiceResource>> GetAllAsync()
        {
            var services = await _hiredServiceService.ListAsync();
            var resources = _mapper.Map<IEnumerable<HiredService>, IEnumerable<HiredServiceResource>>(services);
            return resources;
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get A Hired Service By Id",
            Description = "Get A Hired Service Identified By Id",
            Tags = new[] {"Hired-Services"})]
        public async Task<HiredServiceResource> GetByIdAsync(int id)
        {
            var service = await _hiredServiceService.FindById(id);
            var resources = _mapper.Map<HiredService, HiredServiceResource>(service.Resource);
            return resources;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A Hired Service",
            Description = "Add A Hired Service To The Database.",
            Tags = new[] {"Hired-Services"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveHiredServiceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var hiredService = _mapper.Map<SaveHiredServiceResource, HiredService>(resource);
            
            var result = await _hiredServiceService.SaveAsync(hiredService);

            if (!result.Success)
                return BadRequest(result.Message);

            var serviceResource = _mapper.Map<HiredService, HiredServiceResource>(result.Resource);

            return Ok(serviceResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A Hired Service Identified By Id",
            Description = "Edit A Hired Service To The Database.",
            Tags = new[] {"Hired-Services"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveHiredServiceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var service = _mapper.Map<SaveHiredServiceResource, HiredService>(resource);

            var result = await _hiredServiceService.UpdateAsync(id, service);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var serviceResource = _mapper.Map<HiredService, HiredServiceResource>(result.Resource);

            return Ok(serviceResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A Hired Service Identified By Id",
            Description = "Delete A Hired Service To The Database.",
            Tags = new[] {"Hired-Services"})]
        public async Task<IActionResult> RemoteAsync(int id)
        {
            var result = await _hiredServiceService.DeleteAsync(id);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var serviceResource = _mapper.Map<HiredService, HiredServiceResource>(result.Resource);

            return Ok(serviceResource);
        }
    }
}