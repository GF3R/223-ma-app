using System.Security.Claims;
using L_Bank_W_Backend.Core.Models;
using L_Bank_W_Backend.DbAccess;
using L_Bank_W_Backend.DbAccess.Repositories;
using L_Bank_W_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace L_Bank_W_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        private readonly ILedgerRepository ledgerRepository;

        public LedgersController(ILedgerRepository ledgerRepository)
        {
            this.ledgerRepository = ledgerRepository;
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrators,Users")]
        public IEnumerable<Ledger> Get()
        {
            var allLedgers = this.ledgerRepository.GetAllLedgers();
            return allLedgers;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrators,Users")]
        public Ledger? Get(int id)
        {
            var ledger = this.ledgerRepository.SelectOne(id);
            return ledger;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrators")]
        public void Put(int id, [FromBody] Ledger ledger)
        {
            this.ledgerRepository.Update(ledger);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrators")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.ledgerRepository.DeleteLedger(id);
                return Ok(new { Message = $"Ledger with ID {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

    }
}
