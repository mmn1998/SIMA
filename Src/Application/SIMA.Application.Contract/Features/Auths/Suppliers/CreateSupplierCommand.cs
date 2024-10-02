using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Suppliers;

public class CreateSupplierCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long SupplierRankId { get; set; }
    public string? IsInBlackList { get; set; }
    public int SuccessOrderCountinTheYear { get; set; }
    public List<CreateSupplierAccountListCommand> AccountList { get; set; }
    public List<CreateSupplierAddressBookCommand> AddressBooks { get; set; }
    public List<CreateSupplierPhoneBookCommand> PhoneBooks { get; set; }

}