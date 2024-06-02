using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;

public class ServiceCustomerType : Entity
{
    private ServiceCustomerType()
    {
    }
    private ServiceCustomerType(CreateServiceCustomerTypeArg arg)
    {
        Id = new ServiceCustomerTypeId(arg.Id);
        if (arg.ParentId.HasValue) ParentId = new ServiceCustomerTypeId(arg.ParentId.Value);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceCustomerType> Create(CreateServiceCustomerTypeArg arg, IServiceCustomerTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ServiceCustomerType(arg);
    }
    public async Task Modify(ModifyServiceCustomerTypeArg arg, IServiceCustomerTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.ParentId.HasValue) ParentId = new ServiceCustomerTypeId(arg.ParentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateServiceCustomerTypeArg arg, IServiceCustomerTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyServiceCustomerTypeArg arg, IServiceCustomerTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ServiceCustomerTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public virtual ServiceCustomerType? Parent { get; private set; }
    public ServiceCustomerTypeId? ParentId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceCustomer> _serviceCustomers = new();
    public ICollection<ServiceCustomer> ServiceCustomers => _serviceCustomers;
}
