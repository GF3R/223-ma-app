using System.ComponentModel.DataAnnotations.Schema;

namespace L_Bank_W_Backend.Core.Models
{
    public class Booking
    {
        public const string CollectionName = "Booking";
        
        public int Id { get; set; }
        
        [ForeignKey("Source")]
        public int SourceId { get; set; }
    
        [ForeignKey("Destination")]
        public int DestinationId { get; set; }
        
        public Decimal Amount { get; set; }
        
        public Ledger? Source { get; set; }
        public Ledger? Destination { get; set; }
    }
}
