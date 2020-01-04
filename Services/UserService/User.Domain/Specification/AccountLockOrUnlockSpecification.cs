using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Aggregation;

namespace User.Domain.Specification
{
    public class AccountLockOrUnlockSpecification : IOperateSpecification<UserEntity>
    {
        Guid MyUserId { get; set; }
        public AccountLockOrUnlockSpecification(Guid myUserId)
        {
            MyUserId = myUserId;
        }
        public async Task<bool> SatisfiedBy(UserEntity entity)
        {
            if (MyUserId == entity.Id)
            {
                throw new DomainException("登录者不能对自己进行账号锁定/解锁操作!");
            }
            return true;
        }
    }
}
