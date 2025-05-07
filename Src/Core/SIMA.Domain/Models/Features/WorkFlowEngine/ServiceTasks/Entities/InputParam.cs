using SIMA.Domain.Models.Features.Auths.DataTypes.Entities;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities
{
    public class InputParam : Entity
    {
        private InputParam()
        {
        }
        private InputParam(CreateInputParamArg arg)
        {
            Id = new InputParamId(IdHelper.GenerateUniqueId());
            InputName = arg.InputName;
            DataTypeId = new(arg.DataTypeId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static InputParam Create(CreateInputParamArg arg)
        {
            return new InputParam(arg);
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

        public InputParamId Id { get; private set; }
        public string InputName { get; private set; }
        public DataTypeId DataTypeId { get; private set; }
        public virtual DataType DataType { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public long? ModifiedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }

        private List<ServiceInputParam> _serviceInputParams = new();
        public ICollection<ServiceInputParam> ServiceInputParams => _serviceInputParams;


    }
}

