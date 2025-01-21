using SIMA.Domain.Models.Features.Notifications.Messages.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Notifications.Messages
{
    public class MessageDomainService : IMessageDomainService
    {
        private readonly SIMADBContext _context;

        public MessageDomainService(SIMADBContext context)
        {
            _context = context;
        }
        
    }
}
