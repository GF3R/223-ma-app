using System;
using Bank.Core.Models;
using Bank.DbAccess.Data;
using Bank.DbAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using TestProject1;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;
using static Bank.Core.Models.Ledger;
using static Bank.DbAccess.Repositories.IBookingRepository;

namespace BookingTest
{
    public class BookingRepositoryTest : TestBed<TestProjectFixture>
    {
        private readonly AppDbContext? _context;

        public BookingRepositoryTest(ITestOutputHelper testOutputHelper, TestProjectFixture fixture)
            : base(testOutputHelper, fixture)
        {
            _context = fixture.ServiceProvider.GetService<AppDbContext>();
        }

        [Fact]
        public void Book_TransferValidAmountBetweenLedgers()
        {
            var bookingRepository = _fixture.ServiceProvider.GetService<IBookingRepository>()!;
            var sourceLedgerId = 1;
            var destinationLedgerId = 2;
            decimal amount = 100;
            
            var sourceLedger = new Ledger { Id = sourceLedgerId, Balance = 200};
            var destinationLedger = new Ledger { Id = destinationLedgerId, Balance = 50};
            
            bookingRepository.Book(sourceLedger.Id, destinationLedger.Id, amount);
            
            Assert.Equal(100, sourceLedger.Balance);
            Assert.Equal(150, destinationLedger.Balance);
        }
        [Fact]
        public void Book_TransferMaxValidAmountBetweenLedgers()
        {
            var bookingRepository = _fixture.ServiceProvider.GetService<IBookingRepository>()!;
            var sourceLedgerId = 1;
            var destinationLedgerId = 2;
            decimal amount = 100;
            
            var sourceLedger = new Ledger { Id = sourceLedgerId, Balance = 100};
            var destinationLedger = new Ledger { Id = destinationLedgerId, Balance = 200};
            
            bookingRepository.Book(sourceLedger.Id, destinationLedger.Id, amount);
            
            Assert.Equal(0, sourceLedger.Balance);
            Assert.Equal(300, destinationLedger.Balance);
        }
        [Fact]
        public void Book_TransferInvalidAmountBetweenLedgers()
        {
            var bookingRepository = _fixture.ServiceProvider.GetService<IBookingRepository>()!;
            var sourceLedgerId = 1;
            var destinationLedgerId = 2;
            decimal amount = 100;
            
            var sourceLedger = new Ledger { Id = sourceLedgerId, Balance = 80};
            var destinationLedger = new Ledger { Id = destinationLedgerId, Balance = 200};

            var task = bookingRepository.Book(sourceLedger.Id, destinationLedger.Id, amount);
            Assert.False(task.Result);
        }
    }
}