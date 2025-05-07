using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities
{
    public class ServiceTask : Entity
    {
        private ServiceTask()
        {
        }
        private ServiceTask(CreateServiceTaskArg arg)
        {
            Id = new ServiceTaskId(IdHelper.GenerateUniqueId());
            Address = arg.Address;
            ApiMethodActionId = new(arg.APIMethodActionId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static ServiceTask Create(CreateServiceTaskArg arg)
        {
            return new ServiceTask(arg);
        }

        public async Task Modify(ModifyServiceTaskArg arg)
        {
            Address = arg.Address;
            ApiMethodActionId = new(arg.APIMethodActionId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }

        public async Task ChangeStatus(ActiveStatusEnum status)
        {
            ActiveStatusId = (long)status;
        }

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public ServiceTaskId Id { get; private set; }
        public string Address { get; private set; }
        public ApiMethodActionId ApiMethodActionId { get; private set; }
        public virtual ApiMethodAction ApiMethodAction { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public long? ModifiedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }

        private List<ServiceInputParam> _serviceInputParams = new();
        public ICollection<ServiceInputParam> ServiceInputParams => _serviceInputParams;

        private List<StepServiceTask> _stepServiceTasks = new();
        public ICollection<StepServiceTask> StepServiceTasks => _stepServiceTasks;


    }
}
