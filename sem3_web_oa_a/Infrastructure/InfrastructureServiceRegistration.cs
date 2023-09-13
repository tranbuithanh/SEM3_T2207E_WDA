using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    // Install
    // Install-Package Microsoft.EntityFrameworkCore
    // Install-Package Microsoft.EntityFrameworkCore.SqlServer
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Server=localhost,1433;Database=SEM3_WDA_OA;User Id=sa;Password=Abcd@1234;TrustServerCertificate=true"));
            services?.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
