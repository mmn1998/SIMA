using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;

public class DeleteTrustyDraftCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
