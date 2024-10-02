using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class FormUser
{
    private FormUser() { }

    private FormUser(CreateFormUserArg arg)
    {
        Id = new FormUserId(IdHelper.GenerateUniqueId());
        UserId = new UserId(arg.UserId.Value);
        FormId = new FormId(arg.FormId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<FormUser> Create(CreateFormUserArg arg)
    {
        return new FormUser(arg);
    }
    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public FormUserId Id { get; private set; }
    public FormId FormId { get; private set; }
    public virtual Form Form { get; private set; }
    public virtual User User { get; private set; }
    public UserId UserId { get; private set; }

    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
}