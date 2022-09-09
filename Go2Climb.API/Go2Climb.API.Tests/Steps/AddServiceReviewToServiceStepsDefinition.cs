using Go2Climb.API.Domain.Models;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Go2Climb.API.Tests.Steps
{
    [Binding]
    public class AddServiceReviewToServiceStepsDefinition
    {
        private static RestClient _restClient;
        private static RestRequest _restRequest;
        private static IRestResponse _restResponse;
        private static ServiceReview _serviceReview;
        [Given(@"the user wants to review a service")]
        public static void GivenTheUserWantsToReviewAService()
        { 
            _restClient = new RestClient("https://localhost:5001/");
            _restRequest = new RestRequest("api/v1/servicereviews", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
        }

        [When(@"user add a new review")]
        public static void WhenUserAddANewReview(Table serviceReviewData)
        {
            _serviceReview = serviceReviewData.CreateInstance<ServiceReview>();
            _serviceReview = new ServiceReview()
            {
                Date = "06-11-2021",
                Comment = "Nice service",
                Score = 5,
                ServiceId = 1
            };
            _restRequest.AddJsonBody(_serviceReview);
            _restResponse = _restClient.Execute(_restRequest);
        }

        [Then(@"the review will be added successfully")]
        public static void ThenTheReviewWillBeAddedSuccessfully()
        {
            Assert.That("Nice service", Is.EqualTo(_serviceReview.Comment));
        }
    }
}