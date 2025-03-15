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
}