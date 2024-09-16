using Microsoft.Extensions.Logging;
using MovetruCase.Business.Abstract;
using MovetruCase.Core.Utilities.Results.DataResult;
using MovetruCase.Core.Utilities.Results.ErrorDataResult;
using MovetruCase.Core.Utilities.Results.SucessDataResult;
using MovetruCase.DataAccessLayer.Abstract.Repositories;
using MovetruCaseEntities.Entities;
using System;

namespace MovetruCase.Business.Concrete
{
    public class StepManager : IStepService
    {
        private readonly IDailyStepLogRepository dailyStepLogRepository;
        private readonly ILogger<StepManager> logger;
        public StepManager(IDailyStepLogRepository dailyStepLogRepository, ILogger<StepManager> logger)
        {
            this.dailyStepLogRepository = dailyStepLogRepository;
            this.logger = logger;
        }

        public async Task<IDataResult<DailyStepLog?>> GetDailyStepLog(string userID, DateTimeOffset date)
        {
            try
            {
                var foundLog = await dailyStepLogRepository.GetDailyStepLog((log =>
                    log.UserId == userID && log.CreationDate.Value.Date == date.Date));


                if (foundLog == null)
                {
                    if (date.Date == DateTime.UtcNow.Date)
                    {
                        var result = await AddDailyStepLog(new DailyStepLog()
                        {
                            UserId = userID,
                            CreationDate = DateTime.UtcNow,
                            TotalSteps = 0
                        });

                        if (result.Success)
                        {
                            foundLog = result.Data;
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

                return new SuccessDataResult<DailyStepLog?>(foundLog, true, Constants.OkMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching DailyStepLog for user {UserId} and date {Date}: {Message}, Inner Exception: {InnerException}", userID, date, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<DailyStepLog?>(null, false, ex.Message);
            }

        }

        public async Task<IDataResult<List<DailyStepLog?>>> GetDailyStepLogsBetweenDates(string userID, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            try
            {
                var foundLog = await dailyStepLogRepository.GetDailyStepLogs((log =>
                    log.UserId == userID && log.CreationDate.Value.Date >= startDate.Date && log.CreationDate <= endDate.Date));

                if (foundLog == null)
                {
                    throw new NullReferenceException();
                }

                List<DailyStepLog?> dailyStepLogs = new List<DailyStepLog?>();

                for (DateTime currentDate = startDate.Date; currentDate < endDate.Date; currentDate = currentDate.AddDays(1))
                {
                    DateTimeOffset dateTimeOffset = new DateTimeOffset(currentDate);
                    var logToAdd = new DailyStepLog()
                    {
                        CreationDate = dateTimeOffset,
                        TotalSteps = 0
                    };

                    var log = foundLog.FirstOrDefault(item => item.CreationDate.Value.Date == currentDate.Date);

                    if (log != null)
                    {
                        logToAdd.TotalSteps = log.TotalSteps;
                    }
                    
                    dailyStepLogs.Add(logToAdd);
                }

                return new SuccessDataResult<List<DailyStepLog?>>(dailyStepLogs, true, Constants.OkMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching DailyStepLogs for user {UserId}: {Message}, Inner Exception: {InnerException}", userID, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<List<DailyStepLog?>>(null, false, ex.Message);
            }
        }

        public async Task<IDataResult<DailyStepLog?>> AddDailyStepLog(DailyStepLog log)
        {
            try
            {
                var createdLog = await dailyStepLogRepository.AddDailyStepLog(log);

                if (createdLog == null)
                {
                    throw new NullReferenceException();
                }

                return new SuccessDataResult<DailyStepLog?>(createdLog, true, Constants.OkMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching DailyStepLogs for user {UserId}: {Message}, Inner Exception: {InnerException}", log.UserId, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<DailyStepLog?>(null, true, ex.Message);
            }
        }

        public async Task<IDataResult<DailyStepLog?>> UpdateDailyStepLog(DailyStepLog log)
        {
            try
            {
                var updatedLog = await dailyStepLogRepository.UpdateDailyStepLog(log);

                if (updatedLog == null)
                {
                    throw new NullReferenceException();
                }

                return new SuccessDataResult<DailyStepLog?>(updatedLog, true, Constants.OkMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching DailyStepLogs for user {UserId}: {Message}, Inner Exception: {InnerException}", log.UserId, ex.Message, ex.InnerException);
                Console.WriteLine(ex);
                return new ErrorDataResult<DailyStepLog?>(null, false, ex.Message);
            }
        }

    }
}
