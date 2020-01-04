using ApplicationBase;
using DomainBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trade.Domain.Aggregation;
using Trade.Domain.Aggregation.ValueObject;
using Trade.Domain.Factory;
using Trade.Domain.Repository;
using Trade.Domain.Specification;
using TradeServiceInterface.Dtos;
using TradeServiceInterface.UseCase;
using UserServiceInterface.Query;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Trade.Application.UseCase
{
    public class AccountRecharge : BaseUseCase<AccountRechargeDto>, IAccountRecharge
    {
        private readonly ITradeRepository tradeRepository;
        private readonly ITradeRecordRepository tradeRecordRepository;
        private readonly IGetUserState getUserState;
        private readonly ITransaction transaction;
        public AccountRecharge(ITradeRepository tradeRepository, ITradeRecordRepository tradeRecordRepository,
            ITransaction transaction, IServiceProxy serviceProxy, IIocContainer iocContainer) : base(iocContainer)
        {
            this.tradeRepository = tradeRepository;
            this.tradeRecordRepository = tradeRecordRepository;
            this.getUserState = serviceProxy.CreateProxy<IGetUserState>();
            this.transaction = transaction;
        }
        public async Task<BaseApiResult<bool>> Excute(AccountRechargeDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //获取交易对象
                var trade = await tradeRepository.GetAsync(x => x.UserId == input.UserId);
                if (trade == null)
                {
                    throw new ApplicationException("没有找到账户交易信息!");
                }
                //执行款项变更
                trade.OperationBalance(input.RechargeBalance);
                //获取用户状态
                var userState = (await getUserState.Query(new BaseAuthDto() { UserId = input.UserId })).Data;
                //添加交易流水
                var record = new TradeRecordFactory().Create(trade.Id, input.RechargeBalance);
                //规约校验
                if (await new AccountRechargeCreateSpecification((UserStateEnum)userState.State).SatisfiedBy(trade))
                {
                    using (var tran = transaction.BeginTransaction())
                    {
                        //仓储添加
                        tradeRepository.Update(trade);
                        tradeRecordRepository.Add(record);
                        //工作单元持久化
                        await tradeRepository.SaveAsync();
                        tran.Commit();
                        return true;
                    }
                }
                return false;
            });
        }
    }
}
