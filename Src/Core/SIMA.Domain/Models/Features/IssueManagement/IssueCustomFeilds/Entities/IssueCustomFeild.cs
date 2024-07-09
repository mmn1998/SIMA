using SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Entities;

public class IssueCustomFeild : Entity
{
    private IssueCustomFeild()
    {
    }

    public IssueCustomFeild(CreateIssueCustomFeildArg arg)
    {
        Id = new IssueCustomFeildId(IdHelper.GenerateUniqueId());
        IssueId = new(arg.IssueId);
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
    public async Task Modify(ModifyIssueCustomFeildArg arg)
    {
        IssueId = new(arg.IssueId);
        CustomeFeildId = arg.CustomeFeildId;
        KeyValues = arg.KeyValues;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueCustomFeildId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public long CustomeFeildId { get; private set; }
    public string KeyValues { get; private set; }
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
}
