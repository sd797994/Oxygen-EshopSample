using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Aggregation;
using User.Domain.Aggregation.ValueObject;
using User.Domain.Repository;

namespace User.Domain.Factory
{
    public class UserFactory
    {
        public UserEntity CreateUser(string account)
        {
            return new UserEntity() { Id = Guid.NewGuid(), Account = account, State = UserStateEnum.Normal };
        }
    }
}
