namespace SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField
{
    public class GetViewFieldQueryResult
    {
        public long Id { get; set; }
        public long ViewId { get; set; }
        public string ViewName { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ActiveStatusId { get; set; }
        public string ActiveStatus { get; set; }
    }
}
