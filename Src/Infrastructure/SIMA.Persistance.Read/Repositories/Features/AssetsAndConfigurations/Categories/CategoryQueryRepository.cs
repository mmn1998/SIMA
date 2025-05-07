using System.Data.SqlClient;
using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Categories;

public class CategoryQueryRepository: ICategoryQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;

    public CategoryQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT DO.[Id]
              ,DO.[Name]
              ,DO.[Code]
              ,DO.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [AssetAndConfiguration].Category DO
          INNER JOIN [Basic].[ActiveStatus] A ON DO.ActiveStatusId = A.ID
          WHERE DO.ActiveStatusId <> 3";
    }
    public async Task<GetCategoryQueryResult> GetById(GetCategoryQuery request)
    {
	    var query = $@"
          {_mainQuery} AND DO.[Id] = @Id";
	    using var connection = new SqlConnection(_connectionString);
	    await connection.OpenAsync();
	    var result = await connection.QueryFirstAsync<GetCategoryQueryResult>(query, new { request.Id });
	    result.NullCheck();
	    return result ?? throw SimaResultException.NotFound;
    }

    public async Task<Result<IEnumerable<GetCategoryQueryResult>>> GetAll(GetAllCategoryQuery request)
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
        var response = await multi.ReadAsync<GetCategoryQueryResult>();
        return Result.Ok(response, request, count);
    }
}