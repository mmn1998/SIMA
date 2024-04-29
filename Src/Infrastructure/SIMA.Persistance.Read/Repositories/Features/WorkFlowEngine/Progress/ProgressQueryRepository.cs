using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Progress
{
    public class ProgressQueryRepository : IProgressQueryRepository
    {
        private readonly string _connectionString;

        public ProgressQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();

        }
        public async Task<GetProgressQueryResult> FindById(long id)
        {
            var response = new GetProgressQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
           SELECT DISTINCT 
                                      P.[Id]
                                     ,P.[StateID] StateId
                                     ,S.[Name] StatusName
                                     ,P.[Name]
                                     ,P.[ActiveStatusID]
                                     ,A.[Name] ActiveStatus
                                     ,p.[CreatedAt]
                            		 ,P.[workFlowId]
                            		 ,W.[Name] as WorkFlowName
                            		 ,D.Id DomainID
                                     ,D.Name DomainName
                            		 ,Pro.Id ProjectId
                                     ,Pro.Name ProjectName
                             FROM [Project].[Progress] P
                             INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                             left JOIN [Project].[State] S on P.StateId = s.Id         
                             join [PROJECT].[WorkFlow] W on P.WorkFlowID = W.Id
                             join Project.Project Pro on Pro.Id=W.ProjectID
                             join Authentication.Domain D on D.Id=Pro.DomainID
                             WHERE  P.ActiveStatusId != 3 and W.ActiveStatusId != 3 and P.Id = @Id";
                var result = await connection.QueryFirstOrDefaultAsync<GetProgressQueryResult>(query, new { Id = id });
                response = result;
            }
            return response;
        }

        public async Task<Result<IEnumerable<GetProgressQueryResult>>> GetAll(GetAllProgressQuery request)
        {


            using (var connection = new SqlConnection(_connectionString))
            {
                string queryCount = @"
                             SELECT DISTINCT 
                             Count(*) Result
                             FROM [Project].[Progress] P
                             INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                             left JOIN [Project].[State] S on P.StateId = s.Id         
                             join [PROJECT].[WorkFlow] W on P.WorkFlowID = W.Id
                             join Project.Project Pro on Pro.Id=W.ProjectID
                             join Authentication.Domain D on D.Id=Pro.DomainID
                             WHERE  P.ActiveStatusId != 3 and W.ActiveStatusId != 3
                                and (@SearchValue is null OR P.[Name] like @SearchValue)
                             AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR Pro.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)";

                await connection.OpenAsync();
                string query = $@"
            
                            SELECT DISTINCT 
                                      P.[Id]
                                     ,P.[StateID] StateId
                                     ,S.[Name] StatusName
                                     ,P.[Name]
                                     ,P.[ActiveStatusID]
                                     ,A.[Name] ActiveStatus
                                     ,p.[CreatedAt]
                            		 ,P.[workFlowId]
                            		 ,W.[Name] as WorkFlowName
                            		 ,D.Id DomainID
                                     ,D.Name DomainName
                            		 ,Pro.Id ProjectId
                                     ,Pro.Name ProjectName
                             FROM [Project].[Progress] P
                             INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                             left JOIN [Project].[State] S on P.StateId = s.Id         
                             join [PROJECT].[WorkFlow] W on P.WorkFlowID = W.Id
                             join Project.Project Pro on Pro.Id=W.ProjectID
                             join Authentication.Domain D on D.Id=Pro.DomainID
                             WHERE  P.ActiveStatusId != 3 and W.ActiveStatusId != 3 
                             and (@SearchValue is null OR P.[Name] like @SearchValue)
                             AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR Pro.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
                             order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                             OFFSET @Skip rows FETCH NEXT @Take rows only; 
                                                            ";
               
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    Take = request.PageSize,
                    request.Skip,
                    WorkFlowId = request.WorkFlowId,
                    ProjectId = request.ProjectId,
                    DomainId = request.DomainId,
                }))
                {
                    var response = await multi.ReadAsync<GetProgressQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }

            }
        }
    }
}
