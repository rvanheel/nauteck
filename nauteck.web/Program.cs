using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;


using nauteck.core.Abstraction;
using nauteck.core.Features.Account;
using nauteck.core.Implementation;
using nauteck.data.Configuration;

using System.Data;
using System.Globalization;

using MySql.Data.MySqlClient;
using Dapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<DatabaseSettings>()
    .Configure(options =>
    {
        options.Host = Environment.GetEnvironmentVariable("DB_HOST");
        options.Port = Environment.GetEnvironmentVariable("DB_PORT");
        options.Database = Environment.GetEnvironmentVariable("DB_NAME");
        options.User = Environment.GetEnvironmentVariable("DB_USER");
        options.Password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    });

var azureStorageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

// Add services to the container.
var services = builder.Services;


services
    .AddHttpContextAccessor()
    .AddLogging()
    .AddMemoryCache()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInQuery).Assembly))
    .AddSingleton<IHelper, Helper>()
    .AddTransient<IDbConnection>(sp =>
    {
        var dbSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
        return new MySqlConnection(dbSettings.ConnectionString);
    });
//.AddAzureClients(config => config.AddBlobServiceClient(azureStorageConnectionString));


builder.Services.AddControllersWithViews();


services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = _ => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
SetUpDevelopmentMode(app);
SetUpLocalization(app);

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
return;

static void SetUpDevelopmentMode(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        return;
    }
    app.UseExceptionHandler("/Home/Error");
}

static void SetUpLocalization(IApplicationBuilder app)
{
    var ci = new CultureInfo("nl-NL")
    {
        NumberFormat =
        {
            CurrencyDecimalSeparator = ".",
            NumberDecimalSeparator = "."
        }
    };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(ci),
        SupportedCultures = [ci],
        SupportedUICultures = [ci]
    });
}