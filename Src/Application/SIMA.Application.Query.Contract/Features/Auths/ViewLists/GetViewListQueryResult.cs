namespace SIMA.Application.Query.Contract.Features.Auths.ViewLists
{
    public class GetViewListQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ActiveStatusId { get; set; }
        public string ActiveStatus { get; set; }
    }
}
