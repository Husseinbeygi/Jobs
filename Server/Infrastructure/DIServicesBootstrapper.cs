using Application.AccountApp;
using Application.JobApp;
using Application.UserApp;
using Domain.JobAgg;
using Domain.UserAgg;
using Framework.Password;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure
{
    public class DIServicesBootstrapper
    {
        internal static void ServicesBootstrapper(IServiceCollection services)
        {
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IPasswordHasher,PasswordHasher>();

            services.AddTransient<IJobApplication, JobApplication>();
            services.AddTransient<IJobRepository, JobRepository>();
        }
    }
}
