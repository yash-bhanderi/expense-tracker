using System.Reflection;
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
            .RegisterRepositories()
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
    
    private static IServiceCollection RegisterInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        const string dbConnectionString = "Host=localhost;" +
                                              "Database=practice;" +
                                              "Username=postgres;" +
                                              "Password=yash2002;" +
                                              "SSL Mode=Disable;" +
                                              "Trust Server Certificate=true;";
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                $"{dbConnectionString}",
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
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
        services.AddSingleton<IErrorResponseProvider, ErrorResponseProvider>();
        return services;
    }
}