using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Resources;
using Go2Climb.API.Security.Authorization.Handlers.Interfaces;
using Go2Climb.API.Security.Domain.Services.Communication;
using Go2Climb.API.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Go2Climb.API.Services
{
    public class AgencyService : IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public AgencyService(IAgencyRepository agencyRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
        {
            _agencyRepository = agencyRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Agency>> ListAsync()
        {
            return await _agencyRepository.ListAsync();
        }
        
        public async Task<IEnumerable<Agency>> ListByName(string name)
        {
            return await _agencyRepository.ListByName(name);
        }
        
        public async Task<Agency> GetByIdAsync(int id)
        {
            var agency = await _agencyRepository.FindByIdAsync(id);
            if (agency == null) throw new KeyNotFoundException("Agency not found.");
            return agency;
        }
        
        public async Task RegisterAsync(SaveAgencyResource request)
        {
            //Validate
            if (_agencyRepository.ExistsByEmail(request.Email))
                throw new AppException($"Email {request.Email} is already taken.");
            
            //Map request to customer
            var customer = _mapper.Map<Agency>(request);
            
            //Hash Password
            customer.PasswordHash = BCryptNet.HashPassword(request.Password);
            
            //Save customer
            try
            {
                await _agencyRepository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while saving the agency: {e.Message}");
            }
        }

        public async Task UpdateAsync(int id, UpdateAgencyRequest request)
        {
            var agency = GetById(id);
            
            //Validate
            if(_agencyRepository.ExistsByEmail(request.Email)) 
                throw new AppException($"Email {request.Email} is already taken.");
            
            //Hash Password if entered
            if (!string.IsNullOrEmpty(request.Password))
                agency.PasswordHash = BCryptNet.HashPassword(request.Password);
            
            //Map request to Customer
            _mapper.Map(request, agency);
            
            try
            {
                _agencyRepository.Update(agency);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the agency: {e.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var agency = GetById(id);

            try
            {
                _agencyRepository.Remove(agency);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while deleting the agency: {e.Message}");
            }
        }

        public async Task<AgencyResponse> FindById(int id)
        {
            var existingAgency = await _agencyRepository.FindByIdAsync(id);

            if (existingAgency == null)
                return new AgencyResponse("Agency not found.");

            return new AgencyResponse(existingAgency);
        }

        //Helper method
        private Agency GetById(int id)
        {
            var agency = _agencyRepository.FindById(id);
            if (agency == null) throw new KeyNotFoundException("Agency not found.");
            return agency;
        }
    }
}