using Bank.DbAccess;
using Bank.DbAccess.Data;
using Bank.DbAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TestProject1;

public class TestProjectFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; }

    public TestProjectFixture()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;
        services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, new MariaDbServerVersion(new Version(11, 6, 2))));
        var dbSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>() ?? throw new InvalidOperationException();
        var
            options = Microsoft.Extensions.Options.Options
                .Create(dbSettings); // this is needed to ensure compatability with the web application
        services.AddSingleton<IOptions<DatabaseSettings>>(options);
        services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
        services.AddTransient<ILedgerRepository, LedgerRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IBookingRepository, BookingRepository>();
        ServiceProvider =
            services.BuildServiceProvider(); // Ensure the database is created
        using var scope = ServiceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var scope = ServiceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
}