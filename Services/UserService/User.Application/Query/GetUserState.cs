using ApplicationBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Repository;
using UserServiceInterface.Dtos;
using UserServiceInterface.Query;

namespace User.Application.Query
{
    public class GetUserState : BaseQueryService<BaseAuthDto, UserLoginResponse> ,IGetUserState
    {
        private readonly IUserRepository userRepository;
        public GetUserState(IUserRepository userRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.userRepository = userRepository;
        }
        public override async Task<BaseApiResult<UserLoginResponse>> Query(BaseAuthDto input)
        {
            return await HandleAsync(input, async () =>
            {
                var result = await userRepository.GetAsync(input.UserId);
                return new UserLoginResponse()
                {
                    State = (int)result?.State
                };
            });
        }
    }
}
