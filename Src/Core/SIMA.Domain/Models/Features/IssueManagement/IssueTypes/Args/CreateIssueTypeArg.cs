using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Args;

public class CreateIssueTypeArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string IconPath { get; set; }
    public string ColorHex { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }

}
