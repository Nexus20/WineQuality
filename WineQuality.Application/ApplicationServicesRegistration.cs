using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Services;

namespace WineQuality.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // services.AddSingleton<EcgTimerManager>();
        // services.AddSingleton<HeartRateTimerManager>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IProcessPhaseService, ProcessPhaseService>();
        services.AddScoped<IProcessParameterService, ProcessParameterService>();
        services.AddScoped<IGrapeSortService, GrapeSortService>();
        services.AddScoped<IGrapeSortStandardService, GrapeSortStandardService>();
        services.AddScoped<IWineMaterialBatchService, WineMaterialBatchService>();

        services.AddHttpClient<IModelTrainingService, ModelTrainingService>(c => 
            c.BaseAddress = new Uri(configuration["MachineLearningSettings:ServiceAddress"]));
        // services.AddScoped<IDoctorService, DoctorService>();
        // services.AddScoped<IPatientService, PatientService>();
        // services.AddScoped<IHospitalAdministratorService, HospitalAdministratorService>();
        // services.AddScoped<IPatientCaretakerService, PatientCaretakerService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }
}