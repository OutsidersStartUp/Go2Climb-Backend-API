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
    public class SubscriptionsController: ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public SubscriptionsController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Subscriptions",
            Description = "Get All Subscriptions From The Database.",
            Tags= new[] {"Subscriptions"})]
        public async Task<IEnumerable<SubscriptionResource>> GetAllAsync()
        {
            var subscriptions = await _subscriptionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionResource>>(subscriptions);
            return resources;
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Subscription By Id",
            Description = "Get A Subscription already stored by its Id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _subscriptionService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register a Subscription",
            Description = "Add a Subscription to the Database")]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriptionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscription = _mapper.Map<SaveSubscriptionResource, Subscription>(resource);
            var result = await _subscriptionService.SaveAsync(subscription);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var subscriptionResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);

            return Ok(subscriptionResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update a Subscription",
            Description = "Update a Subscription From the Database by its Id")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSubscriptionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var subscription = _mapper.Map<SaveSubscriptionResource, Subscription>(resource);

            var result = await _subscriptionService.UpdateAsync(id, subscription);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);

            return Ok(subscriptionResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A Subscription",
            Description = "Remove A Subscription already stored by its Id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _subscriptionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var subscriptionResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            
            return Ok(subscriptionResource);
        }
    }
}