using Dapper;
using Microsoft.Extensions.Configuration;
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
    public async Task<Result<List<GetMainAggregateQueryResult>>> GetAll(GetAllMainAggregateQuery request)
    {
        var response = new List<GetMainAggregateQueryResult>();
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
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
                        WHERE M.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
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
                        WHERE M.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize
                }))
                {
                    response = (await multi.ReadAsync<GetMainAggregateQueryResult>()).ToList();
                    var count = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
                string countQuery = @"
SELECT DISTINCT 
COUNT(*) Result
  FROM [Authentication].[MainAggregate] M
INNER JOIN [Basic].[ActiveStatus] A on A.ID = M.ActiveStatusId
INNER JOIN [Authentication].[Domain] D on D.Id = M.DomainId
WHERE (@SearchValue is null OR (D.[Name] like @SearchValue OR M.[Name] like @SearchValue OR A.Name like @SearchValue OR M.Code like @SearchValue)) AND M.ActiveStatusId <> 3
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
WHERE (@SearchValue is null OR (D.[Name] like @SearchValue OR M.[Name] like @SearchValue OR A.Name like @SearchValue OR M.Code like @SearchValue)) AND M.ActiveStatusId <> 3
order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @Take rows only;
";
                using (var result = await connection.QueryMultipleAsync(query + countQuery, new
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
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }

}
