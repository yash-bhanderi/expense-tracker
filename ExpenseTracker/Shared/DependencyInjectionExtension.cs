using System.Reflection;
using CodeCommandos.Domain;
using CodeCommandos.Shared.Helper.CacheManager;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace CodeCommandos.Shared;

public static class DependencyInjectionExtension
{
    public static void AddServiceExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterMapper()
            .RegisterInfraStructure(configuration)
            .RegisterServices(configuration)
            .RegisterRedisService(configuration);
    }
    
    private static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetAssembly(typeof(DependencyInjectionExtension))!);
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
        return services;
    }
    
    private static IServiceCollection RegisterInfraStructure(this IServiceCollection services,  IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LocalDb");
        string migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Error);
            builder.AddConsole();
        });
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.SetMinimumLevel(LogLevel.Warning);
            loggingBuilder.AddConsole();
        });
        services.AddDbContext<ExpenseTrackingContext>(options =>
        {
            options.UseLoggerFactory(loggerFactory);
            options.UseSqlServer(connectionString, ob => { ob.MigrationsAssembly(migrationsAssembly); });
        });

        return services;
    }
    
    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        return services;
    }
    
    private static IServiceCollection RegisterRedisService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
        });
        services.AddTransient(typeof(ICacheService<>), typeof(CacheService<>));

        //Register cache repository
        return services;
    }
    
    private static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}