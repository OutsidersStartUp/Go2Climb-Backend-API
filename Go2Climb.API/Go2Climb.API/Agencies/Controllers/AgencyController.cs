using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Agencies.Resources;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Resources;
using Go2Climb.API.Security.Authorization.Attributes;
using Go2Climb.API.Security.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Go2Climb.API.Agencies.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AgenciesController : ControllerBase
    {
        private readonly IAgencyService _agencyService;
        private readonly IMapper _mapper;

        public AgenciesController(IAgencyService agencyService, IMapper mapper)
        {
            _agencyService = agencyService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Agencies",
            Description = "Get All Agencies already stored",
            Tags = new[] {"Agencies"})]
        public async Task<IEnumerable<AgencyResource>> GetAllAsync()
        {
            var agencies = await _agencyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Agency>, IEnumerable<AgencyResource>>(agencies);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a agency by Id",
            Description = "Get a Agency Data already stored",
            Tags = new[] {"Agencies"})]
        public async Task<IActionResult> GetById(int id)
        {
            var agency = await _agencyService.GetByIdAsync(id);
            var resources = _mapper.Map<Agency, AgencyResource>(agency);
            return Ok(resources);
        }

        [AllowAnonymous]
        [HttpPost("auth/sign-up")]
        [SwaggerOperation(
            Summary = "Register a new agency",
            Description = "register a new agency in the database",
            Tags = new[] {"Agencies"})]
        public async Task<IActionResult> Register(SaveAgencyResource request)
        {
            await _agencyService.RegisterAsync(request);
            return Ok(new {message = "Registration successful"});
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit a agency",
            Description = "Updates the data of a stored agency given its id",
            Tags = new[] {"Agencies"})]
        public async Task<IActionResult> Update(int id, UpdateAgencyRequest request)
        {
            await _agencyService.UpdateAsync(id, request);
            return Ok(new {message = "Agency updated successfully"});
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a agency",
            Description = "Delete the data of a stored agency given its id",
            Tags = new[] {"Agencies"})]
        public async Task<IActionResult> Delete(int id)
        {
            await _agencyService.DeleteAsync(id);
            return Ok(new {message = "Agency deleted successfully"});
        }
    }
}