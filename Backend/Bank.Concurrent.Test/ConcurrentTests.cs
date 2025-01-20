using Bank.DbAccess.Data;
using Bank.DbAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Bank.Concurrent.Test


{
    public class ConcurrentTests : TestBed<TestProjectFixture>
    {
        private readonly AppDbContext? _context;


        public ConcurrentTests(ITestOutputHelper testOutputHelper, TestProjectFixture fixture)
            : base(testOutputHelper, fixture)
        {
            _context = fixture.ServiceProvider.GetService<AppDbContext>();
        }


        [Fact]
        public async Task TestBookingParallel()
        {
            var bookingRepository = _fixture.ServiceProvider.GetService<IBookingRepository>();
            const int numberOfBookings = 1;
            const int users = 2;

            Task[] tasks = new Task[users];
            var allLedgers = await _context.Ledgers.OrderBy(ledger => ledger.Name).ToArrayAsync();

            async Task UserAction(int bookingsCount, decimal startingMoney)
            {
                Random random = new Random();
                for (int i = 0; i < numberOfBookings; i++)
                {
                    // Book methode testen
                    // Implementieren Sie hier die parallelen Buchungen
                    // Bestimmen sie zwei zufällige Ledgers
                    var from = allLedgers[random.Next(allLedgers.Length)];
                    var to = allLedgers[random.Next(allLedgers.Length)];
                    var amount = random.NextInt64(1, 101);
                    await bookingRepository.Book(from.Id, to.Id, amount);
                }
            }

            for (int i = 0; i < users; i++)
            {
                tasks[i] = Task.Run(() => UserAction(numberOfBookings, 1000));
            }

            Task.WaitAll(tasks);
        }
    }
}