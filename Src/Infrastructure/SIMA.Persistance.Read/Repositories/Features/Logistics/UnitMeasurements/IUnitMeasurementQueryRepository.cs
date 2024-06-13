using SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.UnitMeasurements;

public interface IUnitMeasurementQueryRepository : IQueryRepository
{
    Task<GetUnitMeasurementQueryResult> GetById(GetUnitMeasurementQuery request);
    Task<Result<IEnumerable<GetUnitMeasurementQueryResult>>> GetAll(GetAllUnitMeasurementQuery request);
}