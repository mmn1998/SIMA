using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.SysConfigs.Interfaces;

public interface ISysConfigRepository : IRepository<SysConfig>
{
    Task<SysConfig> GetById(long id);
}
