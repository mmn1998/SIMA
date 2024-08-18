namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class GetApiAuthenticationMethodsQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ActiveStatus { get; set; }
    }
}
