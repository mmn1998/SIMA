using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class FormUser
{
    private FormUser() { }


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