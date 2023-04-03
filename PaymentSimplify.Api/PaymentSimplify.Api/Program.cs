using PaymentSimplify.Common.Settings;
using PaymentSimplify.Infra;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

//add settings
builder.Services.AddSingleton(appSettings);

builder.Services.AddInfrastructureServices(appSettings);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();