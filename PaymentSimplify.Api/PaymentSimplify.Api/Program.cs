using PaymentSimplify.Common.Settings;
using PaymentSimplify.Infra;

new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

//base
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddHttpContextAccessor();

//add settings
builder.Services.AddSingleton(appSettings);

//http context

//infra
builder.Services.AddInfrastructureServices(appSettings);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapGet("/", () => "Hello World!");



app.Run();