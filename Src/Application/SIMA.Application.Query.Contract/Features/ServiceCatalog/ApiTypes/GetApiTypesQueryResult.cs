namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiTypes
{
    public class GetApiTypesQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ActiveStatus { get; set; }
    }
}
