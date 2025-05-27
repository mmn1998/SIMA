using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Application.Query.Contract.Features.Notifications.Messages;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.EvaluationCriterias;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Notifications.Messages
{
    public class MessageQueryRepository : IMessageQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        private readonly ISimaIdentity _simaIdentity;

        public MessageQueryRepository(IConfiguration configuration , ISimaIdentity simaIdentity)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"
                        SELECT 
                        M.Id,
                        M.Subject,
                        M.Description,
                        M.ExpireDate,
                        M.CreatedAt,
                        P.FirstName + ' ' + P.LastName AS CreatedBy,
                         CASE 
                            WHEN MA.Id IS NOT NULL THEN 1 
                            ELSE 0 
                         END AS HasDocument
                    FROM Notification.Message M
                    JOIN Authentication.Users U ON M.CreatedBy = U.Id
                    JOIN Authentication.Profile P ON P.Id = U.ProfileID
                    LEFT JOIN Notification.MessageAttachment MA ON MA.MessageId = M.Id
                    WHERE M.ActiveStatusId <> 3
                        ";
            _simaIdentity = simaIdentity;
        }

        public async Task<Result<IEnumerable<GetMessageQueryResult>>> GetAll(GetAllMessagesQuery request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


            string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);


            var count = await multi.ReadFirstAsync<int>();
            var response = await multi.ReadAsync<GetMessageQueryResult>();
            return Result.Ok(response, request, count);
        }
        public async Task<Result<IEnumerable<GetMessageQueryResult>>> GetAllForUser(GetAllMessageForUserQuery request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string queryCount = $@" WITH Query as(
                                    SELECT 
	                                    distinct
                                        M.Id,
                                        M.Subject,
                                        M.Description,
                                        M.IsEveryOne,
                                        M.ExpireDate,
                                        M.CreatedAt,
                                        P.FirstName + ' ' + P.LastName AS CreatedBy,
                                         CASE 
                                            WHEN MA.Id IS NOT NULL THEN 1 
                                            ELSE 0 
                                         END AS HasDocument
                                    FROM Notification.Message M
                                    LEFT JOIN Notification.MessageAttachment MA ON MA.MessageId = M.Id 
                                    JOIN Authentication.Users U ON M.CreatedBy = U.Id
                                    JOIN Authentication.Profile P ON P.Id = U.ProfileID
                                    left join Notification.MessagePositionDisplay MPD on  MPD.MessageId = M.Id and MPD.ActiveStatusId <> 3
                                    left join Notification.MessageGroupDisplay MGD on  MGD.MessageId = M.Id and MGD.ActiveStatusId <> 3
                                    left join Organization.Position Po on Po.Id = MPD.PositionId
                                    left join Organization.Staff S on S.PositionId = Po.Id
                                    left JOIN Authentication.Profile PP ON PP.Id = S.ProfileId
                                    left join Authentication.Users UU on UU.ProfileID = PP.Id
                                    WHERE M.ActiveStatusId <> 3 and( M.IsEveryOne = '1' or UU.Id = @userId or MGD.GroupId in @groupId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


            string query = $@" WITH Query as(
                                        SELECT 
	                                        distinct
                                            M.Id,
                                            M.Subject,
                                            M.Description,
                                            M.IsEveryOne,
                                            M.ExpireDate,
                                            M.CreatedAt,
                                            P.FirstName + ' ' + P.LastName AS CreatedBy,
                                             CASE 
                                                WHEN MA.Id IS NOT NULL THEN 1 
                                                ELSE 0 
                                             END AS HasDocument
                                        FROM Notification.Message M
                                        LEFT JOIN Notification.MessageAttachment MA ON MA.MessageId = M.Id 
                                        JOIN Authentication.Users U ON M.CreatedBy = U.Id
                                        JOIN Authentication.Profile P ON P.Id = U.ProfileID
                                        left join Notification.MessagePositionDisplay MPD on  MPD.MessageId = M.Id and MPD.ActiveStatusId <> 3
                                        left join Notification.MessageGroupDisplay MGD on  MGD.MessageId = M.Id and MGD.ActiveStatusId <> 3
                                        left join Organization.Position Po on Po.Id = MPD.PositionId
                                        left join Organization.Staff S on S.PositionId = Po.Id
                                        left JOIN Authentication.Profile PP ON PP.Id = S.ProfileId
                                        left join Authentication.Users UU on UU.ProfileID = PP.Id
                                        WHERE M.ActiveStatusId <> 3 and(M.IsEveryOne = '1' or UU.Id = @userId or MGD.GroupId in @groupId)
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("userId", _simaIdentity.UserId);
            dynaimcParameters.Item2.Add("groupId", _simaIdentity.GroupId);
            using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);


            var count = await multi.ReadFirstAsync<int>();
            var response = await multi.ReadAsync<GetMessageQueryResult>();
            return Result.Ok(response, request, count);
        }

        public async Task<Result<IEnumerable<GetMessageQueryResult>>> GetSeenCountForUser(GetAllMessageForUserQuery request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


            string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);


            var count = await multi.ReadFirstAsync<int>();
            var response = await multi.ReadAsync<GetMessageQueryResult>();
            return Result.Ok(response, request, count);
        }

        public async Task<GetMessageQueryResult> GetById(long id)
        {
            {
                try
                {
                    var response = new GetMessageQueryResult();
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        string query = @"
                           Select 
                                 M.Id
                                ,M.Subject
                                ,M.Description
                                ,M.ExpireDate
                                ,M.CreatedAt
                                ,P.FirstName + ' ' + P.LastName CreatedBy,
                                 CASE 
                                    WHEN MA.Id IS NOT NULL THEN 1 
                                    ELSE 0 
                                 END AS HasDocument,
	                               CASE 
                                      WHEN MS.Id IS NOT NULL THEN 1 
                                      ELSE 0 
                                   END AS IsSeen
                                from Notification.Message M 
                                join Authentication.Users U on M.CreatedBy = U.Id
                                join Authentication.Profile P on P.Id = u.ProfileID
                                LEFT JOIN Notification.MessageAttachment MA ON MA.MessageId = M.Id
                                LEFT JOIN Notification.MessageSeenStatistics MS ON MS.MessageId = M.Id
                                where M.ActiveStatusId <> 3 and M.Id = @Id


                                ----notificationMessagePositionDisplay

                                Select 
                                 MP.PositionId 
                                ,PT.Id  PositionTypeId
                                ,PT.Name PositionTypeName
                                ,PL.Id PositionLevelId 
                                ,PL.Name PositionLevelName
                                ,P.Id PositionName
                                ,P.Name PositionName
                                ,D.Id DepartmentId
                                ,D.Name DepartmentName
                                ,B.Id BranchId
                                ,B.Name BranchName
                                ,S.Name ActiveStatus
                                ,MP.CreatedAt
                                from Notification.Message M 
                                join Notification.MessagePositionDisplay MP on M.Id = MP.MessageId and MP.ActiveStatusId <> 3
                                join Organization.Position P on P.Id = MP.PositionId and P.ActiveStatusId <> 3
                                join Organization.PositionType PT on P.PositionTypeId = PT.Id and PT.ActiveStatusId <> 3
                                join Organization.PositionLevel PL on PL.Id = P.PositionLevelId and PL.ActiveStatusId <> 3
                                join Organization.Department D on P.DepartmentId = D.Id and D.ActiveStatusId <> 3
                                join Bank.Branch B on B.Id = P.BranchId and B.ActiveStatusId <> 3
                                join Basic.ActiveStatus S on S.Id = M.ActiveStatusId 
                                where M.ActiveStatusId <> 3 and M.Id = @Id


                                ----notificationMessageGroupDisplay
                                Select 
                                MG.GroupId
                                ,G.Name
                                ,G.Code
                                ,S.Name ActiveStatus
                                ,MG.CreatedAt
                                from Notification.Message M 
                                join Notification.MessageGroupDisplay MG on M.Id = MG.MessageId and MG.ActiveStatusId <> 3
                                join Authentication.Groups G on G.Id = MG.GroupId and G.ActiveStatusId<> 3
                                join Basic.ActiveStatus S on S.Id = M.ActiveStatusId 
                                where M.ActiveStatusId <> 3 and M.Id = @Id


                                ----notificationMessageAttachment
                                Select 
                                 MA.DocumentId
                                ,D.Name
                                ,DT.Id DocumentTypeId
                                ,DT.Name DocumentTypeName
                                ,MA.CreatedAt
                                from Notification.Message M
                                Join Notification.MessageAttachment MA on M.Id = MA.MessageId and MA.ActiveStatusId<> 3
                                join DMS.Documents D on D.Id = MA.DocumentId and D.ActiveStatusId<> 3
                                join DMS.DocumentType DT on DT.Id = D.DocumentTypeId and D.ActiveStatusId <> 3
                                where M.ActiveStatusId <> 3 and M.Id = @Id

        ";

                        using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
                        {
                            response = multi.ReadAsync<GetMessageQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                            response.NotificationMessagePositionDisplayList = multi.ReadAsync<GetNotificationMessagePositionDisplay>().GetAwaiter().GetResult().ToList();
                            response.NotificationMessageGroupDisplayList = multi.ReadAsync<GetNotificationMessageGroupDisplay>().GetAwaiter().GetResult().ToList();
                            response.NotificationMessageAttachmentDisplayList = multi.ReadAsync<GetNotificationMessageAttachmentDisplay>().GetAwaiter().GetResult().ToList();
                        }
                        response.NullCheck();
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<string> CheckSeenMessage(long messageId, long userId)
        {
            try
            {
                var isSeen = "0";
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                       SELECT 
                            M.Id
                       FROM Notification.Message M
                       join Notification.MessageSeenStatistics MS on M.Id = MS.MessageId 
                       join Organization.Staff S on Ms.StaffId = S.Id
                       join Authentication.Profile P on s.ProfileId = P.Id 
                       join Authentication.Users U on U.ProfileID = P.Id 
                       WHERE M.ActiveStatusId <> 3 and M.Id = @MessageId and U.Id = @userId
                        ";
                    var result = await connection.QueryFirstOrDefaultAsync<long>(query, new { MessageId = messageId,  userId });

                    if (result > 0)
                        isSeen = "1";

                    return isSeen;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
