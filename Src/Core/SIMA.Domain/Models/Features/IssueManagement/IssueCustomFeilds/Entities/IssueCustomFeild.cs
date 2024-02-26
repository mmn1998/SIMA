using SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Entities;

public class IssueCustomFeild : Entity
{
    private IssueCustomFeild()
    {
    }

    public IssueCustomFeild(CreateIssueCustomFeildArg arg)
    {
        Id = new IssueCustomFeildId(IdHelper.GenerateUniqueId());
        IssueId = arg.IssueId;
        CustomeFeildId = arg.CustomeFeildId;
        KeyValues = arg.KeyValues;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public async Task<IssueCustomFeild> Create(CreateIssueCustomFeildArg arg)
    {
        return new IssueCustomFeild(arg);
    }
    public async void Modify(ModifyIssueCustomFeildArg arg)
    {
        IssueId = arg.IssueId;
        CustomeFeildId = arg.CustomeFeildId;
        KeyValues = arg.KeyValues;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueCustomFeildId Id { get; private set; }
    public long IssueId { get; private set; }
    public long CustomeFeildId { get; private set; }
    public string KeyValues { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
}
