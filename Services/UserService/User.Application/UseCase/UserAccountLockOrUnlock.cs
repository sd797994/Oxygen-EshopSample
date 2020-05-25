using ApplicationBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Repository;
using User.Domain.Specification;
using UserServiceInterface.Dtos;
using UserServiceInterface.UseCase;
using ApplicationException = ApplicationBase.ApplicationException;

namespace User.Application.UseCase
{
    public class UserAccountLockOrUnlock : BaseUseCase<AccountLockOrUnLockDto>, IUserAccountLockOrUnlock
    {
        private readonly IUserRepository userRepository;
        public UserAccountLockOrUnlock(IUserRepository userRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.userRepository = userRepository;
        }
        public async Task<BaseApiResult<bool>> Excute(AccountLockOrUnLockDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //获取用户信息
                var user = await userRepository.GetAsync(input.LockUserId);
                if (user == null)
                {
                    throw new ApplicationException("当前所选用户不存在!");
                }
                //进行用户锁定/解锁操作
                user.LockOrUnLock(input.IsLock);
                //规约校验
                if (await new AccountLockOrUnlockSpecification(input.UserId).SatisfiedBy(user))
                {
                    //持久化
                    userRepository.Update(user);
                    await userRepository.SaveAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
