using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;

public class ServiceBoundle : Entity
{
    private ServiceBoundle() { }
    private ServiceBoundle(CreateServiceBoundleArg arg)
    {
        Id = new ServiceBoundleId(arg.Id);
        ServiceCategoryId = new(arg.ServiceCategoryId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceBoundle> Create(CreateServiceBoundleArg arg, IServiceBoundleDomainService service)
    {
        await CreateGuards(arg, service);
        return new ServiceBoundle(arg);
    }
    public async Task Modify(ModifyServiceBoundleArg arg, IServiceBoundleDomainService service)
    {
        await ModifyGuards(arg, service);
        ServiceCategoryId = new(arg.ServiceCategoryId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateServiceBoundleArg arg, IServiceBoundleDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyServiceBoundleArg arg, IServiceBoundleDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ServiceBoundleId Id { get; private set; }
    public ServiceCategoryId ServiceCategoryId { get; private set; }
    public virtual ServiceCategory ServiceCategory { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    private List<Service> _services = new();
    public ICollection<Service> Services => _services;
}
