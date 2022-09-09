using Go2Climb.API.Domain.Models;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Go2Climb.API.Tests.Steps
{
    [Binding]
    public class AddActivityToServiceStepsDefinition
    {
        private static RestClient _restClient;
        private static RestRequest _restRequest;
        private static IRestResponse _restResponse;
        private static Activity _activity;
        [Given(@"the agency wants to add on activity endpoint")]
        public static void GivenTheAgencyWantsToAddOnActivityEndpoint()
        {
            _restClient = new RestClient("https://localhost:5001/");
            _restRequest = new RestRequest("api/v1/activities", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
        }

        [When(@"owner add a new activity")]
        public static void WhenOwnerAddANewActivity(Table activityData)
        {
            _activity = activityData.CreateInstance<Activity>();
            _activity = new Activity()
            {
                Name = "Activity 1",
                Description = "DescriptionActivity1",
                ServiceId = 1
            };
            _restRequest.AddJsonBody(_activity);
            _restResponse = _restClient.Execute(_restRequest);
        }

        [Then(@"the activity will be added successfully")]
        public static void ThenTheActivityWillBeAddedSuccessfully()
        {
            Assert.That("Activity 1", Is.EqualTo(_activity.Name));
        }
    }
}