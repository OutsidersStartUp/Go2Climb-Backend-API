using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Domain.Services.Communication;

namespace Go2Climb.API.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork, IActivityRepository activityRepository)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _activityRepository = activityRepository;
        }

        public async Task<IEnumerable<Activity>> ListAsync()
        {
            return await _activityRepository.ListAsync();
        }

        public async Task<ActivityResponse> GetById(int id)
        {
            var existingActivity = _activityRepository.FindById(id);
            if (existingActivity.Result == null)
                return new ActivityResponse("The activity does not exist.");
            
            return new ActivityResponse(existingActivity.Result);
        }

        public async Task<IEnumerable<Activity>> ListByServiceIdAsync(int serviceId)
        {
            return await _activityRepository.ListByServiceId(serviceId);
        }

        public async Task<ActivityResponse> SaveAsync(Activity activity)
        {
            try
            {
                await _activityRepository.AddAsync(activity);
                await _unitOfWork.CompleteAsync();
                return new ActivityResponse(activity);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while saving the activity: {e.Message}");
            }
        }

        public async Task<ActivityResponse> UpdateAsync(int id, Activity activity)
        {
            var existingActivity = await _activityRepository.FindById(id);
            if (existingActivity == null)
                return new ActivityResponse("Activity not found");
            existingActivity.Name = activity.Name;
            existingActivity.Description = activity.Description;
            try
            {
                _activityRepository.Update(existingActivity);
                await _unitOfWork.CompleteAsync();
                return new ActivityResponse(existingActivity);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while updating the Activity: {e.Message}");
            }
        }

        public async Task<ActivityResponse> DeleteAsync(int id)
        {
            var existingActivity = await _activityRepository.FindById(id);
            if (existingActivity == null)
                return new ActivityResponse("Activity not found");
            try
            {
                _activityRepository.Remove(existingActivity);
                await _unitOfWork.CompleteAsync();
                return new ActivityResponse(existingActivity);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while deleting the Activity: {e.Message}");
            }
        }
    }
}