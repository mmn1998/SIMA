using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.Resources.Contracts;

public interface IResourceRepository : IRepository<Resource>
{
    Task<Resource> GetById(ResourceId id);
}