using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.LicenseTypes;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.LicenseTypes;

public class LicenseTypeQueryHandler : IQueryHandler<GetLicenseTypeQuery, Result<GetLicenseTypeQueryResult>>,
    IQueryHandler<GetAllLicenseTypeQuery, Result<IEnumerable<GetLicenseTypeQueryResult>>>
{
    private readonly ILicenseTypeQueryRepository _repository;

    public LicenseTypeQueryHandler(ILicenseTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetLicenseTypeQueryResult>> Handle(GetLicenseTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetLicenseTypeQueryResult>>> Handle(GetAllLicenseTypeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }  
}