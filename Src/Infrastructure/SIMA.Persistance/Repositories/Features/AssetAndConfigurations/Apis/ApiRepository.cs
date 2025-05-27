using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.Apis;

public class ApiRepository : Repository<Api>, IApiRepository
{
    private readonly SIMADBContext _context;

    public ApiRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Api> GetById(ApiId Id)
    {
        var entity = await _context.Apis
            .Include(x => x.ApiDocuments)
            .Include(x => x.ApiSupportTeams)
            .Include(x => x.ConfigurationItemApis)
            .Include(x => x.ApiRequestHeaderParams)
            .Include(x => x.ApiResponseHeaderParams)
            .Include(x => x.ApiRequestBodyParams)
            .Include(x => x.ApiResponseBodyParams)
            .Include(x => x.ApiRequestUrlParams)
            .Include(x => x.ApiRequestQueryStringParams)
                        .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}