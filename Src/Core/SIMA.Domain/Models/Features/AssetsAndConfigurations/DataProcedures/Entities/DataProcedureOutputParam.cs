using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

public class DataProcedureOutputParam
{
    private DataProcedureOutputParam() { }
    private DataProcedureOutputParam(CreateDataProcedureOutputParamArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Description = arg.Description;
        DataType = arg.DataType;
        DataProcedureId = new(arg.DataProcedureId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static DataProcedureOutputParam Create(CreateDataProcedureOutputParamArg arg)
    {
        return new DataProcedureOutputParam(arg);
    }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}