using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Infra.Data.Context;

namespace UserAPI.Infra.Data.Repositories
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(UserApiContext context)
            : base(context)
        {

        }
    }
}
