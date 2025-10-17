namespace ticket_dashboard.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Offen";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public int? UserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}
