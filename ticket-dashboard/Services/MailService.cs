using ticket_dashboard.Models;
using ticket_dashboard.Services.Repository;

namespace ticket_dashboard.Services
{
    public class MailService
    {
        private readonly Repository<MailTicket> _repository;

        public MailService(Repository<MailTicket> repository)
        {
            _repository = repository;
        }

        public List<MailTicket> GetUnassignedMails()
        {
            return _repository.GetAll().Where(m => !m.IsAssigned).ToList();
        }
    }
}
