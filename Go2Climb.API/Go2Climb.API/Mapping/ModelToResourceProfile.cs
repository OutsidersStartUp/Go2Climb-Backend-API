using AutoMapper;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Agencies.Resources;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Resources;
using Go2Climb.API.Security.Domain.Services.Communication;
using Go2Climb.API.Services.Resources;

namespace Go2Climb.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {

            CreateMap<Activity, ActivityResource>();
            CreateMap<Agency, AgencyResource>();
            CreateMap<Agency, AuthenticateResponse>();
            CreateMap<Service, ServiceResource>();

            CreateMap<Customer, CustomerResource>();
            CreateMap<Customer, AuthenticateResponse>();
            CreateMap<HiredService, HiredServiceResource>();
            CreateMap<AgencyReview, AgencyReviewResource>();
            CreateMap<ServiceReview, ServiceReviewResource>();
            CreateMap<Subscription, SubscriptionResource>();
            
            CreateMap<AuthenticateResponse, AuthenticateAgencyResponse>();
            CreateMap<AuthenticateResponse, AuthenticateCustomerResponse>();
            
        }
    }
}