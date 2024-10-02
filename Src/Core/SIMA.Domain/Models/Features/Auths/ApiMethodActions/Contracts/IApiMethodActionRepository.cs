using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.ApiMethodActions.Contracts;

public interface IApiMethodActionRepository : IRepository<ApiMethodAction>
{
    Task<ApiMethodAction> GetById(ApiMethodActionId id);
}