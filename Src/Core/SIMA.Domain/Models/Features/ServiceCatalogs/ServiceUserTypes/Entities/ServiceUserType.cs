using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;

public class ServiceUserType : Entity
{
    private ServiceUserType()
    {

    }
    private ServiceUserType(CreateServiceUserTypeArg arg)
    {
        Id = new ServiceUserTypeId(arg.Id);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceUserType> Create(CreateServiceUserTypeArg arg, IServiceUserTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ServiceUserType(arg);
    }
    public async Task Modify(ModifyServiceUserTypeArg arg, IServiceUserTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateServiceUserTypeArg arg, IServiceUserTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyServiceUserTypeArg arg, IServiceUserTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ServiceUserTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public virtual ServiceUserType? Parent { get; private set; }
    public ServiceUserTypeId? ParentId { get; private set; }
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
    private List<ServiceUser> _serviceUsers = new();
    public ICollection<ServiceUser> ServiceUsers => _serviceUsers;
}
