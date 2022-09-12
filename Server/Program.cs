using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var webApplicationOptions =
    new WebApplicationOptions
    {
        EnvironmentName =
            System.Diagnostics.Debugger.IsAttached ?
            Environments.Development
            :
            Environments.Production,
    };

var builder =
    WebApplication.CreateBuilder(options: webApplicationOptions);

builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;

    options.SuppressCheckForUnhandledSecurityMetadata = false;
});

builder.Services.AddRazorPages();

builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>
    (builder.Configuration.GetSection(key: Infrastructure.Settings.ApplicationSettings.KeyName))
    .AddSingleton
    (implementationFactory: serviceType =>
    {
        var result =
            serviceType.GetRequiredService
            <Microsoft.Extensions.Options.IOptions
            <Infrastructure.Settings.ApplicationSettings>>().Value;

        return result;
    });

Infrastructure.DIServicesBootstrapper.ServicesBootstrapper(builder.Services);

builder.Services
    .AddAuthentication(defaultScheme: Infrastructure.Security.Utility.AuthenticationScheme)
    .AddCookie(authenticationScheme: Infrastructure.Security.Utility.AuthenticationScheme);

var connectionString =
    builder.Configuration.GetConnectionString(name: "ConnectionString");

builder.Services.AddDbContext<Persistence.DatabaseContext>
    (optionsAction: options =>
    {
        //options
        //	// using Microsoft.EntityFrameworkCore;
        //	.UseLazyLoadingProxies();

        options
            .UseSqlServer(connectionString: connectionString);
    });
var app =
    builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}
else {
    app.UseGlobalException();

    app.UseExceptionHandler("/Errors/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseActivationKeys();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCultureCookie();

app.MapRazorPages();

app.Run();
