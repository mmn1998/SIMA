using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Contracts
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message> GetById(long id);
    }
}
