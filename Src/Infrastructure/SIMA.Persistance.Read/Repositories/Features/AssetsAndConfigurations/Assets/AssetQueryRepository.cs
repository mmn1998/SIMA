using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Assets;

public class AssetQueryRepository : IAssetQueryRepository
{
    private readonly string _connectionString;
    public AssetQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }


    public async Task<Result<IEnumerable<GetAssetQueryResult>>> GetAll(GetAllAssetsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                              Select 
	                             A.[Id]
	                            ,A.[SerialNumber]
	                            ,A.[Title]
	                            ,A.[Model]
                                ,A.CreatedAt
	                            ,active.[Name] ActiveStatus
	                            From AssetAndConfiguration.Asset A
	                            join Basic.ActiveStatus active on A.ActiveStatusId = active.Id
	                            WHERE A.[ActiveStatusID] <> 3
							";
            var queryCount = $@"
                             WITH Query as(	{mainQuery}	)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

            string query = $@"WITH Query as(
							 {mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAssetQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

}