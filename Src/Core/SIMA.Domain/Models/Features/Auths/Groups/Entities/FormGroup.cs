using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Groups.Entities;

public class FormGroup
{

    private FormGroup()
    {

    }
    private FormGroup(CreateFormGroupArg arg)
    {
        Id = new FormGroupId(IdHelper.GenerateUniqueId());
        GroupId = new GroupId(arg.GroupId.Value);
        FormId = new FormId(arg.FormId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<FormGroup> Create(CreateFormGroupArg arg)
    {
        return new FormGroup(arg);
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
    public FormGroupId Id { get; private set; }
    public FormId FormId { get; private set; }
    public Form Form { get; private set; }
    public GroupId GroupId { get; private set; }
    public Group Group { get; private set; }
    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    

}
