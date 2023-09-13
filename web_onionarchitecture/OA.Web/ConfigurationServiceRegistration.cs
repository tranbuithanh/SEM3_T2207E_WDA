using Microsoft.EntityFrameworkCore;
using OA.Repo;
using OA.Service;

namespace OA.Web
{
    public static class ConfigurationServiceRegistration
    {
        public static IServiceCollection ConfigureOAServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            return services;
        }
    }
}
