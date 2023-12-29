using CoreStorage.AppContext;
using Microsoft.EntityFrameworkCore;

namespace BussinessManager.ModuleApplication.SqlService;

public static class SqlServices
{
    public static void StartSqlService(this IServiceCollection service, IConfiguration configuration)
    {
        var sqlConnection = configuration.GetConnectionString("DefaultConnection");
        service.AddDbContextPool<ApplicationContext>(context =>
        {
            context.UseSqlServer(sqlConnection, x =>
            {
                x.EnableRetryOnFailure(3);
                x.MinBatchSize(10);
                x.MaxBatchSize(100);
            });
            context.AddInterceptors();
            context.EnableDetailedErrors();
            context.EnableSensitiveDataLogging();
            context.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}