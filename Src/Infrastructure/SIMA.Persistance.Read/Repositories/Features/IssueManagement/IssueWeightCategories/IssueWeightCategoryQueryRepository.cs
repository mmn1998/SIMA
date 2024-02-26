using Dapper;
using Microsoft.Extensions.Configuration;
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
    public async Task<Result<List<GetIssueWeightCategoryQueryResult>>> GetAll(BaseRequest baseRequest)
    {
        var response = new List<GetIssueWeightCategoryQueryResult>();
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = string.Empty;
            if (!string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                query = @"
SELECT TOP (1000) WG.[Id]
      ,WG.[Name]
      ,WG.[Code]
      ,WG.[MinRange]
      ,WG.[MaxRange]
      ,WG.[ActiveStatusId]
      ,S.[Name] ActiveStatus
,wg.[CreatedAt]
  FROM [IssueManagement].[IssueWeightCategory] WG
  INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
    WHERE (WG.[MinRange] like @SearchValue or WG.[MaxRange] like @SearchValue or WG.[Name] like @SearchValue or WG.[Code] like @SearchValue ) and WG.ActiveStatusId != 3
Order By wg.[CreatedAt] desc  ";
                var result = await connection.QueryAsync<GetIssueWeightCategoryQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                    .Take(baseRequest.Take)
                    .ToList();
            }
            else
            {
                query = @"
SELECT TOP (1000) WG.[Id]
      ,WG.[Name]
      ,WG.[Code]
      ,WG.[MinRange]
      ,WG.[MaxRange]
      ,WG.[ActiveStatusId]
      ,S.[Name] ActiveStatus
,wg.[CreatedAt]
  FROM [IssueManagement].[IssueWeightCategory] WG
  INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId    
where WG.ActiveStatusId != 3
Order By wg.[CreatedAt] desc  ";
                var result = await connection.QueryAsync<GetIssueWeightCategoryQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                    .Take(baseRequest.Take)
                    .ToList();
            }
        }
        return Result.Ok(response, totalCount); ;
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
            result.NullCheck();
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
