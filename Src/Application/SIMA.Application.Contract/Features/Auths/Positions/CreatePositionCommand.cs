using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Positions;

public class CreatePositionCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? BranchId { get; set; }
    public int PersonLimitation { get; set; }
    public long PositionLevelId { get; set; }
    public long PositionTypeId { get; set; }
    public long DepartmentId { get; set; }

}
