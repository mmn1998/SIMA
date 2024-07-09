using Sima.Framework.Core.Mediator;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.Suppliers;

public class ModifySupplierCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    [CustomePhoneNumber(PhoneTypeEnum.Phone)]
    public string? PhoneNumber { get; set; }
    [CustomePhoneNumber(PhoneTypeEnum.Mobile)]
    public string? MobileNumber { get; set; }
    public string? FaxNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public long SupplierRankId { get; set; }
    public string? IsInBlackList { get; set; }
}