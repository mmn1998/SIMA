using SIMA.Application.Query.Contract.Features.Auths.SysConfigs;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.SysConnfigs;

public interface ISysConfigQueryRepository : IQueryRepository
{
    Task<SysConfig> FindById(long id);
    Task<List<GetSysConfigQueryResult>> GetAll();
}
