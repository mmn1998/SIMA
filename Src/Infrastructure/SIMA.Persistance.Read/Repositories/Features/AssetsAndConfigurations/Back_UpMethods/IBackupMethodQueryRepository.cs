using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Back_UpMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Back_UpMethods;

public interface IBackupMethodQueryRepository : IQueryRepository
{
    Task<GetBackupMethodQueryResult> GetById(GetBackupMethodQuery request);
    Task<Result<IEnumerable<GetBackupMethodQueryResult>>> GetAll(GetAllBackupMethodsQuery request);
}