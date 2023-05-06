using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using WineQuality.Infrastructure.Persistence;

namespace WineQuality.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomRequestLocalization(this IApplicationBuilder app, IConfiguration configuration)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var scope = scopeFactory.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var supportedCultures = context.Cultures
            .AsNoTracking()
            .Select(c => new CultureInfo(c.CultureCode))
            .ToList();

        var requestLocalizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US")),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            }
        };

        return app.UseRequestLocalization(requestLocalizationOptions);
    }
}