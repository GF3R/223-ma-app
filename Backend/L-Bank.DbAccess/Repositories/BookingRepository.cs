using L_Bank_W_Backend.DbAccess.Data;
using L_Bank_W_Backend.Models;
using Microsoft.Extensions.Options;

namespace L_Bank_W_Backend.DbAccess.Repositories
{
    public class BookingRepository(IOptions<DatabaseSettings> settings, AppDbContext dbContext)
        : IBookingRepository
    {
        private DatabaseSettings _settings = settings.Value;

        public bool Book(int sourceLedgerId, int destinationLedgerId, decimal amount)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                var sourceLedger = dbContext.Ledgers.Find(sourceLedgerId);
                var destinationLedger = dbContext.Ledgers.Find(destinationLedgerId);

                if (sourceLedger == null || destinationLedger == null || sourceLedger.Balance < amount)
                {
                    transaction.Rollback();
                    return false;
                }

                sourceLedger.Balance -= amount;

                destinationLedger.Balance += amount;

                dbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return false;
            }

            return true;
        }
    }
}