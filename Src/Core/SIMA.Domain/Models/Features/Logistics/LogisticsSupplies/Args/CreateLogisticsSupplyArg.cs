namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args
{
    public class CreateLogisticsSupplyArg
    {
        public long IssueId { get;  set; }
        public string Code { get;  set; }
        public string Description { get;  set; }
        public long ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
       
    }
}
