using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.Resources;

public class ModifyResourceCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long AccountTypeId { get; set; }
    public long BrokerId { get; set; }
    public long CurrencyTypeId { get; set; }
    public string? Title { get; set; }
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvaliableBalance { get; set; }
    public decimal BlockedBalance { get; set; }
    public string? Code { get; set; }
}