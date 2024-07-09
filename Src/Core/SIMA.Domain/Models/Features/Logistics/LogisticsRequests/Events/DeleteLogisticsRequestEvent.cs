using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Domain;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events
{
    public sealed record DeleteLogisticsRequestEvent(long issueId) : IDomainEvent;
}
