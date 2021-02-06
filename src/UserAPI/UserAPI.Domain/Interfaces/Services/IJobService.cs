using System.Collections.Generic;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Security;

namespace UserAPI.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<User>
    {
        List<User> Todos();
    }
}
