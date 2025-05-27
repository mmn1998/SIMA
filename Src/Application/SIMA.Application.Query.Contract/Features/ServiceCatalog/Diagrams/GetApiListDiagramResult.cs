namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetApiListDiagramResult
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? IsInternalApi { get; set; }
    public string? Prerequisites { get; set; }
    public string? BaseUrl { get; set; }
    public string? ApiAddress { get; set; }
    public string? IPAddress { get; set; }
    public string? ProtNumber { get; set; }
    public long? RateLimitingMin { get; set; }
    public long? RateLimitingMax { get; set; }
    public ApiType? ApiType { get; set; }
    public NetworkProtocol? NetworkProtocolId { get; set; }
    public ApiMethodAction? ApiMethodAction { get; set; }
    public ApiAuthenticationMethod? ApiAuthenticationMethod { get; set; }
    public string? AuthenticationWorkflow { get; set; }
    public OwnerDepartment? OwnerDepartment { get; set; }
    public OwnerResponsible? OwnerResponsible { get; set; }
    public string? RulesAndConditions { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedAt { get; set; }


}

public class GetApiListResultWrapper
{
    public List<GetApiListDiagramResult>? Data { get; set; }
}


public class OwnerResponsible
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class OwnerDepartment
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class ApiAuthenticationMethod
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class ApiMethodAction
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class NetworkProtocol
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class ApiType
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}