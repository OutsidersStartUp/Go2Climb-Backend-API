using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class ServiceResponse : BaseResponse<Service>
    {
        //UNHAPPY
        public ServiceResponse(string message) : base(message)
        {
        }
        //HAPPY
        public ServiceResponse(Service resource) : base(resource)
        {
        }
    }
}