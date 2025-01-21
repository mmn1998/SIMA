using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;

public class IssueWeightCategoryQueryRepository : IIssueWeightCategoryQueryRepository
{
    private readonly string _connectionString;
    public IssueWeightCategoryQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetIssueWeightCategoryQueryResult>>> GetAll(GetAllIssueWeightCategoriesQuery request)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
						   SELECT WG.[Id]
       ,WG.[Name]
       ,WG.[Code]
       ,WG.[MinRange]
       ,WG.[MaxRange]
       ,WG.[ActiveStatusId]
       ,S.[Name] ActiveStatus
       ,wg.[CreatedAt]
   FROM [IssueManagement].[IssueWeightCategory] WG
   INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
     WHERE WG.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            var query = $@"WITH Query as(
							SELECT WG.[Id]
       ,WG.[Name]
       ,WG.[Code]
       ,WG.[MinRange]
       ,WG.[MaxRange]
       ,WG.[ActiveStatusId]
       ,S.[Name] ActiveStatus
       ,wg.[CreatedAt]
   FROM [IssueManagement].[IssueWeightCategory] WG
   INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
     WHERE WG.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetIssueWeightCategoryQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetIssueWeightCategoryQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT TOP (1000) WG.[Id]
      ,WG.[Name]
      ,WG.[Code]
      ,WG.[MinRange]
      ,WG.[MaxRange]
      ,WG.[ActiveStatusId]
      ,S.[Name] ActiveStatus
  FROM [IssueManagement].[IssueWeightCategory] WG
  INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
  WHERE WG.[Id] = @Id and WG.ActiveStatusId != 3
";
            var result = await connection.QueryFirstOrDefaultAsync<GetIssueWeightCategoryQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<long> GetIdByWeight(int weight)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                            SELECT TOP 1 Id
                            FROM IssueManagement.IssueWeightCategory
                            WHERE MinRange <= @Weight AND MaxRange >= @Weight";
            var result = await connection.QueryFirstOrDefaultAsync<long>(query, new { Weight = weight });
            //result.NullCheck();
            return result;
        }
    }
    public async Task<string> GetByWeight(int weight)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                            SELECT TOP 1 Name
                            FROM IssueManagement.IssueWeightCategory
                            WHERE MinRange <= @Weight AND MaxRange >= @Weight";
            var result = await connection.QueryFirstOrDefaultAsync<string>(query, new { Weight = weight });
            if (result == null) throw (SimaException)SimaResultException.NotFound;
            return result;
        }
    }
}
