using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Entities.Entities;
using MovetruCaseEntities.Entities;

namespace MovetruCase.DataAccessLayer.Abstract.Repositories
{
    public interface IDailyStepLogRepository
    {
        Task<DailyStepLog?> GetDailyStepLog(Expression<Func<DailyStepLog, bool>> filter);
        Task<List<DailyStepLog>> GetDailyStepLogs(Expression<Func<DailyStepLog, bool>> filter);
        Task<DailyStepLog> AddDailyStepLog(DailyStepLog dailyStepLog);
        Task<DailyStepLog?> UpdateDailyStepLog(DailyStepLog dailyStepLog);
    }
}
