using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
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
    #region AddMethods
    public void AddDataProcedureDocuments(List<CreateDataProcedureDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = DataProcedureDocument.Create(arg);
            _dataProcedureDocuments.Add(entity);
        }
    }
    public void AddDataProcedureInputParams(List<CreateDataProcedureInputParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = Entities.DataProcedureInputParam.Create(arg);
            _dataProcedureInputParam.Add(entity);
        }
    }
    public void AddConfigurationItemDataProcedures(List<CreateConfigurationItemDataProcedureArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemDataProcedure.Create(arg);
            _configurationItemDataProcedure.Add(entity);
        }
    }
    public void AddDataProcedureOutputParams(List<CreateDataProcedureOutputParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = Entities.DataProcedureOutputParam.Create(arg);
            _dataProcedureOutputParam.Add(entity);
        }
    }
    public void AddDataProcedureSupportTeams(List<CreateDataProcedureSupportTeamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = DataProcedureSupportTeam.Create(arg);
            _dataProcedureSupportTeams.Add(entity);
        }
    }
    #endregion
    #region ModifyMethods
    public void ModifyDataProcedureDocuments(List<CreateDataProcedureDocumentArg> args)
    {
        var activeEntities = _dataProcedureDocuments.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _dataProcedureDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = DataProcedureDocument.Create(arg);
                _dataProcedureDocuments.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemDataProcedures(List<CreateConfigurationItemDataProcedureArg> args)
    {
        var activeEntities = _configurationItemDataProcedure.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ConfigurationItemId == x.ConfigurationItemId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ConfigurationItemId.Value == x.ConfigurationItemId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemDataProcedure.FirstOrDefault(x => x.ConfigurationItemId.Value == arg.ConfigurationItemId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemDataProcedure.Create(arg);
                _configurationItemDataProcedure.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyDataProcedureSupportTeams(List<CreateDataProcedureSupportTeamArg> args)
    {
        var activeEntities = _dataProcedureSupportTeams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _dataProcedureSupportTeams.FirstOrDefault(x => x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = DataProcedureSupportTeam.Create(arg);
                _dataProcedureSupportTeams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyDataProcedureInputParams(List<CreateDataProcedureInputParamArg> args)
    {
        var activeEntities = _dataProcedureInputParam.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DataType == x.DataType && c.Name == x.Name));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DataType == x.DataType && c.Name == x.Name));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _dataProcedureInputParam.FirstOrDefault(x => x.DataType == arg.DataType && x.Name == arg.Name && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = Entities.DataProcedureInputParam.Create(arg);
                _dataProcedureInputParam.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyDataProcedureOutputParams(List<CreateDataProcedureOutputParamArg> args)
    {
        var activeEntities = _dataProcedureOutputParam.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DataType == x.DataType && c.Name == x.Name));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DataType == x.DataType && c.Name == x.Name));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _dataProcedureOutputParam.FirstOrDefault(x => x.DataType == arg.DataType && x.Name == arg.Name && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = Entities.DataProcedureOutputParam.Create(arg);
                _dataProcedureOutputParam.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteDataProcedureSupportTeams(long userId)
    {
        foreach (var entity in _dataProcedureSupportTeams)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteDataProcedureInputParams(long userId)
    {
        foreach (var entity in _dataProcedureInputParam)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteDataProcedureOutputParams(long userId)
    {
        foreach (var entity in _dataProcedureOutputParam)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteDataProcedureDocuments(long userId)
    {
        foreach (var entity in _dataProcedureDocuments)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemDataProcedures(long userId)
    {
        foreach (var entity in _configurationItemDataProcedure)
        {
            entity.Delete(userId);
        }
    }
    #endregion
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
        #region DeleteRelatedEntities
        DeleteConfigurationItemDataProcedures(userId);
        DeleteDataProcedureDocuments(userId);
        DeleteDataProcedureInputParams(userId);
        DeleteDataProcedureOutputParams(userId);
        DeleteDataProcedureSupportTeams(userId);
        #endregion
    }

}