using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftIssueTypes;

public class DeleteDraftIssueTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}