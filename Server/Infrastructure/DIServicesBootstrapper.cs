using Application.AccountApp;
using Application.JobApp;
using Application.CategoryApp;
using Application.UserApp;
using Domain.JobAgg;
using Application.CommentApp;
using Domain.CategoryAgg;
using Domain.CommentAgg;
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
			services.AddTransient<ICommentApplication, CommentApplication>();
			services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IPasswordHasher,PasswordHasher>();

            services.AddTransient<IJobApplication, JobApplication>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IPasswordHasher,PasswordHasher>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryApplication, CategoryApplication>();
        }
    }
}
