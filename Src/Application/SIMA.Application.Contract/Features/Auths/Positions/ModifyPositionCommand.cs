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
        public long ActiveStatusId { get; set; }

    }
}
