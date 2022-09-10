using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Go2Climb.API;
using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Resources;
using Go2Climb.API.Services.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace GoClimb.API.XUnit.test.Steps
{
    [Binding]
    public class AddHiredServiceStepsDefinition
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }
        private ServiceResource Service { get; set; }
        private CustomerResource Customer { get; set; }
        private HiredServiceResource HiredService { get; set; }

        public AddHiredServiceStepsDefinition(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/hiredservice is available")]
        public void GivenTheEndpointHttpsLocalhostApiVHiredServiceIsAvailable(int port, int version)
        {
            BaseUri = new Uri($"https://localhost:{port}/api/v{version}");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
        }

        [Given(@"A Service already exists")]
        public async void GivenAServiceAlreadyExists(Table existingServiceResource)
        {
            var serviceUri = new Uri("https://localhost:5001/api/v1/services");
            var resource = existingServiceResource.CreateSet<SaveServiceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var serviceResponse = Client.PostAsync(serviceUri, content);
            var serviceResponseData = await serviceResponse.Result.Content.ReadAsStringAsync();
            var existingService = JsonConvert.DeserializeObject<ServiceResource>(serviceResponseData);
            Service = existingService;
        }
        
        [Given(@"A Customer hired that service")]
        public async void GivenACustomerHiredThatService(Table existingCustomerResource)
        {
            var customerUri = new Uri("https://localhost:5001/api/v1/customers");
            var resource = existingCustomerResource.CreateSet<SaveCustomerResourse>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var customerResponse = Client.PostAsync(customerUri, content);
            var interestResponseData = await customerResponse.Result.Content.ReadAsStringAsync();
            var existingInterest = JsonConvert.DeserializeObject<CustomerResource>(interestResponseData);
            Customer = existingInterest;
        }

        [When(@"A HiredService Request is Sent")]
        public void WhenAHiredServiceRequestIsSent(Table saveHiredServiceResource)
        {
            var resource = saveHiredServiceResource.CreateSet<SaveHiredServiceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = Client.PostAsync(BaseUri, content);
        }
        
        [Then(@"A Response With Status (.*) is received")]
        public void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.Equal(actualStatusCode, actualStatusCode);
        }
    }
}