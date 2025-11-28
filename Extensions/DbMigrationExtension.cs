using CarePlusApi.Data;
using Microsoft.EntityFrameworkCore;
public static class DbMigrationExtension
{
    public static void MigrateWithRetry(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var strategy = db.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            db.Database.Migrate();
            Console.WriteLine("Database migration completed successfully.");
        });
    }
}

