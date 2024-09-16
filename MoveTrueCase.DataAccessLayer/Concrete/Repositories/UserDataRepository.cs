using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovetruCase.DataAccessLayer.Abstract.Repositories;
using MovetruCase.Entities.Entities;
using MovetruCaseDataAccessLayer.Concrete;
using NLog.Filters;

namespace MovetruCase.DataAccessLayer.Concrete.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private MovetruContext context;
        private readonly ILogger<UserDataRepository> logger;

        public UserDataRepository(MovetruContext context, ILogger<UserDataRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<UserData?> GetUserData(Expression<Func<UserData, bool>> filter)
        {

            try
            {
                var userData = await context.Set<UserData>().Where(filter).FirstOrDefaultAsync();

                return userData;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in GetUserData: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }

        }

        public async Task<UserData> AddUserData(UserData userData)
        {
            try
            {
                var addedEntity = context.Entry(userData);
                addedEntity.State = EntityState.Added;

                await context.SaveChangesAsync();

                return addedEntity.Entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in AddUserData: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }

        }

        public async Task<UserData?> UpdateUserData(UserData userData)
        {
            try
            {
                var userDataToUpdate = await context.Set<UserData>().Where(uD => uD.UserID == userData.UserID).FirstOrDefaultAsync();

                if (userDataToUpdate != null)
                {
                    userDataToUpdate.Age = userData.Age;
                    userDataToUpdate.WeightAsKilogram = userData.WeightAsKilogram;
                    userDataToUpdate.HeightAsCentimeter = userData.HeightAsCentimeter;
                    userDataToUpdate.UpdateDate = userData.UpdateDate;
                    userDataToUpdate.Name = userData.Name;
                    userDataToUpdate.IsUserNewlyRegistered = false;

                    await context.SaveChangesAsync();

                }

                return userDataToUpdate;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in UpdateUserData: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
