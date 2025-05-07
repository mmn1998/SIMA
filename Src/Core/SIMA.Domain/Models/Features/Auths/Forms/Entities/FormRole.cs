using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Args;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class FormRole
{
    private FormRole() { }

    private FormRole(CreateFormRoleArg arg)
    {
        Id = new FormRoleId(IdHelper.GenerateUniqueId());
        RoleId = new RoleId(arg.RoleId.Value);
        FormId = new FormId(arg.FormId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<FormRole> Create(CreateFormRoleArg arg)
    {
        return new FormRole(arg);
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

    public FormRoleId Id { get; private set; }
    public FormId FormId { get; private set; }
    public Form Form { get; private set; }
    public RoleId RoleId { get; private set; }
    public Role Role { get; private set; }
    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
