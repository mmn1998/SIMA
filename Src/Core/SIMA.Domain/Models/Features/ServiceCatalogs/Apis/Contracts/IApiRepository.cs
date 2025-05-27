using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;

public interface IApiRepository : IRepository<Api>
{
    Task<Api> GetById(ApiId id);
}