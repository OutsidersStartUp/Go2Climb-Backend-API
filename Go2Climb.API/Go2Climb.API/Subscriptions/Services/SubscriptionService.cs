using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Domain.Services.Communication;

namespace Go2Climb.API.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _subscriptionRepository.ListAsync();
        }

        public async Task<SubscriptionResponse> GetById(int id)
        {
            var existingSubscription = _subscriptionRepository.FindById(id);
            if (existingSubscription.Result == null)
                return new SubscriptionResponse("The subscription does not exist");
            
            return new SubscriptionResponse(existingSubscription.Result);
        }

        public async Task<SubscriptionResponse> SaveAsync(Subscription subscription)
        {
            try
            {
                await _subscriptionRepository.AddAsync(subscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(subscription);
            }
            catch (Exception e)
            {
                return new SubscriptionResponse($"An error occurred while saving the Subscription: {e.Message}");
            }
        }

        public async Task<SubscriptionResponse> UpdateAsync(int id, Subscription subscription)
        {
            var existingSubscription = await _subscriptionRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionResponse("Subscription not found");
            existingSubscription.Name = subscription.Name;
            existingSubscription.Price = subscription.Price;
            existingSubscription.Description = subscription.Description;
            try
            {
                _subscriptionRepository.Update(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(existingSubscription);
            }
            catch (Exception e)
            {
                return new SubscriptionResponse($"An error occurred while updating the Subscription: {e.Message}");
            }
        }

        public async Task<SubscriptionResponse> DeleteAsync(int id)
        {
            var existingSubscription = await _subscriptionRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionResponse("Subscription not found");
            try
            {
                _subscriptionRepository.Remove(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(existingSubscription);
            }
            catch (Exception e)
            {
                return new SubscriptionResponse($"An error occurred while deleting the Subscription: {e.Message}");
            }
        }
    }
}