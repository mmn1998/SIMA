using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.SubjectPriorities;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class MeetingHoldingStatusQueryRepository : IMeetingHoldingStatusQueryRepository
    {
        private readonly string _connectionString;
        public MeetingHoldingStatusQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<Result<IEnumerable<GetMeetingHoldingStatusQueryResult>>> GetAll(GetAllMeetingHoldingStatusQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string queryCount = @" WITH Query as(
						  SELECT M.[Id]
    ,M.[Name]
    ,M.[Code]
    ,S.[Name] ActiveStatus
    ,S.[Id] ActiveStatusId
FROM [SecurityCommitee].[MeetingHoldingStatus] M
INNER JOIN [Basic].[ActiveStatus] S on S.ID = M.ActiveStatusId
WHERE M.ActiveStatusId <> 3  
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@"
                              WITH Query as(
							SELECT M.[Id]
    ,M.[Name]
    ,M.[Code]
     ,M.CreatedAt
    ,S.[Name] ActiveStatus
    ,S.[Id] ActiveStatusId
FROM [SecurityCommitee].[MeetingHoldingStatus] M
INNER JOIN [Basic].[ActiveStatus] S on S.ID = M.ActiveStatusId
WHERE M.ActiveStatusId <> 3  
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetMeetingHoldingStatusQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }

        }

        public async Task<GetMeetingHoldingStatusQueryResult> GetById(long Id)
        {
            try
            {
                var response = new GetMeetingHoldingStatusQueryResult();
                string query = $@"
                    SELECT M.[Id]
                          ,M.[Name]
                          ,M.[Code]
        
                    	  ,A.[Name] ActiveStatus
                          ,A.[Id] ActiveStatusId
                      FROM [SecurityCommitee].[MeetingHoldingStatus] M
                      INNER JOIN [Basic].[ActiveStatus] A ON A.ID = M.ActiveStatusId
                      WHERE M.[Id] = @Id
                    ";
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryFirstOrDefaultAsync<GetMeetingHoldingStatusQueryResult>(query, new { Id = Id });
                    result.NullCheck();
                    response = result ?? throw SimaResultException.NotFound;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
