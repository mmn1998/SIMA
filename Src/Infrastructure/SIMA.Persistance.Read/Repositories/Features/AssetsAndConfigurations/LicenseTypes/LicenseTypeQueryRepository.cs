using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.LicenseTypes;

public class LicenseTypeQueryRepository : ILicenseTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public LicenseTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select
     l.[Id]
    ,l.[Name]
    ,l.[Code]
    ,l.CreatedAt
    ,l.[ActiveStatusId]
    ,A.[Name] ActiveStatus
From AssetAndConfiguration.LicenseType l
join Basic.ActiveStatus A on l.ActiveStatusId = A.Id
WHERE l.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetLicenseTypeQueryResult>>> GetAll(GetAllLicenseTypeQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var queryCount = $@"
                             WITH Query as(	{_mainQuery}	)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

            string query = $@"WITH Query as(
							 {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetLicenseTypeQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetLicenseTypeQueryResult> GetById(GetLicenseTypeQuery request)
    {
        var query = $@"
         {_mainQuery} AND l.[Id] = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetLicenseTypeQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}