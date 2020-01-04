using ApplicationBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Event;
using User.Domain.Factory;
using User.Domain.Repository;
using User.Domain.Specification;
using UserServiceInterface;
using UserServiceInterface.Dtos;
using UserServiceInterface.UseCase;
using ApplicationException = ApplicationBase.ApplicationException;

namespace User.Application.UseCase
{
    /// <summary>
    /// 用户创建用例
    /// </summary>
    public class UserAccountCreate : BaseUseCase<AccountCreateDto>, IUserAccountCreate
    {
        private readonly IUserRepository userRepository;
        private readonly IGlobalTool globalTool;
        private readonly IEventBus eventBus;
        private readonly ITransaction transaction;
        public UserAccountCreate(IUserRepository userRepository, ITransaction transaction, IGlobalTool globalTool, IEventBus eventBus, IIocContainer iocContainer) : base(iocContainer)
        {
            this.userRepository = userRepository;
            this.globalTool = globalTool;
            this.eventBus = eventBus;
            this.transaction = transaction;
        }
        public async Task<BaseApiResult<bool>> Excute(AccountCreateDto input)
        {
            return await HandleAsync(input, async () =>
            {
                using (var tran = transaction.BeginTransaction())
                {
                    //工厂构建用户领域对象
                    var user = new UserFactory().CreateUser(input.Account);
                    //调用领域对象创建密码
                    user.CreatePassword(globalTool.GetMD5SaltCode(input.Password, user.Id.ToString()));
                    //调用规约校验
                    if (await new CreateUserSpecification(userRepository).SatisfiedBy(user))
                    {
                        //仓储添加
                        userRepository.Add(user);
                        //发送用户创建成功事件
                        await eventBus.PublishAsync("EshopSample.UserCreateEvent", new UserCreateEvent(user.Id));
                        //工作单元持久化
                        await userRepository.SaveAsync();
                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            });
        }
    }
}
