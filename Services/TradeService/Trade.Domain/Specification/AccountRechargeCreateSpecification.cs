using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trade.Domain.Aggregation;
using Trade.Domain.Aggregation.ValueObject;

namespace Trade.Domain.Specification
{
    public class AccountRechargeCreateSpecification : IOperateSpecification<TradeEntity>
    {
        UserStateEnum UserState { get; set; }
        public AccountRechargeCreateSpecification(UserStateEnum userState)
        {
            UserState = userState;
        }
        public async Task<bool> SatisfiedBy(TradeEntity entity)
        {
            if (UserState == UserStateEnum.Frozen)
            {
                throw new DomainException("当前用户已被锁定,无法进行任何交易!");
            }
            return true;
        }
    }
}
