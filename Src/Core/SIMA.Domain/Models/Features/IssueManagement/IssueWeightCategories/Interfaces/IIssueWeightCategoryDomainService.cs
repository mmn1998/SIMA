using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
public interface IIssueWeightCategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    Task<bool> IsRangeExist(int minRange, int maxRange, long id);
    bool IsRangeVilid(int minRange, int maxRange);
}
