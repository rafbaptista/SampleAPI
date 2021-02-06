using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserAPI.Domain.Entities;

namespace UserAPI.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
