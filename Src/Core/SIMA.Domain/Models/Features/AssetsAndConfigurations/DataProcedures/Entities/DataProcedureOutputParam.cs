using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

public class DataProcedureOutputParam
{
    private DataProcedureOutputParam() { }
    private DataProcedureOutputParam(CerateDataProcedureOutputParamArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Description = arg.Description;
        DataType = arg.DataType;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DataProcedureOutputParam> Create(CerateDataProcedureOutputParamArg arg, IDataProcedureOutputParamDomainService service)
    {
        await CreateGuards(arg, service);
        return new DataProcedureOutputParam(arg);
    }
    public async Task Modify(ModifyDataProcedureOutputParamArg arg, IDataProcedureOutputParamDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }

    #region Guards
    private static async Task CreateGuards(CerateDataProcedureOutputParamArg arg, IDataProcedureOutputParamDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private async Task ModifyGuards(ModifyDataProcedureOutputParamArg arg, IDataProcedureOutputParamDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion


    public DataProcedureOutputParamId Id { get; private set; }
    public DataProcedureId DataProcedureId { get; private set; }
    public virtual DataProcedure DataProcedure { get; private set; }
    public string Name { get; private set; }
    public string DataType { get; set; }
    public DataProcedureOutputParamId? ParentId { get; set; }
    public virtual DataProcedureOutputParam? Parent { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }


}