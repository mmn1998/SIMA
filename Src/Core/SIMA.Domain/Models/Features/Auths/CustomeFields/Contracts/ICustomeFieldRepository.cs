using SIMA.Domain.Models.Features.Auths.CustomeFields.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFields.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.CustomeFields.Contracts;

public interface ICustomeFieldRepository : IRepository<CustomeField>
{
    Task<CustomeField> GetById(CustomeFieldId id);
}