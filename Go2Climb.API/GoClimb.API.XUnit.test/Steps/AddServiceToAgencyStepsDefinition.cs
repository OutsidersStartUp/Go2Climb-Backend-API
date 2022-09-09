using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Go2Climb.API;
using Go2Climb.API.Agencies.Resources;
using Go2Climb.API.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace GoClimb.API.XUnit.test.Steps
{
    [Binding]
    public class AddServiceToAgencyStepsDefinition
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }
        private AgencyResource Agency { get; set; }

        public AddServiceToAgencyStepsDefinition(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/services is available")]
        public void GivenTheEndpointHttpsLocalhostApiVServicesIsAvailable(int port, int version)
        {
            BaseUri = new Uri($"https://localhost:{port}/api/v{version}/services");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
        }
        
        [Given(@"A Agency Is Already Stored")]
        public async void GivenAAgencyIsAlreadyStored(Table existingAgencyResource)
        {
            var agencyUri = new Uri("https://localhost:5001/api/v1/agencies");
            var resource = existingAgencyResource.CreateSet<SaveAgencyResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var agencyResponse = Client.PostAsync(agencyUri, content);
            var interestResponseData = await agencyResponse.Result.Content.ReadAsStringAsync();
            var existingInterest = JsonConvert.DeserializeObject<AgencyResource>(interestResponseData);
            Agency = existingInterest;
        }

        [When(@"A Service Request is Sent")]
        public void WhenAServiceRequestIsSent(Table saveServiceResource)
        {
            var resource = saveServiceResource.CreateSet<SaveServiceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = Client.PostAsync(BaseUri, content);
        }
        
        [Then(@"A Response with status (.*) is Received")]
        public void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.Equal(actualStatusCode, actualStatusCode);
        }
    }
}