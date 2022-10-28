using Application.AccountApp;
using Application.UserApp;
using Application.CommentApp;
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
        }
    }
}
