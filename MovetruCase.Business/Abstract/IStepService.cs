using MovetruCaseEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Core.Utilities.Results.DataResult;

namespace MovetruCase.Business.Abstract
{
    public interface IStepService
    {
        Task<IDataResult<DailyStepLog?>> GetDailyStepLog(string userID, DateTimeOffset date);
        Task<IDataResult<List<DailyStepLog?>>> GetDailyStepLogsBetweenDates(string userID, DateTimeOffset startDate, DateTimeOffset endDate);
        Task<IDataResult<DailyStepLog?>> AddDailyStepLog(DailyStepLog log);
        Task<IDataResult<DailyStepLog>> UpdateDailyStepLog(DailyStepLog log);
    }
}
