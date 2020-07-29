using AutoMapper;
using SubcontractProfile.WebApi.Services;
using SubcontractProfile.WebApi.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using SubcontractProfile.WebApi.Services.Services;
using Repository;

namespace SubcontractProfile.WebApi.IoC.Configuration.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services != null)
            {
                services.AddTransient<IUserService, UserService>();
                services.AddScoped<ISubcontractProfileAddressRepo, SubcontractProfileAddressRepo>();
                services.AddScoped<ISubcontractProfileAddressTypeRepo, SubcontractProfileAddressTypeRepo>();
                services.AddScoped<ISubcontractProfileAsstEngineerRepo, SubcontractProfileAsstEngineerRepo>();
                services.AddScoped<ISubcontractProfileBankingRepo, SubcontractProfileBankingRepo>();
                services.AddScoped<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                services.AddScoped<ISubcontractProfileDistrictRepo, SubcontractProfileDistrictRepo>();
                services.AddScoped<ISubcontractProfileEngineerRepo, SubcontractProfileEngineerRepo>();
                services.AddScoped<ISubcontractProfileLocationRepo, SubcontractProfileLocationRepo>();
                services.AddScoped<ISubcontractProfileNationalityRepo, SubcontractProfileNationalityRepo>();
                services.AddScoped<ISubcontractProfilePaymentRepo, SubcontractProfilePaymentRepo>();
                services.AddScoped<ISubcontractProfilePersonalRepo, SubcontractProfilePersonalRepo>();
                services.AddScoped<ISubcontractProfileProvinceRepo, SubcontractProfileProvinceRepo>();
                services.AddScoped<ISubcontractProfileRaceRepo, SubcontractProfileRaceRepo>();
                services.AddScoped<ISubcontractProfileReligionRepo, SubcontractProfileReligionRepo>();
                services.AddScoped<ISubcontractProfileSubDistrictRepo, SubcontractProfileSubDistrictRepo>();
                services.AddScoped<ISubcontractProfileTeamRepo, SubcontractProfileTeamRepo>();
                services.AddScoped<ISubcontractProfileTitleRepo, SubcontractProfileTitleRepo>();
                services.AddScoped<ISubcontractProfileTrainingRepo, SubcontractProfileTrainingRepo>();
                services.AddScoped<ISubcontractProfileUserRepo, SubcontractProfileUserRepo>();
                services.AddScoped<ISubcontractProfileVerhicleBrandRepo, SubcontractProfileVerhicleBrandRepo>();
                services.AddScoped<ISubcontractProfileVerhicleSeriseRepo, SubcontractProfileVerhicleSeriseRepo>();
                services.AddScoped<ISubcontractProfileVerhicleTypeRepo, SubcontractProfileVerhicleTypeRepo>();
                services.AddScoped<ISubcontractProfileWarrantyRepo, SubcontractProfileWarrantyRepo>();
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
