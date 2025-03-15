using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedures;

public class DataProcedureQueryRepository : IDataProcedureQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public DataProcedureQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select
     DP.[Id]
    ,DP.[Name]
    ,DP.[Code]
    ,DP.CreatedAt
    ,DP.[ActiveStatusId]
    ,A.[Name] ActiveStatus
	,DP.DataProcedureTypeId
	,DPT.Name DataProcedureTypeName
	,DP.DatabaseId
	,CI.Title DatabaseName
	,DP.Description
	,DP.IsInternalApi
	,Dp.ReleaseDate
	,DP.VersionNumber
From AssetAndConfiguration.DataProcedure DP
join Basic.ActiveStatus A on DP.ActiveStatusId = A.Id
inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = DP.DatabaseId and CI.ActiveStatusId<>3
inner join AssetAndConfiguration.DataProcedureType DPT on DPT.Id = Dp.DataProcedureTypeId and DPT.ActiveStatusId<>3
WHERE DP.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetDataProcedureQueryResult>>> GetAll(GetAllDataProceduresQuery request)
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
                var response = await multi.ReadAsync<GetDataProcedureQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetDataProcedureQueryResult> GetById(GetDataProcedureQuery request)
    {
        var query = $@"
         {_mainQuery} AND DP.[Id] = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetDataProcedureQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}