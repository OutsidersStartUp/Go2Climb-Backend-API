using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class AgencyReviewResponse : BaseResponse<AgencyReview>
    {
        //UNHAPPY
        public AgencyReviewResponse(string message) : base(message)
        {
        }
        //HAPPY
        public AgencyReviewResponse(AgencyReview resource) : base(resource)
        {
        }
    }
}