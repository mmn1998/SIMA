using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;

public class GetDataProcedureQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? VersionNumber { get; set; }
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? ReleaseDatePersian => ReleaseDate.ToPersianDateTime();
    public string? IsInternalApi { get; set; }
    public long? DataProcedureTypeId { get; set; }
    public string? DataProcedureTypeName { get; set; }
    public long? DatabaseId { get; set; }
    public string? DatabaseName { get; set; }
    public string? ActiveStatus { get; set; }
    public IEnumerable<GetDataProcedureDocumentQueryResult>? DocumentList { get; set; }
    public IEnumerable<GetDataProcedureSupportTeamQueryResult>? SupportTeamList { get; set; }
    public IEnumerable<GetDataProcedureInputParamQueryResult>? InputParamList { get; set; }
    public IEnumerable<GetDataProcedureOutputParamQueryResult>? OutputParamList { get; set; }
    public IEnumerable<GetConfigurationItemDataProcedureResult>? CobfigurationItemList { get; set; }

}
public class GetDataProcedureDocumentQueryResult
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public long DocumentTypeId { get; set; }
    public string? DocumentTypeName { get; set; }
    public string? DocumentFileName { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string? DocumentExtensionName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public string? AttachStepName { get; set; }
}
public class GetDataProcedureSupportTeamQueryResult
{
    public long StaffId { get; set; }
    public string? StaffFullName { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public long? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
}
public class GetDataProcedureInputParamQueryResult
{
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public string? IsMandatory { get; set; }
    public string? DataType { get; set; }
}
public class GetDataProcedureOutputParamQueryResult
{
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public string? DataType { get; set; }
}
public class GetConfigurationItemDataProcedureResult
{
    public long ConfigurationItemId { get; set; }
    public string? ConfigurationItemName { get; set; }
}