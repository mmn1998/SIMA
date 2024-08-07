using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.UIInputElements;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.UIInputElements;

public class UIInputElementQueryRepository : IUIInputElementQueryRepository
{
    private readonly string _connectionString;
    public UIInputElementQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetUIInputElementQueryResult>>> GetAll(GetAllUIInputElementsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               SELECT C.[Id]
              ,C.[Name]
              ,C.[Code] 
              ,C.[IsMultiSelect]
              ,C.[IsSingleSelect]
              ,C.[HasInputInEachRecord]
              ,C.CreatedAt
              ,C.[ActiveStatusId]
              ,A.[Name] ActiveStatus
          FROM [Basic].[UIInputElement] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
								WHERE C.[ActiveStatusID] <> 3
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
                var response = await multi.ReadAsync<GetUIInputElementQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetUIInputElementQueryResult> GetById(GetUIInputElementQuery request)
    {
        var query = @"
          SELECT C.[Id]
              ,C.[Name]
              ,C.[Code] 
              ,C.[IsMultiSelect]
              ,C.[IsSingleSelect]
              ,C.[HasInputInEachRecord]
              ,C.[ActiveStatusId]
              ,A.[Name] ActiveStatus
          FROM [Basic].[UIInputElement] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
          WHERE C.[Id] = @Id AND C.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetUIInputElementQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}