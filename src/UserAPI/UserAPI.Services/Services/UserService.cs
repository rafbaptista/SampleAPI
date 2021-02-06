using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Domain.Interfaces.Services;

namespace UserAPI.Services.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
            :base(userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> Todos()
        {
          return userRepository.QueryAll(includeProperty: u => u.Job).ToList();
        }

    }
}
