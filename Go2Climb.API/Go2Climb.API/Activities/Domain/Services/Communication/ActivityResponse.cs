using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class ActivityResponse : BaseResponse<Activity>
    {
        //UNHAPPY
        public ActivityResponse(string message) : base(message)
        {
            
        }
        //HAPPY
        public ActivityResponse(Activity resource) : base(resource)
        {
            
        }
    }
}