using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;

public interface IIssueWeightCategoryRepository : IRepository<IssueWeightCategory>
{
    Task<IssueWeightCategory> GetById(long id);

}
