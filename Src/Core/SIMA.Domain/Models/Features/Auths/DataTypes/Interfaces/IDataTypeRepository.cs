using SIMA.Domain.Models.Features.Auths.DataTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.DataTypes.Interfaces;

public interface IDataTypeRepository : IRepository<DataType>
{
    Task<DataType> GetById(long id);
}
