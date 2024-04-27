using SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.SubjectPriorities;

public interface ISubjectPriorityQueryRepository : IQueryRepository
{
    Task<GetSubjectPriorityQueryResult> GetById(long Id);
    Task<Result<List<GetSubjectPriorityQueryResult>>> GetAll(GetAllSubjectPrioritiesQuery request);
}