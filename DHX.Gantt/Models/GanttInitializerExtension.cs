namespace DHX.Gantt.Models
{
    public static class GanttInitializerExtension
    {
        public static IHost InitializeDatabase(this IHost webHost)
        {
            var serviceScopeFactory =
             (IServiceScopeFactory?)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory!.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<GanttContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                GanttSeeder.Seed(dbContext);
            }

            return webHost;
        }
    }
}