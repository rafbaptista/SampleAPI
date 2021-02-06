using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserAPI.Domain.Interfaces.Repositories;
using UserAPI.Domain.Interfaces.Services;
using UserAPI.Domain.Interfaces.Transactions;
using UserAPI.Infra.Data.Context;
using UserAPI.Infra.Data.Repositories;
using UserAPI.Infra.Data.Transaction;
using UserAPI.Services.Services;

namespace UserAPI.Infra.CrossCutting.IoC
{
    public static class ServicesInjector
    {
        public static void AddInMemoryDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<UserApiContext>(options =>
                options.UseInMemoryDatabase(connectionString));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<ITokenService, TokenService>();

            //Repositories
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
        }
    }
}
