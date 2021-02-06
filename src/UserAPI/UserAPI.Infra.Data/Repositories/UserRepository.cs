using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Infra.Data.Context;

namespace UserAPI.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(UserApiContext context)
            : base(context)
        {            

        }


        
    }
}
