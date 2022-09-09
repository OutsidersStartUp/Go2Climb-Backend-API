using System;
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
    public class AgencyReviewsController : ControllerBase
    {
        private readonly IAgencyReviewService _agencyReviewService;
        private readonly IMapper _mapper;

        public AgencyReviewsController(IAgencyReviewService agencyReviewService, IMapper mapper)
        {
            _agencyReviewService = agencyReviewService;
            _mapper = mapper;
        }
        
        [SwaggerOperation(
            Summary = "Get All AgencyReviews",
            Description = "Get All AgencyReviews already stored",
            Tags = new[] {"AgencyReviews"})]
        [HttpGet]
        public async Task<IEnumerable<AgencyReviewResource>> GetAllAsync()
        {
            var agencyReviews = await _agencyReviewService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<AgencyReview>, IEnumerable<AgencyReviewResource>>(agencyReviews);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get a Agency Review by id",
            Description = "Get the agency review based on the id if it exists",
            Tags = new[] {"AgencyReviews"})]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _agencyReviewService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var agencyReviewResource = _mapper.Map<AgencyReview, AgencyReviewResource>(result.Resource);
            return Ok(agencyReviewResource);
        }

        [SwaggerOperation(
            Summary = "Register a agency review",
            Description = "Add a agency review to the database ",
            Tags = new[] {"AgencyReviews"})]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAgencyReviewResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var agencyReview = _mapper.Map<SaveAgencyReviewResource, AgencyReview>(resource);
            var result = await _agencyReviewService.SaveAsync(agencyReview);
            Console.WriteLine(result.Message);
            if (!result.Success)
                return BadRequest(result.Message);
            var agencyReviewResource = _mapper.Map<AgencyReview, AgencyReviewResource>(result.Resource);

            return Ok(agencyReviewResource);
        }

        [SwaggerOperation(
            Summary = "Delete a agency review",
            Description = "Delete the information of a agency review identified by his id.",
            Tags = new[] {"AgencyReviews"})]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _agencyReviewService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var agencyReviewResource = _mapper.Map<AgencyReview, AgencyReviewResource>(result.Resource);
            
            return Ok(agencyReviewResource);
        }
    }
}