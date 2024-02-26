using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Departments;

public class CreateDepartmentCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? ParentId { get; set; }
    public long? CompanyId { get; set; }
}
