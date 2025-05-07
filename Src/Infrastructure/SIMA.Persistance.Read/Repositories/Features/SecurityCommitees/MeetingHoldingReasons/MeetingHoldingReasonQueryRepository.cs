using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingReasons;

public class MeetingHoldingReasonQueryRepository : IMeetingHoldingReasonQueryRepository
{
    private readonly string _connectionString;
    public MeetingHoldingReasonQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetMeetingHoldingReasonQueryResult>>> GetAll(GetAllMeetingHoldingReasonsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
           
                string queryCount = $@" WITH Query as(
						    SELECT  MHR.[Id]
    ,MHR.[Code]
    ,MHR.[Name]
    ,S.[Name] ActiveStatus
    ,MHR.[CreatedAt]
FROM [SecurityCommitee].[MeetingHoldingReason] MHR
INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
      WHERE MHR.ActiveStatusId <> 3  
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
                string query = $@" WITH Query as(
							 SELECT  MHR.[Id]
    ,MHR.[Code]
    ,MHR.[Name]
    ,S.[Name] ActiveStatus
    ,MHR.[CreatedAt]
FROM [SecurityCommitee].[MeetingHoldingReason] MHR
INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
      WHERE MHR.ActiveStatusId <> 3  
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetMeetingHoldingReasonQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
    }

    public async Task<GetMeetingHoldingReasonQueryResult> GetById(long Id)
    {
        var response = new GetMeetingHoldingReasonQueryResult();

        string query = @"
SELECT  MHR.[Id]
      ,MHR.[Code]
      ,MHR.[Name]
  FROM [SecurityCommitee].[MeetingHoldingReason] MHR
  INNER JOIN [Basic].[ActiveStatus] S ON S.ID = MHR.ActiveStatusId
  WHERE MHR.Id = @Id
";
        using (var connection = new SqlConnection(_connectionString))
        {
            var result = await connection.QueryFirstOrDefaultAsync<GetMeetingHoldingReasonQueryResult>(query, new { Id = Id });
            result.NullCheck();
            response = result ?? throw SimaResultException.NotFound;
        }
        return response;
    }
}
