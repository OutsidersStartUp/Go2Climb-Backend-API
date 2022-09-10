using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Go2Climb.API;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Agencies.Resources;
using Go2Climb.API.Resources;
using Go2Climb.API.Security.Domain.Services.Communication;
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
    public sealed class AddScoreService_step
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }
        private ServiceResource Service { get; set; }

        public AddScoreService_step(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Given(@"This Endpoint https://localhost:(.*)/api/v(.*)/services/(.*) is available")]
        public void GivenThisEndpointHttpsLocalhostApiVServicesIsAvailable(int port, int version, int service)
        {
            BaseUri = new Uri($"https://localhost:{port}/api/v{version}/services/{service}");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
        }

        [Given(@"A Agency is already stored")]
        public void GivenAAgencyIsAlreadyStored(Table existingAgencyResource)
        {
            var agencyUri = new Uri("https://localhost:5001/api/v1/agencies/auth/sign-up");
            var resource = existingAgencyResource.CreateSet<SaveAgencyResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Client.PostAsync(agencyUri, content);

        }

        [Given(@"A Customer is already store")]
        public void GivenACustomerIsAlreadyStore(Table existingCustomerResource)
        {
            var agencyUri = new Uri("https://localhost:5001/api/v1/customer/auth/sign-up");
            var resource = existingCustomerResource.CreateSet<RegisterCustomerRequest>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Client.PostAsync(agencyUri, content);
        }

        [Given(@"A service is already stored")]
        public async void GivenAServiceIsAlreadyStored(Table existingServiceResource)
        {
            var serviceUri = new Uri("https://localhost:5001/api/v1/services");
            var resource = existingServiceResource.CreateSet<SaveServiceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var serviceResponse = Client.PostAsync(serviceUri, content);
            var interestResponseData = await serviceResponse.Result.Content.ReadAsStringAsync();
            var existingInterest = JsonConvert.DeserializeObject<ServiceResource>(interestResponseData);
            Service = existingInterest;
        }


        [When(@"A Service review is Sent with complete information for add review")]
        public void WhenAServiceReviewIsSentWithCompleteInformationForAddReview(Table saveReviewResource)
        {
            var resource = saveReviewResource.CreateSet<SaveServiceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = Client.PostAsync(BaseUri, content);
        }

        [Then(@"A Response with status (.*) is received")]
        public void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.Equal(actualStatusCode, actualStatusCode);
        }
    }
}