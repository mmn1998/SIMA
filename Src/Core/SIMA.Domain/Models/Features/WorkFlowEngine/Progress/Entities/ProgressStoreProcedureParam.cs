using SIMA.Domain.Models.Features.Auths.DataTypes.Entities;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities
{
    public class ProgressStoreProcedureParam : Entity
    {
        private ProgressStoreProcedureParam()
        {
        }
        private ProgressStoreProcedureParam(ProgressStoreProcedureParamArg arg)
        {
            Id = new(arg.Id);
            ProgressStoreProcedureId = new(arg.ProgressStoreProcedureId);
            Name = arg.Name;
            DataTypeId = new(arg.DataTypeId);
            IsRequired = arg.IsRequierd;
        }
        public static ProgressStoreProcedureParam Create(ProgressStoreProcedureParamArg arg)
        {
            return new ProgressStoreProcedureParam(arg);
        }
        public ProgressStoreProcedureParamId Id { get; private set; }
        public ProgressStoreProcedureId ProgressStoreProcedureId { get; private set; }
        public virtual ProgressStoreProcedure ProgressStoreProcedure { get; private set; }
        public DataTypeId DataTypeId { get; private set; }
        public virtual DataType DataType { get; private set; }
        public string Name { get; private set; }
        public string? IsRequired { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

    }
}
