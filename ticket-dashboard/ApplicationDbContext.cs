using Microsoft.EntityFrameworkCore;
using ticket_dashboard.Models;
namespace ticket_dashboard
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<TicketModel> MailTickets { get; set; }
    }
}
