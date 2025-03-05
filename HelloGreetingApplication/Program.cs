using System;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using NLog;
using NLog.Web;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

//var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//logger.Info("Application is starting...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog Setup
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    builder.Services.AddDbContext<HelloGreetingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HelloAppContext")));



    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/error"); // Proper error handling in production
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    // Run Database Migration
    MigrateDb(app.Services);

    app.Run();
}
catch (Exception ex)
{
   // logger.Error(ex, "Application stopped due to an exception.");
}
finally
{
    //LogManager.Shutdown();
}

// **Database Migration Function**
static void MigrateDb(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<HelloGreetingContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        //var logger = LogManager.GetCurrentClassLogger();
        //logger.Error(ex, "Database migration failed.");
    }
}
