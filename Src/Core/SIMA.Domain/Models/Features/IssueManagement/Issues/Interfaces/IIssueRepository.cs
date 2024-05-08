using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;

public interface IIssueRepository : IRepository<Entities.Issue>
{
    Task<Entities.Issue> GetById(long id);
    Task<Entities.Issue> GetLastIssue();
    Task ExcecuteStoreProcedure(string spName);


    Task<long> GetHighestPriority();
    Task<long> GetIssueTypeRequest();
    Task<(long,int)> GetIssueMiddleWeight();
}
