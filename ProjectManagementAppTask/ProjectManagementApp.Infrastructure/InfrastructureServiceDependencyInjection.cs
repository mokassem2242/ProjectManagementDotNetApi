using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementApp.Application.Contracts.Infrastructure.identity;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Infrastructure.Identity.Models;
using ProjectManagementApp.Infrastructure.Identity.Services;
using ProjectManagementApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementApp.Infrastructure.Persistence;

namespace ProjectManagementApp.Infrastructure
{
    public static class InfrastructureServiceDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           services.AddScoped<ICurrentUserService, CurrentUserService>();

            //  SQL Server
            services.AddDbContext<ProjectManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Identity with Guid key type
           services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                // Optional: Customize password requirements
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ProjectManagementDbContext>()               
            .AddDefaultTokenProviders();





            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IDomainUserRepository, DomainUserRepository>();

            return services;
        }
           
    }
}
