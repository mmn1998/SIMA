using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftStatuses;

public class DeleteDraftStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}