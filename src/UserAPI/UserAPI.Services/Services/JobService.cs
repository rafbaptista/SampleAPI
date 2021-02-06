using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Domain.Interfaces.Services;

namespace UserAPI.Services.Services
{
    public class JobService : ServiceBase<Job>, IJobService
    {
        public JobService(IJobRepository jobRepository)
            :base(jobRepository)
        {

        }
    }
}
