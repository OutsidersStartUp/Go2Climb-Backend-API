using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Domain.Services.Communication;

namespace Go2Climb.API.Services
{
    public class ServiceReviewService : IServiceReviewService
    {
        private readonly IServiceReviewRepository _serviceReviewRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceReviewService(IServiceReviewRepository serviceReviewRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IServiceRepository serviceRepository)
        {
            _serviceReviewRepository = serviceReviewRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<ServiceReview>> ListAsync()
        {
            return await _serviceReviewRepository.ListAsync();
        }

        public async Task<IEnumerable<ServiceReview>> ListByServiceIdAsync(int serviceId)
        {
            return await _serviceReviewRepository.ListByServiceId(serviceId);
        }

        public async Task<IEnumerable<ServiceReview>> ListByCustomerIdAsync(int customerId)
        {
            return await _serviceReviewRepository.ListByCustomerId(customerId);
        }

        public async Task<ServiceReviewResponse> GetByIdAsync(int id)
        {
            var existingResourceReview = _serviceReviewRepository.FindByIdAsync(id);
            if (existingResourceReview.Result == null)
                return new ServiceReviewResponse("The service review does not exist.");
            
            return new ServiceReviewResponse(existingResourceReview.Result);
        }
        
        public async Task<ServiceReviewResponse> SaveAsync(ServiceReview serviceReview)
        {
            var existingCustomer = _customerRepository.FindByIdAsync(serviceReview.CustomerId);
            if (existingCustomer == null)
                return new ServiceReviewResponse("Customer does not exist.");
            var exitingService = _serviceRepository.FindById(serviceReview.ServiceId);
            if (exitingService == null)
                return new ServiceReviewResponse("Service does not exist.");
            try
            {
                await _serviceReviewRepository.AddAsync(serviceReview);
                await _unitOfWork.CompleteAsync();

                return new ServiceReviewResponse(serviceReview);
            }
            catch (Exception e)
            {
                return new ServiceReviewResponse($"An error occurred while saving the service review: {e.Message}");
            }
        }

        public async Task<ServiceReviewResponse> DeleteAsync(int id)
        {
            //Validate ServiceReview
            var existingServiceReview = await _serviceReviewRepository.FindByIdAsync(id);

            if (existingServiceReview == null)
                return new ServiceReviewResponse("Service review not found.");

            try
            {
                _serviceReviewRepository.Remove(existingServiceReview);
                await _unitOfWork.CompleteAsync();

                return new ServiceReviewResponse(existingServiceReview);
            }
            catch (Exception e)
            {
                return new ServiceReviewResponse($"An error occurred while deleting the service review: {e.Message}");
            }
        }
    }
}