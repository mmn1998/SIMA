using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Args;
using SIMA.Domain.Models.Features.Auths.UserTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.UserTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.UserTypes.Entities;

public class UserType : Entity, IAggregateRoot
{
    private UserType()
    {

    }
    private UserType(CreateUserTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<UserType> Create(CreateUserTypeArg arg, IUserTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new UserType(arg);
    }
    public async Task Modify(ModifyUserTypeArg arg, IUserTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateUserTypeArg arg, IUserTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyUserTypeArg arg, IUserTypeDomainService service)
    {

    }
    #endregion
    public UserTypeId Id { get; private set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

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
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
}
