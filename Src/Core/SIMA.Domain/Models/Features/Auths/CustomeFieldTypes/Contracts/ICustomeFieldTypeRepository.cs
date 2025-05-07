using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Contracts;

public interface ICustomeFieldTypeRepository : IRepository<CustomeFieldType>
{
    Task<CustomeFieldType> GetById(CustomeFieldTypeId id);
}
