using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities
{
    public class ServiceInputParam : Entity
    {
        private ServiceInputParam()
        {
        }
        private ServiceInputParam(CreateServiceInputParamArg arg)
        {
            Id = new ServiceInputParamId(IdHelper.GenerateUniqueId());
            ServiceTaskId = new ServiceTaskId(arg.ServiceTaskId);
            InputParamId = new InputParamId(arg.InputParamId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static ServiceInputParam Create(CreateServiceInputParamArg arg)
        {
            return new ServiceInputParam(arg);
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

        public ServiceInputParamId Id { get; private set; }
        public ServiceTaskId ServiceTaskId { get; private set; }
        public virtual ServiceTask ServiceTask { get; private set; }
        public  InputParamId InputParamId { get; private set; }
        public virtual InputParam InputParam { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public long? ModifiedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
    }
}
