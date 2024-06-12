using SIMA.Domain.Models.Features.Auths.DataTypes.Entities;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class StepOutputParam : Entity
{
    private StepOutputParam() { }
    private StepOutputParam(CreateStepOutputParamArg arg)
    {
        Id = new(arg.Id);
        StepId = new(arg.StepId);
        DataTypeId = new(arg.DataTypeId);
        IsRequired = arg.IsRequired;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static StepOutputParam Create(CreateStepOutputParamArg arg)
    {
        return new StepOutputParam(arg);
    }
    public StepOutputParamId Id { get; private set; }
    public StepId StepId { get; private set; }
    public virtual Step Step { get; private set; }
    public DataTypeId DataTypeId { get; private set; }
    public virtual DataType DataType { get; private set; }
    public string? IsRequired { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public long? ModifiedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
}
