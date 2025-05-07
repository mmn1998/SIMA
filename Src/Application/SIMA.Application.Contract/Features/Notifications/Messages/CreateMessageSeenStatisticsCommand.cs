using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Notifications.Messages
{
    public class CreateMessageSeenStatisticsCommand : ICommand<Result<long>>
    {
        public long MessageId { get; set; }
    }
}
