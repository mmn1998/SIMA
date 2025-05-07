using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Warehouses;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Warehouses;

public class WarehouseQueryRepository : IWarehouseQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public WarehouseQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
     W.[Id]
    ,W.[Name]
    ,W.[Code]
    ,W.[CreatedAt]
	,A.[Name] ActiveStatus
FROM [Authentication].[Warehouse] W
INNER JOIN [Basic].[ActiveStatus] A ON W.ActiveStatusId = A.ID
WHERE W.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetWarehouseQueryResult>>> GetAll(GetAllWarehousesQuery request)
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
        var response = await multi.ReadAsync<GetWarehouseQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetWarehouseQueryResult> GetById(GetWarehouseQuery request)
    {
        var query = $@"
          {_mainQuery} AND W.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetWarehouseQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}