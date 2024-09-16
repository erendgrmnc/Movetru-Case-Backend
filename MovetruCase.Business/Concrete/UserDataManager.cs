using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Logging;
using Microsoft.Extensions.Logging;
using MovetruCase.Business.Abstract;
using MovetruCase.Business.DTO;
using MovetruCase.Core.Utilities.Results.DataResult;
using MovetruCase.Core.Utilities.Results.ErrorDataResult;
using MovetruCase.Core.Utilities.Results.SucessDataResult;
using MovetruCase.DataAccessLayer.Abstract.Repositories;
using MovetruCase.Entities.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovetruCase.Business.Concrete
{
    public class UserDataManager : IUserDataService
    {
        private readonly IUserDataRepository userDataRepository;
        private readonly ILogger<UserDataManager> logger;

        public UserDataManager(IUserDataRepository userDataRepository, ILogger<UserDataManager> logger)
        {
            this.userDataRepository = userDataRepository;
            this.logger = logger;
        }

        public async Task<IDataResult<UserDataDTO?>> GetUserData(string userID)
        {
            try
            {
                var userData = await userDataRepository.GetUserData((data => data.UserID == userID));

                if (userData == null)
                {
                    if (!string.IsNullOrEmpty(userID))
                    {
                        var newDataResult = await AddUserData(new UserData()
                        {
                            UserID = userID,
                            CreationDate = DateTimeOffset.Now.UtcDateTime,
                            IsUserNewlyRegistered = true
                        });

                        if (newDataResult.Success)
                        {
                            userData = newDataResult.Data;
                        }
                        else
                        {
                            throw new NullReferenceException();
                        }
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }

                UserDataDTO userDataDTO = new UserDataDTO()
                {
                    WeightAsKilogram = userData.WeightAsKilogram,
                    Age = userData.Age,
                    DailyStepGoal = CalculateDailyStepGoal(userData.Age ?? 0, userData.HeightAsCentimeter ?? 0, userData.WeightAsKilogram ?? 0),
                    HeightAsCentimeter = userData.HeightAsCentimeter,
                    IsUserNewlyRegistered = userData.IsUserNewlyRegistered,
                    Name = userData.Name
                };

                return new SuccessDataResult<UserDataDTO>(userDataDTO, true, Constants.OkMessage);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching GetUserData for user {UserId} : {Message}, Inner Exception: {InnerException} ", userID, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<UserDataDTO?>(null, true, ex.Message);
            }
        }

        public async Task<IDataResult<UserData?>> AddUserData(UserData userData)
        {
            try
            {
                var addedUserData = await userDataRepository.AddUserData(userData);
                if (addedUserData == null)
                    throw new NullReferenceException();
                return new SuccessDataResult<UserData?>(addedUserData, true, Constants.OkMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding AddUserData for user {UserId} : {Message}, Inner Exception: {InnerException} ", userData.UserID, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<UserData?>(userData, false, ex.Message);
            }
        }

        public async Task<IDataResult<UserData?>> UpdateUserData(UserData userData)
        {
            try
            {
                var updatedUserData = await userDataRepository.UpdateUserData(userData);
                if (updatedUserData == null)
                    throw new NullReferenceException();
                return new SuccessDataResult<UserData?>(updatedUserData, true, Constants.OkMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating UpdateUserData for user {UserId} : {Message}, Inner Exception: {InnerException} ", userData.UserID, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<UserData?>(userData, false, ex.Message);
            }
        }


        private int CalculateDailyStepGoal(int age, int heightAsCentimeter, int weightAsKilograms)
        {
            int baseStepGoal = (20000 - (age * 100)) + (heightAsCentimeter * 10);

            if (weightAsKilograms > 70)
            {
                baseStepGoal += (weightAsKilograms - 70) * 50;
            }
            else if (weightAsKilograms < 50)
            {
                baseStepGoal -= (50 - weightAsKilograms) * 25;
            }

            return baseStepGoal;
        }
    }
}
