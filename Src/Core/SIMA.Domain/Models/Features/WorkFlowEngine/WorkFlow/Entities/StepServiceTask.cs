using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities
{
    public class StepServiceTask : Entity
    {
        private StepServiceTask()
        {
        }
        private StepServiceTask(CreateStepServiceTaskArg arg)
        {
            Id = new StepServiceTaskId(IdHelper.GenerateUniqueId());
            ServiceTaskId = new ServiceTaskId(arg.ServiceTaskId);
            StepId = new StepId(arg.StepId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static StepServiceTask Create(CreateStepServiceTaskArg arg)
        {
            return new StepServiceTask(arg);
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

        public StepServiceTaskId Id { get; private set; }
        public ServiceTaskId ServiceTaskId { get; private set; }
        public virtual ServiceTask ServiceTask { get; private set; }
        public StepId StepId { get; private set; }
        public virtual Step Step { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public long? ModifiedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
    }
}
