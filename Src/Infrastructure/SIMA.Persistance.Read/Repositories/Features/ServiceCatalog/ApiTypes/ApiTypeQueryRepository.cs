using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ApiTypes;

public class ApiTypeQueryRepository : IApiTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ApiTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT AT.[Id]
              ,AT.[Name]
              ,AT.[Code]
              ,AT.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ApiType] AT
          INNER JOIN [Basic].[ActiveStatus] A ON AT.ActiveStatusId = A.ID
          WHERE AT.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetApiTypesQueryResult>>> GetAll(GetAllApiTypesQuery request)
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
        var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetApiTypesQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetApiTypesQueryResult> GetById(GetApiTypeQuery request)
    {
        var query = @"
          SELECT AT.[Id]
              ,AT.[Name]
              ,AT.[Code]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ApiType] AT
          INNER JOIN [Basic].[ActiveStatus] A ON AT.ActiveStatusId = A.ID
          WHERE AT.[Id] = @Id AND AT.ActiveStatusId <> 3";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetApiTypesQueryResult>(query, new { Id = request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }
}