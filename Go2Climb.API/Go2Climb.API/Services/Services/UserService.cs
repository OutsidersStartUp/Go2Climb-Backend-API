using System;
using System.Threading.Tasks;
using AutoMapper;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Security.Authorization.Handlers.Interfaces;
using Go2Climb.API.Security.Domain.Services.Communication;
using Go2Climb.API.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Go2Climb.API.Services
{
    public class UserService : IUserService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(ICustomerRepository customerRepository, IAgencyRepository agencyRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _agencyRepository = agencyRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var customer = await _customerRepository.FindByEmailAsync(request.Email);
            var agency = await _agencyRepository.FindByEmailAsync(request.Email);

            //Validate
            if (customer == null || !BCryptNet.Verify(request.Password, customer.PasswordHash))
            {
                if (agency == null || !BCryptNet.Verify(request.Password, agency.PasswordHash))
                    throw new AppException("Email or password is incorrect.");
                
                //Authentication successful
                var responseAgency = _mapper.Map<AuthenticateResponse>(agency);
                responseAgency.Token = _jwtHandler.GenerateToken(agency);
                return responseAgency;
            }

            //Authentication successful
            var responseCustomer = _mapper.Map<AuthenticateResponse>(customer);
            responseCustomer.Token = _jwtHandler.GenerateToken(customer);
            return responseCustomer;
        }
    }
}