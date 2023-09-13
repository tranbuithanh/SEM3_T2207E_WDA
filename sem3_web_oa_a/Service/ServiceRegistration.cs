using Domain;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Service.DTOs;
using Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services?.AddAutoMapper(Assembly.GetExecutingAssembly());
            services?.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
            services?.AddScoped(typeof(IUserProfileRepostitory), typeof(UserProfileRepostitory));
            return services;
        }
    }
}
