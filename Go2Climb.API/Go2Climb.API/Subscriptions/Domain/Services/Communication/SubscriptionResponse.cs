using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class SubscriptionResponse : BaseResponse<Subscription>
    {
        //UNHAPPY
        public SubscriptionResponse(string message) : base(message)
        {
            
        }
        //HAPPY
        public SubscriptionResponse(Subscription resource) : base(resource)
        {
            
        }
    }
}