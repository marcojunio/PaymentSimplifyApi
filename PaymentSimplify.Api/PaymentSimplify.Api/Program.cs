using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PaymentSimplify.Api;
using PaymentSimplify.Application;
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
builder.Services.AddAuthorization()
    .AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(f =>
{
    f.SaveToken = true;
    f.RequireHttpsMetadata = false;
    f.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = appSettings.AuthenticatedSettings.ValidAudience,
        ValidIssuer = appSettings.AuthenticatedSettings.ValidIssuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(appSettings.AuthenticatedSettings.Key)),
    };
});

builder.Services.AddResponseCompression();

builder.Services.AddMvc();
builder.Services
    .AddControllers()
    .AddJsonOptions(f =>
    {
        f.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        f.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSwaggerGen();

//add settings
builder.Services.AddSingleton(appSettings);

//infra
builder.Services.AddInfrastructureServices(appSettings);

//application
builder.Services.AddConfigurationsApplication();

//api
builder.Services.AddConfigurationApi();

//Http client factory
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseRequestLocalization(options => options
    .AddSupportedCultures("pt", "en")
    .AddSupportedUICultures("pt", "en")
    .SetDefaultCulture(new []{"pt", "en"}.First()));

app.UseResponseCompression();

app.MapControllers();

app.Run();