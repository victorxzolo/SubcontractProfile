using AutoMapper;
using SubcontractProfile.WebApi.Services;
using SubcontractProfile.WebApi.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using SubcontractProfile.WebApi.Services.Services;

namespace SubcontractProfile.WebApi.IoC.Configuration.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services != null)
            {
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<ISubcontractProfileAddressRepo, SubcontractProfileAddressRepo>();
                services.AddTransient<ISubcontractProfileAsstEngineerRepo, SubcontractProfileAsstEngineerRepo>();
                services.AddTransient<ISubcontractProfileBankingRepo, SubcontractProfileBankingRepo>();
                services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                services.AddTransient<ISubcontractProfileDistrictRepo, SubcontractProfileDistrictRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                //services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
            }

        }

        public static void ConfigureMappings(this IServiceCollection services)
        {
            if (services != null)
            {
                //Automap settings
                services.AddAutoMapper(Assembly.GetExecutingAssembly());
            }
        }
    }
}
