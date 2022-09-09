using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class HideServiceResponse : BaseResponse<HiredService>
    {
        public HideServiceResponse(string message) : base(message) {}

        public HideServiceResponse(HiredService resource) : base(resource) {}
    }
}