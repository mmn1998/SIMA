using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftValorStatuses;

public class DeleteDraftValorStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}