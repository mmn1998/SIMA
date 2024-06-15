using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.Suppliers;

public class CreateSupplierCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? FaxNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public long SupplierRankId { get; set; }
    public string? IsInBlackList { get; set; }
}