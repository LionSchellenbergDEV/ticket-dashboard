using System.Collections.Generic; // 💡 Wichtig: Für IEnumerable
using System.Threading.Tasks;
using ticket_dashboard.Models;
using MailKit; // 💡 Wichtig: Für UniqueId

namespace ticket_dashboard.Repositories.Interfaces
{
    public interface IMailService
    {
        // Ruft eine Liste von E-Mail-Vorschauen ab
        Task<IEnumerable<MailModel>> GetMailPreviewsAsync(string folderName = "INBOX");

        // Ruft die vollständigen Details einer E-Mail anhand der UniqueId ab
        // 💡 ACHTUNG: Hier ist es GetMailDetailsAsync (mit 's')
        Task<MailDetailModel> GetMailDetailsAsync(UniqueId uid, string folderName = "INBOX");

        // Erstellt ein Sage-Ticket, weist es dem Benutzer zu und verschiebt die Mail
        Task<bool> ProcessMailAsTicketAsync(UniqueId uid, string sourceFolder = "INBOX");
    }
}