using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Go2Climb.API;
using Go2Climb.API.Agencies.Resources;
using Go2Climb.API.Domain.Services.Communication;
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
    public class AddAgencyReviewStepsDefinition
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }
        private AgencyResource Agency { get; set; }
        private CustomerResource Customer { get; set; }
        private AgencyReviewResource AgencyReview { get; set; }

        public AddAgencyReviewStepsDefinition(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/agencyreviews is available")]
        public void GivenTheEndpointHttpsLocalhostApiVAgencyreviewsIsAvailable(int port, int version)
        {
            BaseUri = new Uri($"https://localhost:{port}/api/v{version}/posts");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
        }

        [Given(@"A Agency already exists")]
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
        
        [Given(@"A Customer already hire a service")]
        public async void GivenACustomerAlreadyHireAService(Table existingCustomerResource)
        {
            var customerUri = new Uri("https://localhost:5001/api/v1/customers");
            var resource = existingCustomerResource.CreateSet<SaveCustomerResourse>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var customerResponse = Client.PostAsync(customerUri, content);
            var interestResponseData = await customerResponse.Result.Content.ReadAsStringAsync();
            var existingInterest = JsonConvert.DeserializeObject<CustomerResource>(interestResponseData);
            Customer = existingInterest;
        }

        [When(@"A AgencyReview Request is Sent")]
        public void WhenAAgencyReviewRequestIsSent(Table saveAgencyReviewResource)
        {
            var resource = saveAgencyReviewResource.CreateSet<SaveAgencyReviewResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = Client.PostAsync(BaseUri, content);
        }

        [Then(@"A Response With status (.*) is received")]
        public void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.Equal(actualStatusCode, actualStatusCode);
        }
    }
}