using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using ticket_dashboard.Models;

namespace ticket_dashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<MailTicket> MailTickets { get; set; }
    }
}
