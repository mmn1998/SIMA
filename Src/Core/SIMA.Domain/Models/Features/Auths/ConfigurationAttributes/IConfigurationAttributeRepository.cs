using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.ConfigurationAttributes;

public interface IConfigurationAttributeRepository : IRepository<ConfigurationAttribute>
{
    Task<ConfigurationAttribute> GetById(long id);
}
