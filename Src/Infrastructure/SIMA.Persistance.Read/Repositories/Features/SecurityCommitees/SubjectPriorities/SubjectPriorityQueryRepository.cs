using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.SubjectPriorities;

public class SubjectPriorityQueryRepository : ISubjectPriorityQueryRepository
{
    private readonly string _connectionString;
    public SubjectPriorityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetSubjectPriorityQueryResult>>> GetAll(GetAllSubjectPrioritiesQuery request)
    {
        var response = new List<GetSubjectPriorityQueryResult>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string queryCount = @"
                              SELECT COUNT(*) Result
                                      FROM [SecurityCommitee].[SubjectPriority] SP
                                      INNER JOIN [Basic].[ActiveStatus] S on S.ID = SP.ActiveStatusId
                                       WHERE (@SearchValue is null OR  (SP.[Ordering] like @SearchValue or SP.[Name] like @SearchValue or SP.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                       SP.ActiveStatusId <> 3 ;";

            string query = $@"
                                                          SELECT SP.[Id]
                                  ,SP.[Name]
                                  ,SP.[Code]
                                  ,SP.[Ordering]
	                              ,S.[Name] ActiveStatus
                                  ,SP.[CreatedAt]
                              FROM [SecurityCommitee].[SubjectPriority] SP
                                INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                                WHERE (@SearchValue is null OR  (SP.[Ordering] like @SearchValue or SP.[Name] like @SearchValue or SP.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                           SP.ActiveStatusId <> 3  
                                 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}            
                                  OFFSET @Skip rows FETCH NEXT @Take rows only ;";
            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                response = (await multi.ReadAsync<GetSubjectPriorityQueryResult>()).ToList();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }

    public async Task<GetSubjectPriorityQueryResult> GetById(long Id)
    {
        var response = new GetSubjectPriorityQueryResult();
        string query = $@"
SELECT SP.[Id]
      ,SP.[Name]
      ,SP.[Code]
      ,SP.[Ordering]
	  ,A.[Name] ActiveStatus
  FROM [SecurityCommitee].[SubjectPriority] SP
  INNER JOIN [Basic].[ActiveStatus] A ON A.ID = SP.ActiveStatusId
  WHERE SP.[Id] = @Id
";
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetSubjectPriorityQueryResult>(query, new { Id = Id });
            result.NullCheck();
            response = result ?? throw SimaResultException.NotFound;
        }
        return response;
    }
}
