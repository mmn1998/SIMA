using SIMA.Domain.Models.Features.Auths.DataTypes.Args;
using SIMA.Domain.Models.Features.Auths.DataTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.Auths.DataTypes.Entities;

public class DataType : Entity
{
    private DataType()
    {

    }
    private DataType(CreateDataTypeArg arg)
    {
        Id = new DataTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
    }
    public static async Task<DataType> Create(CreateDataTypeArg arg, IDataTypeService service)
    {
        await CreateGuards(arg, service);
        return new DataType(arg);
    }

    public async Task Modify(ModifyDataTypeArg arg, IDataTypeService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateDataTypeArg arg, IDataTypeService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyDataTypeArg arg, IDataTypeService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        arg.Name.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public DataTypeId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public string IsList { get; private set; }

    public string IsMultiSelect { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<ProgressStoreProcedureParam> _progressStoreProcedureParams = new();
    public ICollection<ProgressStoreProcedureParam> ProgressStoreProcedureParams => _progressStoreProcedureParams;
    private List<StepOutputParam> _stepOutputParams = new();
    public ICollection<StepOutputParam> StepOutputParams => _stepOutputParams;

    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
