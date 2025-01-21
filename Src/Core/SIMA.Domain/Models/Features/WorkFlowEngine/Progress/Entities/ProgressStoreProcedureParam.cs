using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.DataTypes.Entities;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;

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
        IsRequired = arg.IsRequired;
        DisplayName = arg.DisplayName;
        IsSystemParam = arg.IsSystemParam;
        SystemParamName = arg.SystemParamName;
        JsonFormat = arg.JsonFormat;
        BoundFormat = arg.BoundFormat;
        ApiNameForDataBounding = arg.ApiNameForDataBounding;
        StoredProcedureForDataBounding = arg.StoredProcedureForDataBounding;
        if (arg.UiInputElementId.HasValue) UiInputElementId = new(arg.UiInputElementId.Value);
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
    public string DisplayName { get; private set; }
    public string? JsonFormat { get; private set; }
    public string? BoundFormat { get; private set; }
    public string? ApiNameForDataBounding { get; private set; }
    public string? StoredProcedureForDataBounding { get; private set; }
    public UIInputElementId? UiInputElementId { get; private set; }
    public void Activate()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public void Modify(ProgressStoreProcedureParam arg)
    {
        Name = arg.Name;
        DataTypeId = arg.DataTypeId;
        IsRequired = arg.IsRequired;
        DisplayName = arg.DisplayName;
        IsSystemParam = arg.IsSystemParam;
        SystemParamName = arg.SystemParamName;
        JsonFormat = arg.JsonFormat;
        BoundFormat = arg.BoundFormat;
        ApiNameForDataBounding = arg.ApiNameForDataBounding;
        StoredProcedureForDataBounding = arg.StoredProcedureForDataBounding;
        UiInputElementId = arg.UiInputElementId;
        ApiMethodActionId = arg.ApiMethodActionId;
    }

    public virtual UIInputElement? UiInputElement { get; private set; }
    public string? IsRequired { get; private set; }
    public string? IsSystemParam { get; private set; }
    public string? ComboIsCascade { get; private set; }
    public string? SystemParamName { get; private set; }
    public string? TextBoundName { get; private set; }
    public string? ValueBoundName { get; private set; }
    public string? FixedValue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public ApiMethodActionId? ApiMethodActionId { get; private set; }
    public ApiMethodAction? ApiMethodAction{ get; private set; }


}
