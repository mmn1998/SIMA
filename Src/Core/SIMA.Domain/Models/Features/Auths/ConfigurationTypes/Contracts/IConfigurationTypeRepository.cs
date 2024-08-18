using SIMA.Domain.Models.Features.Auths.ConfigurationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.ConfigurationTypes.Contracts;

public interface IConfigurationTypeRepository : IRepository<ConfigurationType>
{
    Task<ConfigurationType> GetById(ConfigurationTypeId id);
}