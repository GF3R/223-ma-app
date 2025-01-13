using Bank.DbAccess.Data;
using Microsoft.Extensions.Options;

namespace Bank.DbAccess.Repositories;

public class BookingRepository(IOptions<DatabaseSettings> settings, AppDbContext context) : IBookingRepository
{
    private DatabaseSettings _settings = settings.Value;

    public bool Book(int sourceLedgerId, int destinationLKedgerId, decimal amount)
    {
        // Machen Sie eine Connection und eine Transaktion

        // In der Transaktion:

        // Schauen Sie ob genügend Geld beim Spender da ist
        // Führen Sie die Buchung durch und UPDATEn Sie die ledgers
        // Beenden Sie die Transaktion
        // Bei einem Transaktionsproblem: Restarten Sie die Transaktion in einer Schleife 

        return false; // Lösch mich
    }
}