using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;

public class Product : Entity
{
    private Product()
    {
    }

    private Product(CreateProductArg arg)
    {
        Id = new ProductId(arg.Id);
        ServiceStatusId = arg.ServiceStatusId.HasValue? new ServiceStatusId(arg.ServiceStatusId.Value):null;
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

    public static async Task<Product> Create(CreateProductArg arg)
    {
        return new Product(arg);
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
