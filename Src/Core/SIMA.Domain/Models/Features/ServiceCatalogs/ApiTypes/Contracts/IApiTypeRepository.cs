using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Contracts
{
    public interface IApiTypeRepository : IRepository<ApiType>
    {
        Task<ApiType> GetById(ApiTypeId id);
    }
}
