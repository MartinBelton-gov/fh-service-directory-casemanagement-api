using FamilyHubs.ServiceDirectoryCaseManagement.Api;
using FamilyHubs.ServiceDirectoryCaseManagement.Api.Endpoints;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Infrastructure;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Repository;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
                .AddInfrastructureServices(builder.Configuration)
                .AddApplicationServices();

builder.Services.AddTransient<MinimalGeneralEndPoints>();
builder.Services.AddTransient<MinimalReferralEndPoints>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    

    var genapi = scope.ServiceProvider.GetService<MinimalGeneralEndPoints>();
    if (genapi != null)
        genapi.RegisterMinimalGeneralEndPoints(app);

    var referralApi = scope.ServiceProvider.GetService<MinimalReferralEndPoints>();
    if (referralApi != null)
        referralApi.RegisterReferralEndPoints(app);



    try
    {
        var db = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();


        // Seed Database
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync(builder.Configuration);
        await initialiser.SeedAsync();

    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
        if (logger != null)
            logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();
