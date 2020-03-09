using ApplicationBase;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using Oxygen.CommonTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Aggregation;
using User.Domain.Repository;
using User.Infrastructure.EfDataAccess;

namespace User.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<UserEntity, PersistenceObject.User, UserContext>, IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(
            UserContext context, IIocContainer container) : base(context, container)
        {
            _context = context;
        }
    }
}
