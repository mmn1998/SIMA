using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

public class DataProcedureInputParam
{
    
    private DataProcedureInputParam() { }
    private DataProcedureInputParam(CreateDataProcedureInputParamArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Description = arg.Description;
        DataProcedureId = new(arg.DataProcedureId);
        DataType = arg.DataType;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        IsMandatory = arg.IsMandatory;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static DataProcedureInputParam Create(CreateDataProcedureInputParamArg arg)
    {
        return new DataProcedureInputParam(arg);
    }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}