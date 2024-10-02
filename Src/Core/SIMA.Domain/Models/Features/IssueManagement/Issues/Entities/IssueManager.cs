using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class IssueManager : Entity
{
    private IssueManager()
    {
    }

    public IssueManager(CreateIssueManagerArg arg)
    {
        Id = new IssueManagerId(arg.Id);
        IssueId = new IssueId(arg.IssueId);
        UserId = new UserId(arg.UserId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static IssueManager Create(CreateIssueManagerArg arg)
    {
        return new IssueManager(arg);
    }
    public IssueManagerId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public UserId UserId { get; private set; }
    public virtual User User { get; private set; }
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
