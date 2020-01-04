using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Aggregation;

namespace User.Domain.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
    }
}
