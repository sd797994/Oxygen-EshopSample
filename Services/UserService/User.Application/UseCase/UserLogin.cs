using ApplicationBase;
using DomainBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeServiceInterface.Dtos;
using TradeServiceInterface.Query;
using User.Domain.Aggregation;
using User.Domain.Repository;
using User.Domain.Specification;
using UserServiceInterface;
using UserServiceInterface.Dtos;
using UserServiceInterface.UseCase;
using ApplicationException = ApplicationBase.ApplicationException;

namespace User.Application.UseCase
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserLogin: BaseUseCase<UserLoginDto>, IUserLogin
    {
        private readonly IUserRepository userRepository;
        private readonly IGlobalTool globalTool;
        #region rpc
        private readonly IGetMyAccountBalance getMyAccountBalance;
        #endregion
        public UserLogin(IUserRepository userRepository, IGlobalTool globalTool, IServiceProxy serviceProxy, IIocContainer iocContainer) : base(iocContainer)
        {
            this.userRepository = userRepository;
            this.globalTool = globalTool;
            this.getMyAccountBalance = serviceProxy.CreateProxy<IGetMyAccountBalance>();
        }
        public async Task<BaseApiResult<UserLoginResponse>> Excute(UserLoginDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //仓储返回领域对象
                var user = await userRepository.GetAsync(x => x.Account.Equals(input.Account));
                if (user == null)
                {
                    throw new ApplicationException("用户名或密码错误");
                }
                //调用领域对象校验是否可以登录
                if (user.CheckLogin(globalTool.GetMD5SaltCode(input.Password, user.Id.ToString())))
                {
                    //远程调用用户金额服务
                    return new UserLoginResponse()
                    {
                        UserId = user.Id,
                        Account = input.Account,
                        Balance = (await getMyAccountBalance.Query(new MyAccountBalanceDto() { UserId = user.Id })).Data.Balance //获取用户余额
                    };
                }
                return null;
            });
        }
    }
}
