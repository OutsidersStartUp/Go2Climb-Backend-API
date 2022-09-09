using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Domain.Services.Communication;

namespace Go2Climb.API.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork, IAgencyRepository agencyRepository)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _agencyRepository = agencyRepository;
        }

        public async Task<IEnumerable<Service>> ListAsync()
        {
            return await _serviceRepository.ListAsync();
        }

        public async Task<ServiceResponse> GetById(int id)
        {
            var existingService = _serviceRepository.FindById(id);
            if (existingService.Result == null)
                return new ServiceResponse("The service does not exist.");
            
            return new ServiceResponse(existingService.Result);
        }

        public async Task<IEnumerable<Service>> ListByText(string name, int start, int limit)
        {
            return await _serviceRepository.ListByText(name, start, limit);
        }

        public async Task<IEnumerable<Service>> ListByTextAndFilterMoney(string name, int minMoney, int maxMoney, int start, int limit)
        {
            return await _serviceRepository.ListByTextFilterMoney(name, minMoney, maxMoney, start, limit);
        }

        public async Task<IEnumerable<Service>> ListByTextAndFilterScore(string name, int score, int start, int limit)
        {
            return await _serviceRepository.ListByTextFilterScore(name, score, start, limit);
        }

        public async Task<IEnumerable<Service>> ListByTextAndAllFilter(string name, int score, int min, int max, int start, int limit)
        {
            return await _serviceRepository.ListByTextAndAllFilter(name, score, min, max, start, limit);
        }

        public async Task<IEnumerable<Service>> ListByAgencyIdAsync(int agencyId)
        {
            return await _serviceRepository.ListByAgencyId(agencyId);
        }
        
        public async Task<IEnumerable<Service>> FilterByCategory(string name, int start, int limit)
        {
            switch (name)
            {
                case "offers":
                    return await _serviceRepository.FilterByCategoryOffers(start, limit);
                case "populars":
                    return await _serviceRepository.FilterByCategoryPopulars(start, limit);
                case "forYou":
                    return await _serviceRepository.FilterByCategoryForYou(start, limit);
                default:
                    return await _serviceRepository.ListAsync();
            }
        }

        public async Task<ServiceResponse> SaveAsync(Service service)
        {
            /*var existingAgency = await _agencyRepository.FindById(agencyId);
            if (existingAgency == null)
            {
                return new ServiceResponse("Service not found");
            }
            service.Agency = existingAgency;*/
            try
            {
                await _serviceRepository.AddAsync(service);
                await _unitOfWork.CompleteAsync();
                return new ServiceResponse(service);
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred while saving the Service: {e.Message}");
            }
        }

        public async Task<ServiceResponse> UpdateAsync(int id, Service service)
        {
            var existingService = await _serviceRepository.FindById(id);
            if (existingService == null)
                return new ServiceResponse("Service not found");
            existingService.Name = service.Name;
            existingService.Price = service.Price;
            existingService.Location = service.Location;
            existingService.Description = service.Description;
            existingService.CreationDate = service.CreationDate;
            try
            {
                _serviceRepository.Update(existingService);
                await _unitOfWork.CompleteAsync();
                return new ServiceResponse(existingService);
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred while updating the Service: {e.Message}");
            }
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var existingService = await _serviceRepository.FindById(id);
            if (existingService == null)
                return new ServiceResponse("Service not found");
            try
            {
                _serviceRepository.Remove(existingService);
                await _unitOfWork.CompleteAsync();
                return new ServiceResponse(existingService);
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred while deleting the Service: {e.Message}");
            }
        }
    }
}