namespace InstaManager.ModuleApplication.SeedApplication;

public static class InitialScopeService
{
    public static void StartInitialService(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        databaseInitializer.SeedData();
    }
}