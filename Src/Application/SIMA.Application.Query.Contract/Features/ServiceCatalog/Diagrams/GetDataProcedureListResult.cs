namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetDataProcedureListResult
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? IsInternalApi { get; set; }
    public string? VersionNumber { get; set; }
    public string? ReleaseDate { get; set; }
    public Database? Database { get; set; }
    public DataProcedureType? DataProcedureType { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedAt { get; set; }
}

public class DataProcedureType
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class Database
{
    public long? Id { get; set; }
    public string? VersionNumber { get; set; }
    public string? Title { get; set; }
}