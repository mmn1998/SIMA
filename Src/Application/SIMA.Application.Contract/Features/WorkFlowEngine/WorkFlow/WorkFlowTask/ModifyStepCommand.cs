using Sima.Framework.Core.Mediator;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.Steps;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask
{
    public class ModifyStepCommand : ICommand<Result<long>>
    {

        public long Id { get; set; }
        public List<AddStepApprovalOption>? ApprovalOptions { get; set; }
        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        public string? DisplayName { get; set; }
        public long? FormId { get; set; }
        public string HasDocument { get; set; }
        public string? IsAssigneeForced { get; set; }
        public string? UIPropertyBoxTitle { get; set; }
        public List<RequiredDocument>? RequiredDocuments { get; set; }
    }

    public class RequiredDocument
    {
        public int Count { get; set; }
        public long DocumentTypeId { get; set; }
    }
}
