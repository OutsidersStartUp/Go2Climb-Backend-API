using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class ServiceReviewResponse : BaseResponse<ServiceReview>
    {
        //UNHAPPY
        public ServiceReviewResponse(string message) : base(message)
        {
        }
        //HAPPY
        public ServiceReviewResponse(ServiceReview resource) : base(resource)
        {
        }
    }
}