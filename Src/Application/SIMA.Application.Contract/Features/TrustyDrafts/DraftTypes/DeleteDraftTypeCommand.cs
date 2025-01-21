using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftTypes;

public class DeleteDraftTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}