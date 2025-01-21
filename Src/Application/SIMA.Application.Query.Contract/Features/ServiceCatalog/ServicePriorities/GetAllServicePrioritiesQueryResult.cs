namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities
{
    public class GetAllServicePrioritiesQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Ordering { get; set; }
        public long ActiveStatsId { get; set; }
        public string ActiveStatusName { get; set; }
    }
}
