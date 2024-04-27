using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetIssueCommentQueryResult
{
    public long Id { get; set; }
    public string Comment { get; set; }
    public long CreatorId { get; set; }
    public string CreatorFullname { get; set; }
    public DateTime CommentDate { get; set; }
    public string? PersianCommentDate => DateHelper.ToPersianDateTime(CommentDate);
}
