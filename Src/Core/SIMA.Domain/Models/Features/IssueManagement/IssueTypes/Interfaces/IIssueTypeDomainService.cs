using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Interfaces;
public interface IIssueTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    bool IsHexCodeValid(string hexCode);
}
