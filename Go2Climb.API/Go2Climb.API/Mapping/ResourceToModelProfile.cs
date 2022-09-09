using Go2Climb.API.Domain.Services.Communication;
using AutoMapper;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Resources;
using Go2Climb.API.Security.Domain.Services.Communication;
using Org.BouncyCastle.Asn1.X509;

namespace Go2Climb.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {

            CreateMap<SaveActivityResource, Activity>();
            CreateMap<SaveAgencyResource, Agency>();
            CreateMap<UpdateAgencyRequest, Agency>()
                .ForAllMembers(options => options.Condition(
                    (source, Target, property) =>
                    {
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                        return true;
                    }));
            CreateMap<SaveServiceResource, Service>();

            CreateMap<RegisterCustomerRequest, Customer>();
            CreateMap<UpdateCustomerRequest, Customer>()
                .ForAllMembers(options => options.Condition(
                    (source, Target, property) =>
                    {
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                        return true;
                    }));
            CreateMap<SaveCustomerResourse, Customer>();
            CreateMap<SaveHiredServiceResource, HiredService>();
            CreateMap<SaveSubscriptionResource, Subscription>();
            
            CreateMap<SaveAgencyReviewResource, AgencyReview>();
            CreateMap<SaveServiceReviewResource, ServiceReview>();
            
        }
    }
}