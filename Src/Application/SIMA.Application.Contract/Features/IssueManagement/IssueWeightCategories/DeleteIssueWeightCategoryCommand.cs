using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.IssueWeightCategories;

public class DeleteIssueWeightCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
