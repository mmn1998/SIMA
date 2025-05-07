using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.IssueWeightCategories;

public class CreateIssueWeightCategoryCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
}
