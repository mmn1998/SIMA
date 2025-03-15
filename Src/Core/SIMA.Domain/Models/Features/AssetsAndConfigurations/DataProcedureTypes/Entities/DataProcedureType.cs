using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Entities;

public class DataProcedureType : Entity, IAggregateRoot
{
    private DataProcedureType() { }
    private DataProcedureType(CreateDataProcedureTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DataProcedureType> Create(CreateDataProcedureTypeArg arg, IDataProcedureTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new DataProcedureType(arg);
    }
    public async Task Modify(ModifyDataProcedureTypeArg arg, IDataProcedureTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateDataProcedureTypeArg arg, IDataProcedureTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyDataProcedureTypeArg arg, IDataProcedureTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public DataProcedureTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    
    private List<DataProcedure> _dataProcedure = new();
    public ICollection<DataProcedure> DataProcedure => _dataProcedure;
    
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
