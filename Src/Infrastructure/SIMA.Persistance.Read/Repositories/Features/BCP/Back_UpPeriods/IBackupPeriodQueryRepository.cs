using SIMA.Application.Query.Contract.Features.BCP.Back_UpPeriods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Back_UpPeriods;

public interface IBackupPeriodQueryRepository : IQueryRepository
{
    Task<GetBackupPeriodQueryResult> GetById(GetBackupPeriodQuery request);
    Task<Result<IEnumerable<GetBackupPeriodQueryResult>>> GetAll(GetAllBackupPeriodsQuery request);
}