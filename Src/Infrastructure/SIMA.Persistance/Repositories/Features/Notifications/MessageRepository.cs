using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Notifications.Messages.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Notifications
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly SIMADBContext _context;
        public MessageRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Message> GetById(long id)
        {
            var result = await _context.Messages
                .Include(x=>x.MessageAttachments)
                .Include(x=>x.MessagePositionDisplay)
                .Include(x=>x.MessageGroupDisplay)
                .FirstOrDefaultAsync(ip => ip.Id == new MessageId(id));
            result.NullCheck();
            return result;
        }
    }
}
