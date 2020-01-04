using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Aggregation;
using User.Domain.Repository;

namespace User.Domain.Specification
{
    public class CreateUserSpecification : IOperateSpecification<UserEntity>
    {
        private readonly IUserRepository userRepository;
        public CreateUserSpecification(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<bool> SatisfiedBy(UserEntity entity)
        {
            if (await userRepository.CountAsync(x => x.Account == entity.Account) > 0)
            {
                throw new DomainException("创建账号不能重复,请重新添加!");
            }
            return true;
        }
    }
}
