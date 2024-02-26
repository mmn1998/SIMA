using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.IssueWeightCategories;

public class ModifyIssueWeightCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
}
