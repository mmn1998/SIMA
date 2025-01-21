using SIMA.Domain.Models.Features.BranchManagement.Customers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;

public class Customer : Entity
{
    private Customer()
    {

    }
    private Customer(CreateCustomerArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        CustomerNumber = arg.CustomerNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async  Task<Customer> Create(CreateCustomerArg arg , ICustomerDomainService service)
    {
        await CreateGuards(arg, service);
        return new Customer(arg);
    }
    public async Task Modify(ModifyCustomerArg arg , ICustomerDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        CustomerNumber = arg.CustomerNumber;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    #region Guards
    private static async Task CreateGuards(CreateCustomerArg arg, ICustomerDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.CustomerNumber.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.CustomerNumber.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (await service.IsCodeUnique(arg.CustomerNumber , 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyCustomerArg arg, ICustomerDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.CustomerNumber.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.CustomerNumber.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (await service.IsCodeUnique(arg.CustomerNumber,  Id.Value)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion

    public CustomerId Id { get; private set; }
    public string? Name { get; private set; }
    public string CustomerNumber { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<FinancialSupplier> _financialSuppliers = new();
    public ICollection<FinancialSupplier> FinancialSuppliers => _financialSuppliers;

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;

    private List<InquiryRequest> _inquiryRequests = new();
    public ICollection<InquiryRequest> InquiryRequests => _inquiryRequests;
    public void Delete(long userId)
    {
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ModifiedBy = userId;
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
