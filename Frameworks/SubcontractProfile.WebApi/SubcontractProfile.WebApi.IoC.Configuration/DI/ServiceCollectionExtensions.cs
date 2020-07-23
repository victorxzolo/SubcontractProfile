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
                services.AddTransient<IDbContext, DbContext>();
                services.AddTransient<ISubcontractProfileAddressRepo, SubcontractProfileAddressRepo>();
                services.AddTransient<ISubcontractProfileAsstEngineerRepo, SubcontractProfileAsstEngineerRepo>();
                services.AddTransient<ISubcontractProfileBankingRepo, SubcontractProfileBankingRepo>();
                services.AddTransient<ISubcontractProfileCompanyRepo, SubcontractProfileCompanyRepo>();
                services.AddTransient<ISubcontractProfileDistrictRepo, SubcontractProfileDistrictRepo>();
                services.AddTransient<ISubcontractProfileEngineerRepo, SubcontractProfileEngineerRepo>();
                services.AddTransient<ISubcontractProfileLocationRepo, SubcontractProfileLocationRepo>();
                services.AddTransient<ISubcontractProfileNationalityRepo, SubcontractProfileNationalityRepo>();
                services.AddTransient<ISubcontractProfilePaymentRepo, SubcontractProfilePaymentRepo>();
                services.AddTransient<ISubcontractProfilePersonalRepo, SubcontractProfilePersonalRepo>();
                services.AddTransient<ISubcontractProfileProvinceRepo, SubcontractProfileProvinceRepo>();
                services.AddTransient<ISubcontractProfileRaceRepo, SubcontractProfileRaceRepo>();
                services.AddTransient<ISubcontractProfileReligionRepo, SubcontractProfileReligionRepo>();
                services.AddTransient<ISubcontractProfileSubDistrictRepo, SubcontractProfileSubDistrictRepo>();
                services.AddTransient<ISubcontractProfileTeamRepo, SubcontractProfileTeamRepo>();
                services.AddTransient<ISubcontractProfileTitleRepo, SubcontractProfileTitleRepo>();
                services.AddTransient<ISubcontractProfileTrainingRepo, SubcontractProfileTrainingRepo>();
                services.AddTransient<ISubcontractProfileUserRepo, SubcontractProfileUserRepo>();
                services.AddTransient<ISubcontractProfileVerhicleBrandRepo, SubcontractProfileVerhicleBrandRepo>();
                services.AddTransient<ISubcontractProfileVerhicleSeriseRepo, SubcontractProfileVerhicleSeriseRepo>();
                services.AddTransient<ISubcontractProfileVerhicleTypeRepo, SubcontractProfileVerhicleTypeRepo>();
                services.AddTransient<ISubcontractProfileWarrantyRepo, SubcontractProfileWarrantyRepo>();
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
