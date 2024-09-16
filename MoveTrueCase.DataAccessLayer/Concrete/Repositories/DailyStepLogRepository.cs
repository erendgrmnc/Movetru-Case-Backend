using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovetruCase.DataAccessLayer.Abstract.Repositories;
using MovetruCaseDataAccessLayer.Concrete;
using MovetruCaseEntities.Entities;

namespace MovetruCase.DataAccessLayer.Concrete.Repositories
{
    public class DailyStepLogRepository : IDailyStepLogRepository
    {
        private MovetruContext context;
        private readonly ILogger<DailyStepLogRepository> logger;
        public DailyStepLogRepository(MovetruContext context, ILogger<DailyStepLogRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<DailyStepLog?> GetDailyStepLog(Expression<Func<DailyStepLog, bool>> filter)
        {
            try
            {
                var result = await context.Set<DailyStepLog>().Where(filter).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in GetDailyStepLog: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<DailyStepLog>> GetDailyStepLogs(Expression<Func<DailyStepLog, bool>> filter)
        {
            try
            {
                return await context.Set<DailyStepLog>().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in GetDailyStepLogs: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<DailyStepLog> AddDailyStepLog(DailyStepLog dailyStepLog)
        {

            try
            {
                var foundDsLog = await GetDailyStepLog(dsLog => (dsLog.UserId == dailyStepLog.UserId) &&
                                                                (dsLog.CreationDate.Value.Date ==
                                                                 dailyStepLog.CreationDate.Value.Date));

                if (foundDsLog == null)
                {
                    var addedEntry = context.Entry(dailyStepLog);
                    addedEntry.State = EntityState.Added;
                    await context.SaveChangesAsync();
                    return addedEntry.Entity;
                }

                return foundDsLog;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding DailyStepLog: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }

        }

        public async Task<DailyStepLog?> UpdateDailyStepLog(DailyStepLog dailyStepLog)
        {
            try
            {
                var foundDsLog = await GetDailyStepLog(dsLog => (dsLog.UserId == dailyStepLog.UserId) &&
                                                                (dsLog.CreationDate.Value.Date ==
                                                                 dailyStepLog.CreationDate.Value.Date));

                if (foundDsLog != null)
                {
                    foundDsLog.TotalSteps = dailyStepLog.TotalSteps;
                    foundDsLog.UpdateDate = DateTimeOffset.UtcNow;
                    await context.SaveChangesAsync();
                }

                return foundDsLog;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating DailyStepLog: {Message}", ex.Message);
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
