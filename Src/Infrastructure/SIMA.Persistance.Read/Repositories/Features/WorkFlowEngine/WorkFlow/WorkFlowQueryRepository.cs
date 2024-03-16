using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;

public class WorkFlowQueryRepository : IWorkFlowQueryRepository
{
    private readonly string _connectionString;
    public WorkFlowQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetWorkFlowQueryResult>>> GetAll(GetAllWorkFlowsQuery request)
    {
        var response = new List<GetWorkFlowQueryResult>();
        int totalCount = 0;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"
                Select Count(*) Result
                FROM [PROJECT].[WorkFlow] C
                    join [PROJECT].Project P on C.ProjectID = P.Id
					INNER JOIN [Basic].[ActiveStatus] A on C.ActiveStatusID = A.ID
					INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                WHERE C.[ActiveStatusID] <> 3
                and (@SearchValue is null OR (C.Name like @SearchValue or C.Code like @SearchValue or C.[Description] like @SearchValue or P.Name like @SearchValue or D.[Name] like @SearchValue or A.[Name] like @SearchValue))
";

            string query = $@"
                  SELECT  C.[ID] as Id
                  ,C.[Name]
                  ,C.[Code]
                  ,C.[ProjectId]
                  ,C.[ManagerRoleId]
                  ,C.[Description]
                  ,C.[Ordering]
                  ,C.[FileContent]
                  ,C.[BpmnId]   
                  ,P.[Name]  ProjectName
				  ,A.[Name] ActiveStatus
				  ,D.Id DomainId
				  ,D.[Name] DomainName
                  ,c.[CreatedAt]
                    FROM [PROJECT].[WorkFlow] C
                    join [PROJECT].Project P on C.ProjectID = P.Id
					INNER JOIN [Basic].[ActiveStatus] A on C.ActiveStatusID = A.ID
					INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                WHERE C.[ActiveStatusID] <> 3
                and (@SearchValue is null OR (C.Name like @SearchValue or C.Code like @SearchValue or C.[Description] like @SearchValue or P.Name like @SearchValue or D.[Name] like @SearchValue or A.[Name] like @SearchValue))
                  order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                  OFFSET @Skip rows FETCH NEXT @Take rows only;";

            using (var result = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = "%" + request.Filter + "%",
                Take = request.PageSize,
                request.Skip,
            }))
            {
                response = (await result.ReadAsync<GetWorkFlowQueryResult>()).ToList();
                totalCount = await result.ReadSingleAsync<int>();
            }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }
    public async Task<GetWorkFlowQueryResult> FindById(long id)
    {
        var response = new GetWorkFlowQueryResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                 SELECT DISTINCT C.[ID] as Id
                  ,C.[Name]
                  ,C.[Code]
                  ,C.[ProjectId]
                  ,C.[ManagerRoleId]
                  ,C.[Description]
                  ,C.[Ordering]
                  ,C.[FileContent]
                  ,C.[BpmnId]   
                  ,P.[Name]  ProjectName
				  ,A.[Name] ActiveStatus
				  ,D.Id DomainId
				  ,D.[Name] DomainName
                    FROM [PROJECT].[WorkFlow] C
                    join [PROJECT].Project P on C.ProjectID = P.Id
					INNER JOIN [Basic].[ActiveStatus] A on C.ActiveStatusID = A.ID
					INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
              WHERE C.[ActiveStatusID] <> 3 and C.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetWorkFlowQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.WorkflowNotFoundError;
            response = result;
        }
        return response;
    }
    public async Task<Result<IEnumerable<GetStepQueryResult>>> GetAllStep(GetAllStepsQuery request)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {

            var queryCount = @"
                         SELECT COUNT(*) Result
                              FROM [PROJECT].[STEP] C
                              left join [PROJECT].[State] S on C.StateID = S.Id
                              join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
                        	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
                        	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                        	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
                          WHERE  C.ActiveStatusId != 3 and C.[ActionTypeId] != 6
                              and (@SearchValue is null OR C.[Name] like @SearchValue)
                              AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId);";

            await connection.OpenAsync();
            string query = $@"
                          SELECT DISTINCT C.[ID] as Id
                                   ,C.[Name]
                                   ,C.[workFlowId]
                                   ,C.[stateId]
                                   ,S.[Name] as StateName
                                   ,C.[BpmnId]
                                   ,C.[ActionTypeId]
                                   ,W.[Name] as WorkFlowName
                        		   ,A.[Name] ActiveStatus
                        		   ,P.[Name] ProjectName
                        		   ,P.Id ProjectId
                        		   ,D.[Name] DomainName
                        		   ,D.[Id] DomainId
                        		   ,c.[CreatedAt]
                               FROM [PROJECT].[STEP] C
                               left join [PROJECT].[State] S on C.StateID = S.Id
                              join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
                        	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
                        	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                        	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
                         WHERE  C.ActiveStatusId != 3 and C.[ActionTypeId] != 6
                                and (@SearchValue is null OR C.[Name] like @SearchValue)
                               AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
                               order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                        OFFSET @Skip rows FETCH NEXT @Take rows only";


            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {

                SearchValue = "%" + request.Filter + "%",
                WorkFlowId = request.WorkFlowId,
                ProjectId = request.ProjectId,
                DomainId = request.DomainId,
                Take = request.PageSize,
                request.Skip,
            }))
            {
                var response = await multi.ReadAsync<GetStepQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }

    }
    public async Task<List<GetStepQueryResult>> GetAllStepByWorkFlowId(long id)
    {
        var response = new List<GetStepQueryResult>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
  SELECT DISTINCT C.[ID] as Id
           ,C.[Name]
           ,C.[workFlowId]
           ,C.[stateId]
           ,C.[BpmnId]
           ,C.[ActionTypeId]
         	 ,S.[Id] as StateId
         	 ,S.[Name] as StateName
           ,W.[Name] as WorkFlowName
		   ,A.[Name] ActiveStatus
,P.[Name] ProjectName
,P.Id ProjectId
,D.[Name] DomainName
,D.[Id] DomainId
,c.[CreatedAt]
       FROM  [PROJECT].[STEP] C
       left join  [PROJECT].[State] S on C.StateID = S.Id
      join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
WHERE C.[ActiveStatusID] <> 3   and W.Id=@id             
Order By c.[CreatedAt] desc
";
            var result = await connection.QueryAsync<GetStepQueryResult>(query, new { id = id });
            response = result.ToList();
        }

        return response;
    }
    public async Task<GetStepQueryResult> GetStepById(long id)
    {
        var response = new GetStepQueryResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                       SELECT DISTINCT C.[ID] as Id
           ,C.[Name]
           ,C.[workFlowId]
           ,C.[stateId]
           ,C.[BpmnId]
           ,C.[ActionTypeId]
         	 ,S.[Id] as StateId
         	 ,S.[Name] as StateName
           ,W.[Name] as WorkFlowName
		   ,A.[Name] ActiveStatus
,P.[Name] ProjectName
,P.Id ProjectId
,D.[Name] DomainName
,D.[Id] DomainId
       FROM [PROJECT].[STEP] C
       left join [PROJECT].[State] S on C.StateID = S.Id
      join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
WHERE C.[ActiveStatusID] <> 3 and C.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetStepQueryResult>(query, new { Id = id });
            response = result;
        }
        return response;

    }
    public async Task<Result<IEnumerable<GetStateQueryResult>>> GetAllStates(GetAllStatesQuery request)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {

            string queryCount = @"SELECT COUNT(*) Result 
	                        FROM [PROJECT].[STATE] C
	                        join [PROJECT].[WorkFlow] W on C.WorkFlowID = W.Id
	                        join Project.Project P on P.Id=W.ProjectID
	                        join Authentication.Domain D on D.Id=p.DomainID
	                        join Basic.ActiveStatus A on A.Id = c.ActiveStatusID
                            WHERE  C.ActiveStatusId != 3
                                and (@SearchValue is null OR C.[Name] like @SearchValue)
                               AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)";

            await connection.OpenAsync();
            string query = $@"
                               SELECT DISTINCT C.[ID] as Id
                            		 ,C.[Name]
                            		 ,C.[Code]
                            		 ,C.[workFlowId]
                            		 ,W.[Name] as WorkFlowName
                            		 ,D.Id DomainID
                            		 ,D.Name DomainName
                            		 ,P.Id ProjectId
                            		 ,P.Name ProjectName
                            		 ,A.ID ActiveStatusId
                            		 ,A.Name ActiveStatus 
                            		 ,c.[CreatedAt]
                            	FROM [PROJECT].[STATE] C
                            	join [PROJECT].[WorkFlow] W on C.WorkFlowID = W.Id
                            	join Project.Project P on P.Id=W.ProjectID
                            	join Authentication.Domain D on D.Id=p.DomainID
                            	join Basic.ActiveStatus A on A.Id = c.ActiveStatusID
                               WHERE  C.ActiveStatusId != 3
                                   and (@SearchValue is null OR C.[Name] like @SearchValue)
                                  AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
                                  order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                               OFFSET @Skip rows FETCH NEXT @Take rows only";


            using (var multi = await connection.QueryMultipleAsync(query + " " + queryCount, new
            {

                SearchValue = "%" + request.Filter + "%",
                WorkFlowId = request.WorkFlowId,
                ProjectId = request.ProjectId,
                DomainId = request.DomainId,
                Take = request.PageSize,
                request.Skip,
            }))
            {
                var response = await multi.ReadAsync<GetStateQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
    public async Task<List<GetStateQueryResult>> GetAllStatesByWorkFlowId(long id)
    {
        var response = new List<GetStateQueryResult>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                   SELECT DISTINCT C.[ID] as Id
                     ,C.[Name]
                     ,C.[Code]
                     ,C.[workFlowId]
                     ,W.[Name] as WorkFlowName
                     ,P.Name ProjectName
	                 ,D.Name DomainName
                     ,A.ID ActiveStatusId
                     ,A.Name ActiveStatus
,c.[CreatedAt]
                 FROM [PROJECT].[STATE] C
                 join [PROJECT].[WorkFlow] W on C.WorkFlowID = W.Id
                 join Project.Project P on P.Id=W.ProjectID
                 join Authentication.Domain D on D.Id=p.DomainID
                 join Basic.ActiveStatus A on A.Id = c.ActiveStatusID
                    WHERE C.[ActiveStatusID] <> 3  and W.Id = @workFlowId 
Order By c.[CreatedAt] desc";
            var result = await connection.QueryAsync<GetStateQueryResult>(query, new { workFlowId = id });
            if (result is null) throw SimaResultException.StateNotFoundError;
            response = result.ToList();
        }
        return response;
    }
    public async Task<GetStateQueryResult> GetStateById(long stateId)
    {
        var response = new GetStateQueryResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                   SELECT DISTINCT C.[ID] as Id
                     ,C.[Name]
                     ,C.[Code]
                     ,C.[workFlowId]
                     ,W.[Name] as WorkFlowName
                     ,P.Name ProjectName
	                 ,D.Name DomainName
                     ,A.ID ActiveStatusId
                     ,A.Name ActiveStatus
                 FROM [PROJECT].[STATE] C
                 join [PROJECT].[WorkFlow] W on C.WorkFlowID = W.Id
                 join Project.Project P on P.Id=W.ProjectID
                 join Authentication.Domain D on D.Id=p.DomainID
                 join Basic.ActiveStatus A on A.Id = c.ActiveStatusID
                 WHERE C.[ActiveStatusID] <> 3 and C.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetStateQueryResult>(query, new { Id = stateId });
            if (result is null) throw SimaResultException.StateNotFoundError;
            response = result;
        }
        return response;
    }

    public async Task<List<GetWorkFlowQueryResult>> GetByProjectId(long projectId)
    {
        var response = new List<GetWorkFlowQueryResult>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                  SELECT DISTINCT C.[ID] as Id
                  ,C.[Name]
                  ,C.[Code]
                  ,C.[ProjectId]
                  ,C.[ManagerRoleId]
                  ,C.[Description]
                  ,C.[Ordering]
                  ,C.[FileContent]
                  ,C.[BpmnId]   
                  ,P.[Name] as ProjectName
,c.[CreatedAt]
                    FROM [PROJECT].[WorkFlow] C
                    join [PROJECT].Project P on C.ProjectID = P.Id
              WHERE C.[ActiveStatusID] != 3 and C.[ProjectId] = @ProjectId
Order By c.[CreatedAt] desc
";
            var result = await connection.QueryAsync<GetWorkFlowQueryResult>(query, new { ProjectId = projectId });
            if (result is null) throw SimaResultException.WorkflowNotFoundError;
            response = result.ToList();
        }
        return response;
    }
}
