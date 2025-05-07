using SIMA.Application.Query.Contract.Features.Auths.TimeMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.TimeMeasurements;

public interface ITimeMeasurementQueryRepository : IQueryRepository
{
    Task<GetTimeMeasurementQueryResult> GetById(GetTimeMeasurementQuery request);
    Task<Result<IEnumerable<GetTimeMeasurementQueryResult>>> GetAll(GetAllTimeMeasurementsQuery request);
}