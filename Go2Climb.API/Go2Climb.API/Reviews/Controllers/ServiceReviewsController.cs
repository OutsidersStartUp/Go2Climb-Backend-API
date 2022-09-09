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
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ServiceReviewsController : ControllerBase
    {
        private readonly IServiceReviewService _serviceReviewService;
        private readonly IMapper _mapper;

        public ServiceReviewsController(IServiceReviewService serviceReviewService, IMapper mapper)
        {
            _serviceReviewService = serviceReviewService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Get All serviceReviews",
            Description = "Get All serviceReviews already stored",
            Tags = new[] {"ServiceReviews"})]
        [HttpGet]
        public async Task<IEnumerable<ServiceReviewResource>> GetAllAsync()
        {
            var serviceReview = await _serviceReviewService.ListAsync();
            var resources = _mapper.Map<IEnumerable<ServiceReview>, IEnumerable<ServiceReviewResource>>(serviceReview);
            return resources;
        }
        
        [SwaggerOperation(
            Summary = "Get a Service Review by id",
            Description = "Get the service review based on the id if it exists",
            Tags = new[] {"ServiceReviews"})]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _serviceReviewService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var serviceReviewResource = _mapper.Map<ServiceReview, ServiceReviewResource>(result.Resource);
            return Ok(serviceReviewResource);
        }

        [SwaggerOperation(
            Summary = "Register a service review",
            Description = "Add a service review to the database",
            Tags = new[] {"ServiceReviews"})]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveServiceReviewResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var serviceReview = _mapper.Map<SaveServiceReviewResource, ServiceReview>(resource);
            var result = await _serviceReviewService.SaveAsync(serviceReview);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var serviceReviewResource = _mapper.Map<ServiceReview, ServiceReviewResource>(result.Resource);
            return Ok(serviceReviewResource);
        }

        [SwaggerOperation(
            Summary = "Delete a service review",
            Description = "Delete the information of a service review identified by his id.",
            Tags = new[] {"ServiceReviews"})]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _serviceReviewService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var serviceReviewResource = _mapper.Map<ServiceReview, ServiceReviewResource>(result.Resource);
            return Ok(serviceReviewResource);
        }
    }
}