using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using L_Bank_W_Backend.Core.Models;
using Microsoft.Extensions.Options;
using L_Bank_W_Backend;
using L_Bank_W_Backend.DbAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace L_Bank_W_Backend.DbAccess.Repositories;

public class LedgerRepository : ILedgerRepository
{
    private readonly DatabaseSettings databaseSettings;
    private readonly AppDbContext _context;
    
    public LedgerRepository(IOptions<DatabaseSettings> databaseSettings, AppDbContext context)
    {
        this._context = context;
        this.databaseSettings = databaseSettings.Value;
    }
    
    public void Book(decimal amount, Ledger from, Ledger to)
    {
        from.Balance -= amount;
        this.Update(from);
        // Complicate calculations
        Thread.Sleep(250);
        to.Balance += amount;
        this.Update(to);
    }
    
    public decimal GetTotalMoney()
    {
        const string query = @$"SELECT SUM(balance) AS TotalBalance FROM {Ledger.CollectionName}";
        decimal totalBalance = 0;

        using (SqlConnection conn = new SqlConnection(this.databaseSettings.ConnectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalBalance = Convert.ToDecimal(result);
                }
            }
        }

        return totalBalance;
    }

    public async Task<IEnumerable<Ledger>> GetAllLedgers()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var allLedgers = await _context.Ledgers
                .OrderBy(ledger => ledger.Name)
                .ToListAsync();

            await transaction.CommitAsync();

            return allLedgers;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    public Ledger? SelectOne(int id)
    {
        Ledger? retLedger = null;
        bool worked;

        do
        {
            worked = true;
            using (SqlConnection conn = new SqlConnection(this.databaseSettings.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        retLedger = SelectOne(id, conn, transaction);
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        //Console.WriteLine("  Message: {0}", ex.Message);

                        // Attempt to roll back the transaction.
                        try
                        {
                            transaction.Rollback();
                            if (ex.GetType() != typeof(Exception))
                                worked = false;
                        }
                        catch (Exception ex2)
                        {
                            // Handle any errors that may have occurred on the server that would cause the rollback to fail.
                            //Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            //Console.WriteLine("  Message: {0}", ex2.Message);
                            if (ex2.GetType() != typeof(Exception))
                                worked = false;
                        }
                    }
                }
            }
        } while (!worked);

        return retLedger;
    }

    public Ledger? SelectOne(int id, SqlConnection conn, SqlTransaction? transaction)
    {
        Ledger? retLedger;
        const string query = @$"SELECT id, name, balance FROM {Ledger.CollectionName} WHERE id=@Id";

        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        {
            cmd.Parameters.AddWithValue("@Id", id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                    throw new Exception($"No Ledger with id {id}");

                int ordId = reader.GetInt32(reader.GetOrdinal("id"));
                string ordName = reader.GetString(reader.GetOrdinal("name"));
                decimal ordBalance = reader.GetDecimal(reader.GetOrdinal("balance"));

                retLedger = new Ledger { Id = ordId, Name = ordName, Balance = ordBalance };
            }
        }

        return retLedger;
    }

    public void Update(Ledger ledger, SqlConnection conn, SqlTransaction? transaction)
    {
        const string query = $"UPDATE {Ledger.CollectionName} SET name=@Name, balance=@Balance WHERE id=@Id";
        using (var cmd = new SqlCommand(query, conn, transaction))
        {
            cmd.Parameters.AddWithValue("@Name", ledger.Name);
            cmd.Parameters.AddWithValue("@Balance", ledger.Balance);
            cmd.Parameters.AddWithValue("@Id", ledger.Id);

            // Execute the command
            cmd.ExecuteNonQuery();
        }
    }

    public void Update(Ledger ledger)
    {
        using (SqlConnection conn = new SqlConnection(this.databaseSettings.ConnectionString))
        {
            conn.Open();
            this.Update(ledger, conn, null);
        }
    }
    
    public decimal? GetBalance(int ledgerId, SqlConnection conn, SqlTransaction transaction)
    {
        const string query = @"SELECT balance FROM ledgers WHERE id=@Id";

        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        {
            cmd.Parameters.AddWithValue("@Id", ledgerId);
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                return Convert.ToDecimal(result);
            }
        }

        return null;
    }
}