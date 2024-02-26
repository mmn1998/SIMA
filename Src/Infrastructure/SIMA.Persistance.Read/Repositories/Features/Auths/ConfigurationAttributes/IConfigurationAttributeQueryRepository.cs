using SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ConfigurationAttributes;

public interface IConfigurationAttributeQueryRepository : IQueryRepository
{
    Task<bool> CheckEnglishKeyIsExists(string key);
    Task<ConfigurationAttribute> FindById(long id);
    Task<List<GetConfigurationAttributeQueryResult>> GetAll();
}
