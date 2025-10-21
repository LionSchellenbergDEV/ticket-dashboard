using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ticket_dashboard.Models;
using ticket_dashboard.Repositories.Interfaces;
using MailKit;
using System.Linq;

namespace ticket_dashboard.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        // Konstante für die Seitengröße
        private const int PageSize = 10;

        private readonly ticket_dashboard.Repositories.Interfaces.IMailService _mailService;
        // HINWEIS: Sie benötigen ggf. den TicketService, aber wir lassen ihn hier weg, 
        // da ProcessMailAsTicketAsync die Ticket-Erstellung kapselt.

        public MailController(ticket_dashboard.Repositories.Interfaces.IMailService mailService)
        {
            _mailService = mailService;
        }

        // GET: /Mail/Index?page=1
        // Zeigt die paginierte Liste der Mails an.
        public async Task<IActionResult> Index(int? page) // <--- Parameter hinzugefügt
        {
            // Die aktuelle Seite bestimmen (Standard ist 1)
            int pageNumber = page ?? 1;

            // 1. Alle Mails abrufen (Ineffizient, aber notwendig ohne IMAP-Paging-Unterstützung)
            // Die Methode GetMailPreviewsAsync gibt IEnumerable<MailModel> zurück
            var allMails = (await _mailService.GetMailPreviewsAsync()).ToList();

            // 2. Paginierungslogik
            var count = allMails.Count;
            var items = allMails
                .Skip((pageNumber - 1) * PageSize) // Mails überspringen
                .Take(PageSize) // 10 Mails nehmen
                .ToList();

            // 3. Paging ViewModel erstellen und an die View übergeben
            var paginatedList = new PaginatedList<MailModel>(items, count, pageNumber, PageSize);

            // Die View erwartet nun PaginatedList<MailModel>
            return View(paginatedList);
        }

        // GET: /Mail/Details?uid=...
        // Zeigt die vollständigen Details einer Mail an
        public async Task<IActionResult> Details(uint uid)
        {
            // UniqueId.TryParse erfordert string, daher die Konvertierung
            // ACHTUNG: Die UID ist ein uint, aber das Formular überträgt einen String.
            // Der Controller akzeptiert uint, muss aber den String für TryParse bereitstellen.
            if (!UniqueId.TryParse(uid.ToString(), out UniqueId mailUid))
            {
                return NotFound("Ungültige E-Mail ID.");
            }

            // Fehler 2.1 wird hier behoben: Die View muss nur noch das geladene Model rendern können.
            var mailDetail = await _mailService.GetMailDetailsAsync(mailUid);

            return View(mailDetail);
        }

        // POST: /Mail/Assign
        // Löst die Zuweisung, Ticket-Erstellung und IMAP-Verschiebung aus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(MailAssignViewModel model)
        {
            // 1. Validierung der UID
            if (!UniqueId.TryParse(model.Uid, out UniqueId uid))
            {
                return BadRequest("Ungültige Mail ID für die Zuweisung.");
            }

            // 2. Aufruf der Logik (erstellt Ticket und verschiebt die Mail)
            bool success = await _mailService.ProcessMailAsTicketAsync(uid);

            if (success)
            {
                // Platzhalter für die Weiterleitung zur Ticket-Detailseite
                return RedirectToAction("Details", "Ticket", new { id = 1 });
            }

            // Fehlerfall
            TempData["Error"] = "Fehler bei der Ticket-Erstellung oder beim Verschieben der E-Mail.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Body(uint uid)
        {
            if (!UniqueId.TryParse(uid.ToString(), out UniqueId mailUid))
                return NotFound();

            var mailDetail = await _mailService.GetMailDetailsAsync(mailUid);
            return Content(mailDetail.Body ?? "", "text/html; charset=utf-8");
        }

    }
}