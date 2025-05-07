using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.BusinessEntities;

public class BusinessEntityQueryRepository : IBusinessEntityQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public BusinessEntityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT BE.[Id]
      ,BE.[Name]
      ,BE.[EnglishName]
      ,BE.[Color]
	  ,BE.CreatedAt
	  ,A.[Name] ActiveStatus
  FROM [SIMADB].[Basic].[BusinessEntity] BE
  join Basic.ActiveStatus A on BE.ActiveStatusId = A.Id and BE.ActiveStatusId<>3
";
    }
    public async Task<Result<IEnumerable<GetBusinessEntityQueryResult>>> GetAll(GetAllBusinessEntitiesQuery request)
    {
        using var connection = new SqlConnection(_connectionString);

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
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);

        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetBusinessEntityQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetBusinessEntityQueryResult> GetById(GetBusinessEntityQuery request)
    {
        var query = $@"
          {_mainQuery}
          WHERE BE.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetBusinessEntityQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;

    }
}