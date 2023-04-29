using System.Reflection;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineQuality.Application.Interfaces.FileStorage;
using WineQuality.Application.Interfaces.Infrastructure;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Infrastructure.Auth;
using WineQuality.Infrastructure.FileStorage;
using WineQuality.Infrastructure.Identity;
using WineQuality.Infrastructure.Persistence;
using WineQuality.Infrastructure.Repositories;

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
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // services.AddScoped<IHospitalRepository, HospitalRepository>();
        // services.AddScoped<IDoctorRepository, DoctorRepository>();
        // services.AddScoped<IPatientRepository, PatientRepository>();
        // services.AddScoped<IHospitalAdministratorRepository, HospitalAdministratorRepository>();
        // services.AddScoped<IPatientCaretakerRepository, PatientCaretakerRepository>();
        
        services.AddScoped<IIdentityInitializer, IdentityInitializer>();
        services.AddScoped<ISignInService, SignInService>();
        services.AddScoped<JwtHandler>();
        
        var blobStorageConnectionString = configuration.GetValue<string>("BlobStorageSettings:ConnectionString");
        services.AddSingleton(x => new BlobServiceClient(blobStorageConnectionString));
        services.AddScoped<IFileStorageService, BlobStorageService>();

        return services;
    }
}