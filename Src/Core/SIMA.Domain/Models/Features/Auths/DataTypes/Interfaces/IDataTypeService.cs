using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.DataTypes.Interfaces;

public interface IDataTypeService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
