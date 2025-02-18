using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;

public class Product : Entity, IAggregateRoot
{
    private Product()
    {
    }

    private Product(CreateProductArg arg)
    {
        Id = new ProductId(arg.Id);
        ServiceStatusId = arg.ServiceStatusId.HasValue ? new ServiceStatusId(arg.ServiceStatusId.Value) : null;
        Name = arg.Name;
        Code = arg.Code;
        Description = arg.Description;
        Scope = arg.Scope;
        ProviderCompanyId = arg.ProviderCompanyId.HasValue ? new CompanyId(arg.ProviderCompanyId.Value) : null;
        InServiceDate = arg.InServiceDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Product> Create(CreateProductArg arg, IProductDomainService service)
    {
        await CreateGuards(arg, service);
        return new Product(arg);
    }

    public async Task Modify(ModifyProductArg arg)
    {
        ServiceStatusId = arg.ServiceStatusId.HasValue ? new ServiceStatusId(arg.ServiceStatusId.Value) : null;
        Name = arg.Name;
        Description = arg.Description;
        Scope = arg.Scope;
        ProviderCompanyId = arg.ProviderCompanyId.HasValue ? new CompanyId(arg.ProviderCompanyId.Value) : null;
        InServiceDate = arg.InServiceDate;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateProductArg arg, IProductDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyProductArg arg, IProductDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public void CreateChannel(List<CreateProductChannelArg> args, long ProductId)
    {
        var previousProductChannels = _productChannels.Where(x => x.ProductId == new ProductId(ProductId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addChannel = args.Where(x => !previousProductChannels.Any(c => c.ChannelId.Value == x.ChannelId)).ToList();
        var deleteChannel = previousProductChannels.Where(x => !args.Any(c => c.ChannelId == x.ChannelId.Value)).ToList();


        foreach (var item in addChannel)
        {
            var entity = _productChannels.Where(x => (x.ChannelId == new ChannelId(item.ChannelId) && x.ProductId == new ProductId(ProductId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = ProductChannel.Create(item);
                _productChannels.Add(entity);
            }
        }

        foreach (var document in deleteChannel)
        {
            document.Delete(args[0].CreatedBy);
        }
    }

    public void CreateResponsible(List<CreateProductResponsibleArg> args, long ProductId)
    {
        var previousProductResponsibles = _productResponsibles.Where(x => x.ProductId == new ProductId(ProductId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addResponsible = args.Where(x => !previousProductResponsibles.Any(c => c.ResponsilbeId.Value == x.ResponsibleId && c.ResponsibleTypeId.Value == x.ResponsibleTypeId)).ToList();
        var deleteResponsible = previousProductResponsibles.Where(x => !args.Any(c => c.ResponsibleId == x.ResponsilbeId.Value && c.ResponsibleTypeId == x.ResponsibleTypeId.Value)).ToList();


        foreach (var item in addResponsible)
        {
            var entity = _productResponsibles.Where(x => (x.ResponsilbeId == new StaffId(item.ResponsibleId) && x.ResponsibleTypeId == new Auths.ResponsibleTypes.ValueObjects.ResponsibleTypeId(item.ResponsibleTypeId) && x.ProductId == new ProductId(ProductId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = ProductResponsible.Create(item);
                _productResponsibles.Add(entity);
            }
        }

        foreach (var document in deleteResponsible)
        {
            document.Delete(deleteResponsible[0].CreatedBy);
        }
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? Scope { get; private set; }
    public string? Description { get; private set; }
    public ServiceStatusId? ServiceStatusId { get; private set; }
    public virtual ServiceStatus ServiceStatus { get; private set; }
    public virtual Company ProviderCompany { get; private set; }
    public CompanyId? ProviderCompanyId { get; private set; }
    public DateOnly? InServiceDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<ProductChannel> _productChannels = new();
    public ICollection<ProductChannel> ProductChannels => _productChannels;

    private List<ProductResponsible> _productResponsibles = new();
    public ICollection<ProductResponsible> ProductResponsibles => _productResponsibles;
}
