namespace SIMA.Application.Query.Contract.Features.Auths.Forms
{
    public class GetFormQueryResult
    {
        public long Id { get; set; }
        public long DomainId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string IsSystemForm { get; set; }
        public string JsonContent { get; set; }
        public long ActiveStatusId { get; set; }
        public string ActiveStatusName { get; set; }
    }
}
