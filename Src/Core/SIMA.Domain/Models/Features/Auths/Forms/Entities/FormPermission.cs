using SIMA.Domain.Models.Features.Auths.Forms.Args;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class FormPermission
{
    private FormPermission() { }
    private FormPermission(CreateFormPermissionArg arg)
    {
        Id = new(arg.Id);
        FormId = new(arg.FormId);
        PermissionId = new(arg.PermissionId);
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static FormPermission Create(CreateFormPermissionArg arg)
    {
        return new FormPermission(arg);
    }

    public FormPermissionId Id { get; private set; }
    public FormId FormId { get; private set; }
    public virtual Form Form { get; private set; }
    public virtual Permission Permission { get; private set; }
    public PermissionId PermissionId { get; private set; }
    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
