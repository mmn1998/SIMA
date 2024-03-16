using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Exceptions;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;

public class PaymentType : Entity
{
    private PaymentType()
    {

    }
    private PaymentType(CreatePaymentTypeArg arg)
    {
        Id = new PaymentTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<PaymentType> Create(CreatePaymentTypeArg arg, IPaymentTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new PaymentType(arg);
    }
    public async Task Modify(ModifyPaymentTypeArg arg, IPaymentTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public PaymentTypeId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long? ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private static async Task CreateGuards(CreatePaymentTypeArg arg, IPaymentTypeDomainService paymentTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (arg.Name.Length >= 200)
        {
            throw PaymentTypeExceptions.LengthPaymentTypeNameException;
        }
        if (await paymentTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
        {
            throw SimaResultException.UniqueCodeError;
        }
    }
    private async Task ModifyGuards(ModifyPaymentTypeArg arg, IPaymentTypeDomainService paymentTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (arg.Name.Length >= 200)
        {
            throw PaymentTypeExceptions.LengthPaymentTypeNameException;
        }
        if (await paymentTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
        {
            throw SimaResultException.UniqueCodeError;
        }
    }

}
