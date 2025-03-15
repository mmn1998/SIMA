using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

public class DataProcedure : Entity, IAggregateRoot
{    
    private DataProcedure() { }
    private DataProcedure(CreateDataProcedureArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        Description = arg.Description;
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        IsInternalApi = arg.IsInternalApi;
        DatabaseId = new(arg.DataBaseId);
        DataProcedureTypeId = new(arg.DataProcedureTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DataProcedure> Create(CreateDataProcedureArg arg, IDataProcedureDomainService service)
    {
        await CreateGuards(arg, service);
        return new DataProcedure(arg);
    }
    public async Task Modify(ModifyDataProcedureArg arg, IDataProcedureDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Description = arg.Description;
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        IsInternalApi = arg.IsInternalApi;
        DatabaseId = new(arg.DataBaseId);
        DataProcedureTypeId = new(arg.DataProcedureTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }    
    #region Guards
    private static async Task CreateGuards(CreateDataProcedureArg arg, IDataProcedureDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyDataProcedureArg arg, IDataProcedureDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    
    public DataProcedureId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? VersionNumber { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public string? Description { get; private set; }
    public string IsInternalApi { get; private set; }
    public ConfigurationItemId DatabaseId { get; private set; }
    public virtual ConfigurationItem Database { get; private set; }
    public DataProcedureTypeId DataProcedureTypeId { get; private set; }
    public virtual DataProcedureType DataProcedureType { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    
    private List<DataProcedureOutputParam> _dataProcedureOutputParam = new();
    public ICollection<DataProcedureOutputParam> DataProcedureOutputParam  => _dataProcedureOutputParam;
    
    private List<DataProcedureInputParam> _dataProcedureInputParam = new();
    public ICollection<DataProcedureInputParam> DataProcedureInputParam => _dataProcedureInputParam;
    
    private List<ConfigurationItemDataProcedure> _configurationItemDataProcedure = new();
    public ICollection<ConfigurationItemDataProcedure> ConfigurationItemDataProcedures => _configurationItemDataProcedure;
    
    private List<DataProcedureDocument> _dataProcedureDocuments = new();
    public ICollection<DataProcedureDocument> DataProcedureDocuments => _dataProcedureDocuments;
    
    private List<DataProcedureSupportTeam> _dataProcedureSupportTeams = new();
    public ICollection<DataProcedureSupportTeam> DataProcedureSupportTeams => _dataProcedureSupportTeams;
    
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    
}