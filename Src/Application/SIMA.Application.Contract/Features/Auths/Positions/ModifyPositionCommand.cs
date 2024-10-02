using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;


namespace SIMA.Application.Contract.Features.Auths.Positions
{
    public class ModifyPositionCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? BranchId { get; set; }
        public int PersonLimitation { get; set; }
        public long PositionLevelId { get; set; }
        public long PositionTypeId { get; set; }
        public long ActiveStatusId { get; set; }

    }
}
