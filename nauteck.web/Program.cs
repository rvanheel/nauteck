using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

using nauteck.core.Features.Account;
using nauteck.persistence;

using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

var azureStorageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var dbConnectionString = $"Server={host};port={port};user id={user};password={password};database={database};SslMode=Required;";

// Add services to the container.
var services = builder.Services;


services
    .AddHttpContextAccessor()
    .AddLogging()
    .AddMemoryCache()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInQuery).Assembly))
    .AddDbContext<AppDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseMySQL(dbConnectionString, options =>
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            options.EnableRetryOnFailure();
        });
    });
    //.AddAzureClients(config => config.AddBlobServiceClient(azureStorageConnectionString));

// Add services to the container.
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