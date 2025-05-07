using SIMA.Application.Contract.Attributes;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Contract.Features.BranchManagement.Brokers;

public class CreateBrokerPhoneBookCommand
{
    [CustomePhoneNumber(PhoneTypeEnum.Phone)]
    public string? PhoneNumber { get; set; }
    public long PhoneTypeId { get; set; }
}
