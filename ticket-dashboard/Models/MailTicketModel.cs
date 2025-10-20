using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ticket_dashboard.Models
{
    public class MailTicketModel
    {
        [Key]
        public int MailTicketId { get; set; }
        public string MailId { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string? Body { get; set; }    
        public DateTime ReceivedAt { get; set; }
        public int? TicketId { get; set; }
        [ForeignKey("TicketId")]
        public TicketModel? Ticker { get; set;}
    }
}
