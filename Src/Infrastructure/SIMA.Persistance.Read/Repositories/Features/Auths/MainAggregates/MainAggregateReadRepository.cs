using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Interfaces;
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
    public async Task<Result<List<GetMainAggregateQueryResult>>> GetAll(GetAllMainAggregateQuery request)
    {
        var response = new List<GetMainAggregateQueryResult>();
        int totalCount = 0;
        string countQuery = @"
SELECT DISTINCT 
COUNT(*) Result
  FROM [Authentication].[MainAggregate] M
INNER JOIN [Basic].[ActiveStatus] A on A.ID = M.ActiveStatusId
INNER JOIN [Authentication].[Domain] D on D.Id = M.DomainId
WHERE (@SearchValue is null OR (D.[Name] like @SearchValue OR M.[Name] like @SearchValue OR A.Name like @SearchValue OR M.Code like @SearchValue))
";
        string query = $@"
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
WHERE (@SearchValue is null OR (D.[Name] like @SearchValue OR M.[Name] like @SearchValue OR A.Name like @SearchValue OR M.Code like @SearchValue))
order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @Take rows only;
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
           using (var result = await connection.QueryMultipleAsync(query + countQuery , new
           {
               SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
               Take = request.PageSize,
               Skip = request.Skip
           }))
           {
                response = (await result.ReadAsync<GetMainAggregateQueryResult>()).ToList();
                totalCount = await result.ReadSingleAsync<int>();
           }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }
    
}
