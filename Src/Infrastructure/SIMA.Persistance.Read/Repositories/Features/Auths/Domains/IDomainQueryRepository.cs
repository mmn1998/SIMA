using SIMA.Application.Query.Contract.Features.Auths.Domains;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Domains;

public interface IDomainQueryRepository : IQueryRepository
{
    Task<GetDomainQueryResult> FindById(long id);
    Task<List<GetDomainQueryResult>> GetAll();
}
