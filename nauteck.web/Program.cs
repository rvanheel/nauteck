using MediatR;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Azure;

using MySql.EntityFrameworkCore.Diagnostics;

using nauteck.core.Abstraction;
using nauteck.core.Features.Account;
using nauteck.core.Features.Floor.Color;
using nauteck.core.Implementation;
using nauteck.persistence;

using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

var azureStorageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var dbConnectionString = $"Server={host};port={port};user id={user};password={password};database={database};SslMode=Required;CharSet=utf8mb4;";

// Add services to the container.
var services = builder.Services;


services
    .AddHttpContextAccessor()
    .AddLogging()
    .AddMemoryCache()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInQuery).Assembly))
    .AddSingleton<IHelper, Helper>()
    .AddDbContext<AppDbContext>(optionsBuilder =>
    {
        //optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        optionsBuilder.UseMySQL(dbConnectionString, options =>
        {
            options
                .EnableRetryOnFailure(maxRetryCount: 5,
                                         maxRetryDelay: TimeSpan.FromSeconds(10),
                                         errorNumbersToAdd: null)
                //.MaxBatchSize(100)
                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                ;
        });
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