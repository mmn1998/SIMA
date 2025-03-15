using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataCenters;

public class DataCenterQueryRepository : IDataCenterQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public DataCenterQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select
     DC.[Id]
    ,DC.[Name]
    ,DC.[Code]
    ,DC.CreatedAt
    ,DC.[ActiveStatusId]
    ,A.[Name] ActiveStatus
From AssetAndConfiguration.DataCenter DC
join Basic.ActiveStatus A on DC.ActiveStatusId = A.Id
WHERE DC.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetDataCenterQueryResult>>> GetAll(GetAllDataCentersQuery request)
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
                var response = await multi.ReadAsync<GetDataCenterQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetDataCenterQueryResult> GetById(GetDataCenterQuery request)
    {
        var query = $@"
         {_mainQuery} AND DC.[Id] = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetDataCenterQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}