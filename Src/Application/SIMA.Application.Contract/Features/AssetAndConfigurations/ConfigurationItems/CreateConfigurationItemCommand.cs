using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class CreateConfigurationItemCommand : ICommand<Result<long>>
{
    public long OwnerId { get; set; }
    public long ConfigurationItemTypeId { get; set; }
    public long ConfigurationItemStatusId { get; set; }
    public long LicenseTypeId { get; set; }
    public long? SupplierId { get; set; }
    public long? BusinessCriticalityId { get; set; }
    public long? TimeMeasurementId { get; set; }
    public string? Title { get; set; }
    public string? VersionNumber { get; set; }
    public long CompanyBuildingLocationId { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? UpdateSubject { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ReleaseDate { get; set; }
    public string? LastUpdateDate { get; set; }
    public float? Uptime { get; set; }
    public float? Mttr { get; set; }
    public float? Mtbf { get; set; }
    public ICollection<CreateConfigurationItemCustomFieldValueCommand>? ConfigurationItemCustomFieldValueList { get; set; }
    public ICollection<CreateConfigurationItemCustomFieldOptionCommand>? ConfigurationItemCustomFieldOptionList { get; set; }
    public ICollection<CreateConfigurationItemSupportTeamCommand>? ConfigurationItemSupportTeamList { get; set; }
    public ICollection<CreateConfigurationItemAccessInfoCommand>? ConfigurationItemAccessInfoList { get; set; }
    public ICollection<CreateConfigurationItemBackupScheduleInfo>? ConfigurationItemBackupScheduleList { get; set; }
    public ICollection<CreateConfigurationItemApiCommand>? ConfigurationItemApiList { get; set; }
    public ICollection<CreateConfigurationItemDataProcedureCommand>? ConfigurationItemDataProcedureList { get; set; }
    public ICollection<CreateServiceConfigurationItemCommand>? ServiceConfigurationItemList { get; set; }
    public ICollection<CreateConfigurationItemDocumentCommand>? ConfigurationItemDocumentList { get; set; }
    public ICollection<CreateConfigurationItemAssetCommand>? ConfigurationItemAssetList { get; set; }
}