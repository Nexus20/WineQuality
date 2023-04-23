using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WineQuality.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // services.AddSingleton<EcgTimerManager>();
        // services.AddSingleton<HeartRateTimerManager>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // services.AddScoped<IHospitalService, HospitalService>();
        // services.AddScoped<IDoctorService, DoctorService>();
        // services.AddScoped<IPatientService, PatientService>();
        // services.AddScoped<IHospitalAdministratorService, HospitalAdministratorService>();
        // services.AddScoped<IPatientCaretakerService, PatientCaretakerService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }
}