using SIMA.Application.Query.Contract.Features.TrustyDrafts.CancellationResaons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.CancellationResaons;

public interface ICancellationResaonQueryRepository : IQueryRepository
{
    Task<GetCancellationResaonQueryResult> GetById(GetCancellationResaonQuery request);
    Task<Result<IEnumerable<GetCancellationResaonQueryResult>>> GetAll(GetAllCancellationResaonsQuery request);
}