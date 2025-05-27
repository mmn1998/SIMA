using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.CobitCategories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CobitCategories;

public class CobitCategoryQueryRepository : ICobitCategoryQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public CobitCategoryQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT CC.[Id]
              ,CC.[Name]
              ,CC.[Code]
	          ,A.[Name] ActiveStatus
              ,CC.CreatedAt
          FROM [RiskManagement].[CobitRiskCategory] CC
          INNER JOIN [Basic].[ActiveStatus] A ON CC.ActiveStatusId = A.ID
WHERE CC.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetCobitCategoryQueryResult>>> GetAll(GetAllCobitCategoriesQuery request)
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
        var response = await multi.ReadAsync<GetCobitCategoryQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetCobitCategoryQueryResult> GetById(GetCobitCategoryQuery request)
    {
        var query = $@"
          {_mainQuery} AND CC.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetCobitCategoryQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}