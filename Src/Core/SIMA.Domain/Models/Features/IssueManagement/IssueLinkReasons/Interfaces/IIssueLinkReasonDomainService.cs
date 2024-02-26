using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Interfaces;
public interface IIssueLinkReasonDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
