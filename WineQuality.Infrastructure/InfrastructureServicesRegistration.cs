using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineQuality.Application.Interfaces.Infrastructure;
using WineQuality.Infrastructure.Identity;
using WineQuality.Infrastructure.Persistence;

namespace WineQuality.Infrastructure;

public static class InfrastructureServicesRegistration
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddIdentity<AppUser, AppRole>()
            .AddUserStore<UserStore<AppUser, AppRole, ApplicationDbContext, string, IdentityUserClaim<string>, AppUserRole,
                IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
            .AddRoleStore<RoleStore<AppRole, ApplicationDbContext, string, AppUserRole, IdentityRoleClaim<string>>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddUserManager<UserManager<AppUser>>();
        
        // services.AddScoped<IHealthMeasurementsContext, HealthMeasurementsContext>();
        // services.AddScoped<IMongoHealthMeasurementRepository, MongoHealthMeasurementRepository>();
        //
        // services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
        // services.AddScoped<IHospitalRepository, HospitalRepository>();
        // services.AddScoped<IDoctorRepository, DoctorRepository>();
        // services.AddScoped<IPatientRepository, PatientRepository>();
        // services.AddScoped<IHospitalAdministratorRepository, HospitalAdministratorRepository>();
        // services.AddScoped<IPatientCaretakerRepository, PatientCaretakerRepository>();
        
        services.AddScoped<IIdentityInitializer, IdentityInitializer>();

        return services;
    }
}