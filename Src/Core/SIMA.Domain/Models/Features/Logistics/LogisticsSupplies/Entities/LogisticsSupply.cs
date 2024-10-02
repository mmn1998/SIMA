using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class LogisticsSupply :Entity
{
    private LogisticsSupply() { }
    private LogisticsSupply(CreateLogisticsSupplyArg arg)
    {
        Id = new LogisticsSupplyId(IdHelper.GenerateUniqueId());
        Description = arg.Description;
        Code = arg.Code;
        IssueId = new(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<LogisticsSupply> Create(CreateLogisticsSupplyArg arg)
    {
        return new LogisticsSupply(arg);
    }
    public LogisticsSupplyId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<TenderResult> _tenderResults = new();
    public ICollection<TenderResult> TenderResults => _tenderResults;

    private List<LogisticsSupplyGoods> _logisticsSupplyGoods = new();
    public ICollection<LogisticsSupplyGoods> LogisticsSupplyGoods => _logisticsSupplyGoods;

    private List<Ordering> _ordering = new();
    public ICollection<Ordering> Orderings => _ordering;

    private List<CandidatedSupplier> _candidatedSuppliers = new();
    public ICollection<CandidatedSupplier> CandidatedSuppliers => _candidatedSuppliers;
}
