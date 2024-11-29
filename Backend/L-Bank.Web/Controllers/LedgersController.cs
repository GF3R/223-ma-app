using L_Bank_W_Backend.Core.Models;
using L_Bank_W_Backend.DbAccess.Repositories;
using L_Bank_W_Backend.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<Ledger>> Get()
        {
            var allLedgers = await this.ledgerRepository.GetAllLedgers();
            return allLedgers;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrators,Users")]
        public Ledger? Get(int id)
        {
            var ledger = this.ledgerRepository.SelectOne(id);
            return ledger;
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public IActionResult Post([FromBody] CreateLedgerDto ledger)
        {
            if (string.IsNullOrEmpty(ledger.Name))
            {
                return BadRequest("Ledger name is required");
            }

            ledgerRepository.CreateLedger(ledger.Name);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrators")]
        public void Put(int id, [FromBody] Ledger ledger)
        {
            this.ledgerRepository.Update(ledger);
        }
    }
}