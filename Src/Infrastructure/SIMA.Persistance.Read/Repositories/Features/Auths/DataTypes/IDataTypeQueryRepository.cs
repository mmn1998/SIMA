using SIMA.Application.Query.Contract.Features.Auths.DataTypes;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.DataTypes;

public interface IDataTypeQueryRepository : IQueryRepository
{
    Task<IEnumerable<GetDataTypeQueryResult>> GetAll(GetDataTypeQuery request);
}