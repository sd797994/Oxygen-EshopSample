using ApplicationBase;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
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
            UserContext context, ICurrentUserInfo currentUser) : base(context, currentUser)
        {
            _context = context;
        }
    }
}
