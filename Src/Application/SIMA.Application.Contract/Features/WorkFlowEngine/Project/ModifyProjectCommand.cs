using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.Project
{
    public class ModifyProjectCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long? DomainId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
