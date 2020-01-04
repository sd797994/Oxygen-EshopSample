using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserServiceInterface.Dtos;

namespace UserServiceInterface.UseCase
{
    /// <summary>
    /// 用户锁定/解锁
    /// </summary>
    [AuthSign]
    [RemoteService("UserService")]
    public interface IUserAccountLockOrUnlock
    {
        Task<BaseApiResult<bool>> Excute(AccountLockOrUnLockDto input);
    }
}
