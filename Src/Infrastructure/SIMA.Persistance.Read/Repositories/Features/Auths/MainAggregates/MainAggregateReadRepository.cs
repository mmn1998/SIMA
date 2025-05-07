using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.MainAggregates;

public class MainAggregateReadRepository : IMainAggregateReadRepository
{
    private readonly string _connectionString;
    public MainAggregateReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetMainAggregateQueryResult>>> GetAll(GetAllMainAggregateQuery request)
    {
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();


            string queryCount = @"WITH Query as(
						  SELECT DISTINCT M.[Id]
      ,M.[DomainId]
      ,M.[Name]
      ,M.[Code]
      ,M.[ActiveStatusId]
	  ,D.[Name] DomainName
	  ,A.[Name] ActiveStatus
      ,M.CreatedAt
  FROM [Authentication].[MainAggregate] M
INNER JOIN [Basic].[ActiveStatus] A on A.ID = M.ActiveStatusId
INNER JOIN [Authentication].[Domain] D on D.Id = M.DomainId
WHERE  M.ActiveStatusId <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            string query = $@"WITH Query as(
							SELECT DISTINCT M.[Id]
      ,M.[DomainId]
      ,M.[Name]
      ,M.[Code]
      ,M.[ActiveStatusId]
	  ,D.[Name] DomainName
	  ,A.[Name] ActiveStatus
      ,M.CreatedAt
  FROM [Authentication].[MainAggregate] M
INNER JOIN [Basic].[ActiveStatus] A on A.ID = M.ActiveStatusId
INNER JOIN [Authentication].[Domain] D on D.Id = M.DomainId
WHERE  M.ActiveStatusId <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetMainAggregateQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

}
