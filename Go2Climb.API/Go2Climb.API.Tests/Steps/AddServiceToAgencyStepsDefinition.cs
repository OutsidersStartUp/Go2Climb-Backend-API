using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Service = Go2Climb.API.Domain.Models.Service;

namespace Go2Climb.API.Tests.Steps
{
    [Binding]
    public class AddServiceToAgencyStepsDefinition
    {
        private static RestClient _restClient;
        private static RestRequest _restRequest;
        private static IRestResponse _restResponse;
        private static Service _service;
        
        [Given(@"the agency wants to add on service endpoint")]
        public static void GivenTheAgencyWantsToAddOnServiceEndpoint()
        {
            _restClient = new RestClient("https://localhost:5001/");
            _restRequest = new RestRequest("api/v1/services", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
        }

        [When(@"owner add a new service")]
        public static void WhenOwnerAddANewService(Table serviceData)
        {
            _service = serviceData.CreateInstance<Service>();
            _service = new Service()
            {
                Name = "New Service",
                Price = 420,
                Location = "LocationService",
                CreationDate = "06-11-2021",
                Description = "This is a new service for my agency",
                AgencyId = 1
            };
            _restRequest.AddJsonBody(_service);
            _restResponse = _restClient.Execute(_restRequest);
        }

        [Then(@"the service will be added successfully")]
        public static void ThenTheServiceWillBeAddedSuccessfully()
        {
            Assert.That("New Service", Is.EqualTo(_service.Name));
        }
    }
}