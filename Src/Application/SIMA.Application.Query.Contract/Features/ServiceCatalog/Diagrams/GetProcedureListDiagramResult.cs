using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetProcedureListDiagramResult
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? IsInternalApi { get; set; }
    public string? VersionNumber { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? ReleasePersianDate => ReleaseDate.ToPersianDateTime();
    public Database Database { get; set; }
    public DataProcedureType DataProcedureType { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedAt { get; set; }
    
}


public class GetProcedureListResultWrapper
{
    public List<GetProcedureListDiagramResult>? Data { get; set; }
}