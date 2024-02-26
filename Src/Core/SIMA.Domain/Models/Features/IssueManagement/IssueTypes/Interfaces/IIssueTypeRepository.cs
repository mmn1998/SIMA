using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Interfaces;

public interface IIssueTypeRepository : IRepository<IssueType>
{
    Task<IssueType> GetById(long id);

}
