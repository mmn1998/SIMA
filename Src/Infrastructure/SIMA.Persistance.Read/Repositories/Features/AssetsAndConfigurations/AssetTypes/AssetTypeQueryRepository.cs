using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.AssetTypes;

public class AssetTypeQueryRepository : IAssetTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public AssetTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT DO.[Id]
              ,DO.[Name]
              ,DO.[Code]
              ,DO.[CreatedAt]
	          ,A.[Name] ActiveStatus
			  ,Do.ParentId
			  ,P.Name ParentName
          FROM [AssetAndConfiguration].AssetType DO
          INNER JOIN [Basic].[ActiveStatus] A ON DO.ActiveStatusId = A.ID
		  Left JOIN AssetAndConfiguration.AssetType P on P.Id = DO.ParentId and P.ActiveStatusId<>3
          WHERE DO.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetAssetTypeQueryResult>>> GetAll(GetAllAssetTypesQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAssetTypeQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetAssetTypeQueryResult> GetById(GetAssetTypeQuery request)
    {
        var query = $@"
          {_mainQuery} AND DO.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetAssetTypeQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}