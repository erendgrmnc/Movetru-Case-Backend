using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Business.DTO;
using MovetruCase.Core.Utilities.Results.DataResult;
using MovetruCase.Entities.Entities;

namespace MovetruCase.Business.Abstract
{
    public interface IUserDataService
    {
        Task<IDataResult<UserDataDTO?>> GetUserData(string userID);
        Task<IDataResult<UserData?>> AddUserData(UserData userData);
        Task<IDataResult<UserData?>> UpdateUserData(UserData userData);
    }
}
