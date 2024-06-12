using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
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
    public async Task<Result<IEnumerable<GetSubjectPriorityQueryResult>>> GetAll(GetAllSubjectPrioritiesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                string queryCount = @" WITH Query as(  SELECT SP.[Id]
    ,SP.[Name]
    ,SP.[Code]
    ,SP.[Ordering]
    ,S.[Name] ActiveStatus
    ,SP.[CreatedAt]
FROM [SecurityCommitee].[SubjectPriority] SP
  INNER JOIN [Basic].[ActiveStatus] S on S.ID = SP.ActiveStatusId
  WHERE SP.ActiveStatusId <> 3  
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
							 SELECT SP.[Id]
    ,SP.[Name]
    ,SP.[Code]
    ,SP.[Ordering]
    ,S.[Name] ActiveStatus
    ,SP.[CreatedAt]
FROM [SecurityCommitee].[SubjectPriority] SP
  INNER JOIN [Basic].[ActiveStatus] S on S.ID = SP.ActiveStatusId
  WHERE SP.ActiveStatusId <> 3  
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetSubjectPriorityQueryResult>();
                return Result.Ok(response, request, count);
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
