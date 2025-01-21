using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Notifications.Messages
{
    public class DeleteMessageCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
