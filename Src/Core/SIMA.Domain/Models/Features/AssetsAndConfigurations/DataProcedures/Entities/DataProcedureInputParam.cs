using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

public class DataProcedureInputParam
{
    
    private DataProcedureInputParam() { }
    private DataProcedureInputParam(CreateDataProcedureInputParamArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Description = arg.Description;
        DataType = arg.DataType;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        IsMandatory = arg.IsMandatory;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DataProcedureInputParam> Create(CreateDataProcedureInputParamArg arg, IDataProcedureInputParamDomainService service)
    {
        await CreateGuards(arg, service);
        return new DataProcedureInputParam(arg);
    }
    public async Task Modify(ModifyDataProcedureInputParamArg arg, IDataProcedureInputParamDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Description = arg.Description;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        IsMandatory = arg.IsMandatory;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateDataProcedureInputParamArg arg, IDataProcedureInputParamDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private async Task ModifyGuards(ModifyDataProcedureInputParamArg arg, IDataProcedureInputParamDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public DataProcedureInputParamId Id { get; private set; }
    public DataProcedureId DataProcedureId { get; private set; }
    public virtual DataProcedure DataProcedure { get; private set; }
    public string Name { get; private set; }
    public string DataType { get; private set; }
    public DataProcedureInputParamId? ParentId { get; set; }
    public virtual DataProcedureInputParam? Parent { get; private set; }
    public string? IsMandatory { get; private set; }
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