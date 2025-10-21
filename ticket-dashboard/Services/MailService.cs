using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ticket_dashboard.Repositories.Interfaces;
using MimeKit;
using Markdig;
using ticket_dashboard.Models;

namespace ticket_dashboard.Services
{
    public class MailService : ticket_dashboard.Repositories.Interfaces.IMailService
    {
        private readonly MailSettings _config;
        
        public MailService(IOptions<MailSettings> config)
        {
            _config = config.Value;
        }

        private async Task<ImapClient> ConnectAndAuthenticateAsync()
        {
            var client = new ImapClient();

            //SSL/TLS Verbindung
            await client.ConnectAsync(_config.ImapServer, _config.ImapPort, SecureSocketOptions.SslOnConnect);

            //Authentifizierung
            await client.AuthenticateAsync(_config.Username, _config.Password);
            return client;
        }



        public async Task<IEnumerable<MailModel>> GetMailPreviewsAsync(string folderName = "INBOX")
        {
            using var client = await ConnectAndAuthenticateAsync();
            var folder = client.GetFolder(folderName);

            //Odner nur zum Lesen öffnen
            await folder.OpenAsync(FolderAccess.ReadOnly);

            //Alle Nachrichten-IDs im Ordner abrufen
            var uids = await folder.SearchAsync(SearchQuery.All);

            //Die gewünschte Kopfzeilen und das Datum in einem einzifen Aufruf abrufen (effizienter)
            var items = await folder.FetchAsync(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope | MessageSummaryItems.InternalDate);

            var mails = items
                .OrderByDescending(x => x.InternalDate)
                .Select(item => new MailModel
                {
                    Uid = item.UniqueId,
                    Sender = item.Envelope.From.OfType<MailboxAddress>().FirstOrDefault()?.Address ?? "Unbekannt",
                    Subject = item.Envelope.Subject,
                    Date = item.InternalDate ?? DateTimeOffset.MinValue
                }).ToList();

            await client.DisconnectAsync(true);
            return mails;
        }

        public async Task<MailDetailModel> GetMailDetailsAsync(UniqueId uid, string folderName = "INBOX")
        {
            using var client = await ConnectAndAuthenticateAsync();
            var folder = client.GetFolder(folderName);
            await folder.OpenAsync(FolderAccess.ReadOnly);



            var message = await folder.GetMessageAsync(uid);

            string mailBody;
            if (message.HtmlBody != null)
            {
                mailBody = message.HtmlBody;
            } else
            {
                mailBody = message.TextBody;
            }
                var details = new MailDetailModel
                {
                    Uid = uid,
                    Sender = message.From.OfType<MailboxAddress>().FirstOrDefault()?.Address ?? "Unbekannt",
                    Subject = message.Subject,
                    Date = message.Date,
                    Body = mailBody
                };

                await client.DisconnectAsync(true);
            return details;
        }



        public async Task<bool> ProcessMailAsTicketAsync(UniqueId uid, string sourceFolder = "INBOX")
        {
            using var client = await ConnectAndAuthenticateAsync();
            var source = client.GetFolder(sourceFolder);

            //Quelle zum schreiben und verschieben öffnen
            await source.OpenAsync(FolderAccess.ReadWrite);

            //E-Mail für die Ticket-Einstellung abrufen
            var message = await source.GetMessageAsync(uid);

            //Sage Ticket erstellen
            bool ticketCreated = CreateSageTicket(message);

            if (ticketCreated)
            {
                //Zielordner vorbereiten
                var targetFolderName = _config.ProcessedFolderName;
                var destination = client.GetFolder(targetFolderName);

                if (destination == null || !destination.Exists)
                {
                    //Ordner erstellen, falls er nicht schon existiert
                    await destination.CreateAsync(targetFolderName, true);
                    destination = client.GetFolder(targetFolderName);
                }

                //E-Mails in den Zielordner verschieben und im Quellordner löschen
                await source.MoveToAsync(uid, destination);
                //Verbindung trennen
                await client.DisconnectAsync(true);
                return true;
            }

            //Verbindung trennen
            await client.DisconnectAsync(true);
            return false;
        }

        ///<summary>
        ///Aktuelle Platzhalter Methode die später noch durch die entsprechenden API Aufrufe ersetzt wird.
        ///</summary>
        private bool CreateSageTicket(MimeMessage message)
        {
            var subject = message.Subject;
            var sender = message.From.OfType<MailboxAddress>().FirstOrDefault()?.Address;
            var body = message.TextBody ?? message.HtmlBody;

            // TODO: Logik zur Anbindung an die Sage-API hier einfügen
            return true;
        }
    }
}
