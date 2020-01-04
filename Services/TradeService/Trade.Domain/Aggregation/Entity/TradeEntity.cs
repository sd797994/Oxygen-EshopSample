using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trade.Domain.Aggregation
{
    public class TradeEntity : AggregateRoot
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 操作余额
        /// </summary>
        /// <param name="balance"></param>
        public void OperationBalance(decimal balance)
        {
            if (balance == 0)
            {
                throw new DomainException("金额不能为0,请重试");
            }
            else if (balance > 0)
            {
                this.Balance += balance;
            }
            else if (balance < 0)
            {
                if (this.Balance + balance < 0)
                {
                    throw new DomainException($"扣款金额[{balance}]大于余额[{this.Balance}],无法执行扣款操作,请重试");
                }
                this.Balance += balance;
            }
        }
    }
}
