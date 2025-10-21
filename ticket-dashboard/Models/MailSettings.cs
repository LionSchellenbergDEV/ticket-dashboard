using System.ComponentModel.DataAnnotations;

namespace ticket_dashboard.Models
{
    public class MailSettings
    {
        [Required]
        public string ImapServer { get; set; } = string.Empty;
        [Required]
        public int ImapPort { get; set; } = 0;
        [Required]
        public bool UseSsl { get; set; } = true;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public string ProcessedFolderName { get; set; } = "Zugewiesene Mails";
    }
}
