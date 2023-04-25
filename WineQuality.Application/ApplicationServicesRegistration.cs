using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Services;

namespace WineQuality.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // services.AddSingleton<EcgTimerManager>();
        // services.AddSingleton<HeartRateTimerManager>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IProcessPhaseTypeService, ProcessPhaseTypeService>();
        services.AddScoped<IProcessParameterService, ProcessParameterService>();
        services.AddScoped<IGrapeSortService, GrapeSortService>();
        // services.AddScoped<IDoctorService, DoctorService>();
        // services.AddScoped<IPatientService, PatientService>();
        // services.AddScoped<IHospitalAdministratorService, HospitalAdministratorService>();
        // services.AddScoped<IPatientCaretakerService, PatientCaretakerService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }
}