using FamilyHubs.ServiceDirectoryCaseManagement.Api;
using FamilyHubs.ServiceDirectoryCaseManagement.Api.Endpoints;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Interceptors;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Repository;
using FamilyHubs.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    

    var genservice = scope.ServiceProvider.GetService<MinimalGeneralEndPoints>();
    if (genservice != null)
        genservice.RegisterMinimalGeneralEndPoints(app);

    try
    {
        /*
        IDomainEventDispatcher dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor = scope.ServiceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();

        

        DbContextOptions<ApplicationDbContext> options;

        if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase("FH-LAHubDb").Options;
        }
        else if (builder.Configuration.GetValue<bool>("UseSqlServerDatabase"))
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                             .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                             .Options;
        }
        else
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                             .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                             .Options;
        }

        ApplicationDbContext applicationDbContext = new ApplicationDbContext(options, dispatcher, auditableEntitySaveChangesInterceptor);
        */

        //var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


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
