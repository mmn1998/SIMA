using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Entities;

public class ServiceCategory : Entity
{
    private ServiceCategory()
    {
    }
    private ServiceCategory(CreateServiceCategoryArg arg)
    {
        Id = new ServiceCategoryId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ParentId = arg.ParentId.HasValue ? new(arg.ParentId.Value) : null;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceCategory> Create(CreateServiceCategoryArg arg, IServiceCategoryDomainService service)
    {
        await CreateGuards(arg, service);
        return new ServiceCategory(arg);
    }
    public async Task Modify(ModifyServiceCategoryArg arg, IServiceCategoryDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ParentId = arg.ParentId.HasValue ? new(arg.ParentId.Value) : null;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateServiceCategoryArg arg, IServiceCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyServiceCategoryArg arg, IServiceCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ServiceCategoryId Id { get; private set; }
    public ServiceCategoryId? ParentId { get; private set; }
    public virtual ServiceCategory? Parent { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
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
