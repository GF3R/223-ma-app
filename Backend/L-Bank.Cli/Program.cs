using L_Bank_W_Backend.DbAccess;
using L_Bank_W_Backend.DbAccess.Data;
using L_Bank_W_Backend.DbAccess.Repositories;
using L_Bank_W_Backend.Services;
using L_Bank.Cli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var configuration = new ConfigurationManager().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
var services = new ServiceCollection();

var dbSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>() ?? throw new InvalidOperationException();
var options = Options.Create(dbSettings); // this is needed to ensure compatability with the web application
services.AddSingleton(options);

services.AddDbContext<AppDbContext>(dbContextOptionsBuilder =>
    dbContextOptionsBuilder.UseSqlServer(
        dbSettings.ConnectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("L-Bank.Web") // this is needed to ensure compatability with the web application
    )
);

// Register services
services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
services.AddTransient<ILedgerRepository, LedgerRepository>();
services.AddTransient<IUserRepository, UserRepository>();

// Build the service provider
var serviceProvider = services.BuildServiceProvider();

var databaseSeeder = serviceProvider.GetRequiredService<IDatabaseSeeder>();
var ledgerRepository = serviceProvider.GetRequiredService<ILedgerRepository>();


Console.WriteLine("The L-Bank.Web");
Console.WriteLine();
try
{
    Console.WriteLine("Initializing database.");
    databaseSeeder.Initialize();
    Console.WriteLine("Seeding data.");
    databaseSeeder.Seed();
}
catch (Exception ex)
{
    Console.WriteLine("Error in initializing database.");
    Console.WriteLine(ex.Message);
}


Console.WriteLine();
Console.WriteLine("All Ledgers:");
var allLedgers = await ledgerRepository.GetAllLedgers();
foreach (var ledger in allLedgers)
{
    Console.WriteLine($"ID: {ledger.Id} Name: {ledger.Name} Balance: {ledger.Balance}.");
}


Console.WriteLine();
Console.WriteLine("Getting total money in system at the start.");
try
{
    decimal startMoney = ledgerRepository.GetTotalMoney();
    Console.WriteLine($"Total start money: {startMoney}");
}
catch (Exception ex)
{
    Console.WriteLine("Error in getting total money.");
    Console.WriteLine(ex.Message);
}

Simple.Run(ledgerRepository);

// WithTransactions.Run(allLedgers, ledgerRepository);
