using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Contracts;

public interface IDraftIssueTypeRepository : IRepository<DraftIssueType>
{
    Task<DraftIssueType> GetById(DraftIssueTypeId id);
}