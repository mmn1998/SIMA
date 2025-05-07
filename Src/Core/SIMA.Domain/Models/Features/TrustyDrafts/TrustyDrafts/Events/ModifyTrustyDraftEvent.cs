using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Events;

public sealed record ModifyTrustyDraftEvent(long issueId, MainAggregateEnums MainAggregateType, string Name, long SourceId,
    long IssuePriority, DateTime? DueDate, long? RequesterId) : IDomainEvent;

