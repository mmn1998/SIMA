using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities;

public class UserDomainAccess
{
    private UserDomainAccess() { }
    private UserDomainAccess(CreateUserDomainArg arg)
    {
        Id = new UserDomainAccessId(IdHelper.GenerateUniqueId());
        DomainId = new DomainId(arg.DomainId.Value);
        UserId = new UserId(arg.UserId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<UserDomainAccess> Create(CreateUserDomainArg arg)
    {
        return new UserDomainAccess(arg);
    }
    public async Task Modify(ModifyUserDomainArg arg)
    {
        UserId = new UserId(arg.UserId.Value);
        DomainId = new DomainId(arg.DomainId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public UserDomainAccessId Id { get; private set; }

    public DomainId? DomainId { get; private set; }

    public UserId? UserId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Domains.Entities.Domain? Domain { get; private set; }

    public virtual User? User { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
