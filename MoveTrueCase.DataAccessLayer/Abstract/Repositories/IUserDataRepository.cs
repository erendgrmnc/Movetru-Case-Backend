using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Entities.Entities;

namespace MovetruCase.DataAccessLayer.Abstract.Repositories
{
    public interface IUserDataRepository
    {
        Task<UserData?> GetUserData(Expression<Func<UserData, bool>> filter);
        Task<UserData> AddUserData(UserData userData);
        Task<UserData?> UpdateUserData(UserData userData);
    }
}
