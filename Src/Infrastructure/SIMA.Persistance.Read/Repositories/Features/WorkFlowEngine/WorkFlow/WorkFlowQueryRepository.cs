using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.RestfulClient;
using SIMA.Resources;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using static Dapper.SqlMapper;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;

public class WorkFlowQueryRepository : IWorkFlowQueryRepository
{
    private readonly string _connectionString;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IRestfulClient _restfulClient;
    public WorkFlowQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity, IRestfulClient restfulClient)
    {
        _connectionString = configuration.GetConnectionString();
        _simaIdentity = simaIdentity;
        _restfulClient = restfulClient;

    }
    public async Task<Result<IEnumerable<GetWorkFlowQueryResult>>> GetAll(GetAllWorkFlowsQuery request)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"WITH Query as(
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
  ,M.[Id] MainAggregateId
  ,M.[Name] MainAggregateName
    FROM [PROJECT].[WorkFlow] C
    join [PROJECT].Project P on C.ProjectID = P.Id
				INNER JOIN [Basic].[ActiveStatus] A on C.ActiveStatusID = A.ID
				INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
    INNER JOIN [Authentication].[MainAggregate] M on C.MainAggregateId  = M.Id
WHERE C.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

            string query = $@"WITH Query as(
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
  ,M.[Id] MainAggregateId
  ,M.[Name] MainAggregateName
    FROM [PROJECT].[WorkFlow] C
    join [PROJECT].Project P on C.ProjectID = P.Id
				INNER JOIN [Basic].[ActiveStatus] A on C.ActiveStatusID = A.ID
				INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
    INNER JOIN [Authentication].[MainAggregate] M on C.MainAggregateId  = M.Id
WHERE C.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetWorkFlowQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
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
                  ,M.[Id] MainAggregateId
	              ,M.[Name] MainAggregateName
                    FROM [PROJECT].[WorkFlow] C
                    join [PROJECT].Project P on C.ProjectID = P.Id
					INNER JOIN [Basic].[ActiveStatus] A on C.ActiveStatusID = A.ID
					INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                    INNER JOIN [Authentication].[MainAggregate] M on C.MainAggregateId  = M.Id
              WHERE C.[ActiveStatusID] <> 3 and C.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetWorkFlowQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException(CodeMessges._100057Code, Messages.WorkflowNotFoundError);
            response = result;
        }
        return response;
    }
    public async Task<Result<IEnumerable<GetStepQueryResult>>> GetAllStep(GetAllStepsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
						  SELECT DISTINCT C.[ID] as Id
          ,C.[Name]
          ,C.[CompleteName]
          ,C.[workFlowId]
          ,C.[BpmnId]
          ,C.[ActionTypeId]
          ,W.[Name] as WorkFlowName
     		   ,A.[Name] ActiveStatus
     		   ,P.[Name] ProjectName
     		   ,P.Id ProjectId
     		   ,D.[Name] DomainName
     		   ,D.[Id] DomainId
     		   ,c.[CreatedAt]
     		   ,c.[FormId] 
     		   ,F.[Title] FormName
      FROM [PROJECT].[STEP] C
     join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
  	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
  	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
  	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
     Left JOIN [Authentication].[Form] F on C.FormId = F.Id
WHERE  C.ActiveStatusId != 3 and C.[ActionTypeId] != 6
      AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

            string query = $@"WITH Query as(
							 SELECT DISTINCT C.[ID] as Id
          ,C.[Name]
          ,C.[CompleteName]
          ,C.[workFlowId]
          ,C.[BpmnId]
          ,C.[ActionTypeId]
          ,W.[Name] as WorkFlowName
     		   ,A.[Name] ActiveStatus
     		   ,P.[Name] ProjectName
     		   ,P.Id ProjectId
     		   ,D.[Name] DomainName
     		   ,D.[Id] DomainId
     		   ,c.[CreatedAt]
     		   ,c.[FormId] 
     		   ,F.[Title] FormName
      FROM [PROJECT].[STEP] C
     join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
  	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
  	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
  	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
     Left JOIN [Authentication].[Form] F on C.FormId = F.Id
WHERE  C.ActiveStatusId != 3 and C.[ActionTypeId] != 6
      AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            dynaimcParameters.Item2.Add("WorkFlowId", request.WorkFlowId);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            dynaimcParameters.Item2.Add("ProjectId", request.ProjectId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetStepQueryResult>();
                return Result.Ok(response, request, count);
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
                                   ,C.[CompleteName]
                                   ,C.[workFlowId]
                                   ,C.[BpmnId]
                                   ,C.[ActionTypeId]
                                   ,W.[Name] as WorkFlowName
                        		   ,A.[Name] ActiveStatus
                                   ,P.[Name] ProjectName
                                   ,P.Id ProjectId
                                   ,D.[Name] DomainName
                                   ,D.[Id] DomainId
                                   ,c.[CreatedAt]
                                   ,c.[FormId] 
                        		   ,F.[Title] FormName
                              FROM  [PROJECT].[STEP] C
                              join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
                        	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
                        	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                        	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
                              Left JOIN [Authentication].[Form] F on C.FormId = F.Id
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
                             ,C.[CompleteName]
                             ,C.[workFlowId]
                             ,C.[BpmnId]
                             ,C.[DisplayName]
                             ,C.[ActionTypeId]
                             ,W.[Name] as WorkFlowName
                             ,A.[Name] ActiveStatus
                             ,P.[Name] ProjectName
                             ,P.Id ProjectId
                             ,D.[Name] DomainName
                             ,D.[Id] DomainId
                             ,c.[FormId] 
                             ,F.[Title] FormName
	                         FROM [PROJECT].[STEP] C
                            join [PROJECT].[WorkFlow] W on C.WorkFlowId = W.Id
                            INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
                            INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
                            Left JOIN [Authentication].[Form] F on C.FormId = F.Id
	                        WHERE C.[ActiveStatusID] <> 3 and C.Id = @Id

	                         SELECT DISTINCT 
  	                         ap.Id ApprovalOptionId
	                         ,ap.Name ApprovalOption

                            FROM [PROJECT].[STEP] C
	                        left join Project.StepApprovalOption sap on sap.StepId = c.Id
	                        left join Project.ApprovalOption ap on ap.Id=sap.ApprovalOptionId
	                        WHERE C.[ActiveStatusID] <> 3 and  sap.[ActiveStatusID] <> 3  and C.Id = @Id
	                         SELECT DISTINCT 
  	                          srd.Count Count
	                         ,srd.DocumentTypeId DocumentTypeId,
	                         dt.Name DocumentType
                            FROM [PROJECT].[STEP] C
	                        left join Project.StepRequiredDocument srd  on srd.StepId = c.Id
	                        left join DMS.DocumentType dt on dt.Id=srd.DocumentTypeId
                            WHERE C.[ActiveStatusID] <> 3 and  srd.[ActiveStatusID] <> 3 and C.Id = @Id
                                   ";

            using (var multi = await connection.QueryMultipleAsync(query, new { id = id }))
            {
                response = multi.ReadAsync<GetStepQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                response.ApprovalOptions = multi.ReadAsync<StepApprovalOptionQueryResult>().GetAwaiter().GetResult().ToList();
                response.RequiredDocuments = multi.ReadAsync<RequiredDocumentQueryResult>().GetAwaiter().GetResult().ToList();
            }
        }
        return response;
    }
    public async Task<Result<IEnumerable<GetStateQueryResult>>> GetAllStates(GetAllStatesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"WITH Query as(
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
   AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								
								 ;";

            string query = $@"WITH Query as(
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
   AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            dynaimcParameters.Item2.Add("WorkFlowId", request.WorkFlowId);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            dynaimcParameters.Item2.Add("ProjectId", request.ProjectId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetStateQueryResult>();
                return Result.Ok(response, request, count);
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
            if (result is null) throw new SimaResultException(CodeMessges._100058Code, Messages.StateNotFoundError);
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
            if (result is null) throw new SimaResultException(CodeMessges._100058Code, Messages.StateNotFoundError);
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
            if (result is null) throw new SimaResultException(CodeMessges._100057Code, Messages.WorkflowNotFoundError);
            response = result.ToList();
        }
        return response;
    }
    public async Task<IEnumerable<GetWorkFlowQueryResult>> GetAllWorkFlowForIssue()
    {
        var userid = _simaIdentity.UserId;
        var groups = _simaIdentity.GroupId;
        var roles = _simaIdentity.RoleIds;

        var queryString = @"
              select 
                   W.[ID] as Id
                  ,W.[Name]
                  ,W.[Code]
               from Project.WorkFlow w 
               join Project.WorkFlowActor a on a.WorkFlowId=w.Id
               join Project.Step s on s.WorkFlowID=w.Id
               left join Project.WorkFlowActorUser u on u.WorkFlowActorId=a.Id
               left join Project.WorkFlowActorRole r on r.WorkFlowActorID = a.Id
               left join Project.WorkFlowActorGroup g on g.WorkFlowActorID = a.Id
               where w.ActiveStatusId != 3 and s.ActionTypeId=9 and (u.UserID = @userid or r.RoleID in @roles or g.GroupID in @groups);"
        ;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<GetWorkFlowQueryResult>(queryString, new { userid, roles, groups });
            return result;
        }
    }
    public async Task<bool> AllowAddApprovalForStep(long stepId)
    {
        bool result = false;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                   select
                        s.Name as StepName , S.Id as StepId
                        ,Ps.Name as ProgressSourceName , Ps.Id as PSourceId , PS.SourceId PSSourceId , Ps.TargetId PSTargetId ,PS.ConditionExpression ,PS.HasStoreProcedure,
                        ST.Name as StepNameTarget , ST.Id as StepIdTarget
                        ,PT.Name as ProgressTargetName , PT.Id as PTourceId , PT.SourceId PTSourceId , PT.TargetId PTTargetId ,Pt.ConditionExpression ,Pt.HasStoreProcedure
                        from Project.Step S
                        join Project.Progress PS on S.Id = PS.SourceId
                        join Project.Step ST on PS.TargetId = ST.Id
                        join Project.Progress PT on ST.Id = PT.SourceId
                    where (s.Id  =@StepId) and ST.ActionTypeId = 6 And( PS.HasStoreProcedure is not null or PT.ConditionExpression is not null)";
            var response = await connection.QueryFirstOrDefaultAsync<GetStateQueryResult>(query, new { StepId = stepId });
            if (response is not null) return true;

        }
        return result;
    }

    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetNextStepById(long workflowId, GetNextStepQuery query, List<InputParamServiceQuery>? inputParamServices)
    {
        var model = new Dictionary<long, Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc.NextProgressInfo>();
        // var spParams = new Dictionary<long, StoreProcedureParams>();

        var getNextStepQuery = @"select distinct wp.StateId CurrentProgressStateId, 
								 wp.ProgressId CurrentProgressId, 
								 s.Id StepId, 
								 s.WorkFlowID WorkflowId, 
								 s.ActionTypeId, 
								 p.TargetId, 
								 p.ConditionExpression, 
								 p.HasStoreProcedure, 
								 p.StateId NextStateId, 
								 p.Extension,
                                 p.Id ProgressId,
								 ST.Address URLAddress,
								 ST.ApiMethodActionId,
								 AM.Name APIMethodName,
								 IP.InputName InputServiceName,
								 IP.DataTypeId,
								 DT.Name DataTypeName
								 from project.Step s
                                  inner join project.WorkFlow w on w.Id = s.WorkFlowID
                                  left join Project.Progress p on p.SourceId = s.Id
                                  left join (SELECT iwp.Id ProgressId, iwp.StateId, iwp.WorkFlowId from Project.Progress iwp where iwp.Id = @ProgressId) wp on wp.WorkFlowId = w.Id
								  left join Project.StepServiceTask SST on SST.StepId = S.Id
								  left join Project.ServiceTask ST on SST.ServiceTaskId  = ST.Id
								  LEFT join Basic.ApiMethodAction AM on AM.Id = ST.ApiMethodActionId
								  left join Project.ServiceInputParam SIP on SIP.ServiceTaskId = ST.Id
								  left join Project.InputParam IP on IP.Id = SIP.InputParamId
								  left join Basic.DataType DT on DT.Id = IP.DataTypeId
				                  where w.Id = @WorkflowId and s.Id = @NextStepId and w.ActiveStatusID = 1";
        var result = new GetNextStepInfoQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            var queryResult = await connection.QueryAsync<dynamic>(getNextStepQuery, new { WorkflowId = workflowId, NextStepId = query.NextStepId, ProgressId = query.ProgressId });
            foreach (var current in queryResult)
            {
                NextProgressInfo item;
                if (current.TargetId != null)
                {
                    if (!model.TryGetValue(current.TargetId, out item))
                    {
                        item = new NextProgressInfo { WorkflowId = workflowId, ProgressId = current.ProgressId, ConditionExpression = current.ConditionExpression, Extension = current.Extension, NextStateId = current.NextStateId, TargetId = current.TargetId, SpName = current.SpName };
                        model.Add(current.TargetId, item);
                    }
                }
                result.CurrentProgressId = current.CurrentProgressId;
                result.CurrentProgressStateId = current.CurrentProgressStateId;

                result.StepId = current.StepId;
                result.WorkflowId = current.WorkflowId;
                result.ActionTypeId = current.ActionTypeId;

                result.URLAddress = current.URLAddress;
                result.ApiMethodActionId = current.ApiMethodActionId;
                result.APIMethodName = current.APIMethodName;
                result.InputServiceName = current.InputServiceName;
                result.DataTypeId = current.DataTypeId;
                result.DataTypeName = current.DataTypeName;

            }
            result.NextProgressInfo = model.Values.Any() ? model.Values.ToList() : null;
            if (result.ActionTypeId == 6)
            {
                return await EvaluateNextProgress(result.NextProgressInfo, query.ConditionValue, query.SystemParams);
            }

            if (result.ActionTypeId == 3)
            {
                switch (result.ApiMethodActionId)
                {
                    case 1:

                        break;
                    case 2:
                        var messageList = new List<string>();
                        var mess = string.Empty;
                        var parameterDictionary = inputParamServices.ToDictionary(p => p.ParamKey, p => p.ParamName);
                        var message = JsonConvert.SerializeObject(parameterDictionary);

                        var response = await PostAsync<string, string>(result.URLAddress, message, null);

                        break;

                    default:
                        break;
                }
            }

            return new GetWorkflowInfoByIdResponseQueryResult { SourceStepId = result.StepId, SourceStateId = result.CurrentProgressStateId, ActionTypeId = result.ActionTypeId };

        }
    }
    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetWorkflowInfoByIdAsync(long workFlowId)
    {
        var result = new GetWorkflowInfoByIdResponseQueryResult();
        var nextStep = new StepInfo();

        using (var connection = new SqlConnection(_connectionString))
        {
            // Load workflow with related entities
            var sql = @"
           SELECT * FROM Project.WorkFlow W WHERE W.Id = @WorkFlowId AND W.ActiveStatusId <> 3 ;
            SELECT * FROM Project.State ST WHERE ST.WorkFlowId = @WorkFlowId;
            SELECT * FROM Project.Step S WHERE S.WorkFlowId = @WorkFlowId;
            SELECT * FROM Project.Progress P WHERE P.SourceId IN (SELECT Id FROM Project.Step SS WHERE  SS.WorkFlowId = @WorkFlowId);
            SELECT * FROM Project.WorkFlowActor A WHERE A.WorkFlowId = @WorkFlowId;
            SELECT * FROM Project.WorkFlowActorStep WAS WHERE WAS.WorkFlowActorId IN (SELECT Id FROM Project.WorkFlowActor WHERE WorkFlowId = @WorkFlowId);
            SELECT * FROM Project.WorkFlowActorUser WAU WHERE WorkFlowActorId IN (SELECT Id FROM Project.WorkFlowActor WHERE WorkFlowId = @WorkFlowId);
        ";

            var workFlow = new WorkflowInfoQueryResult();
            using (var multi = await connection.QueryMultipleAsync(sql, new { WorkFlowId = workFlowId }))
            {
                workFlow = multi.Read<WorkflowInfoQueryResult>().SingleOrDefault();
                if (workFlow == null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);

                workFlow.States = multi.Read<StateInfo>().ToList();
                workFlow.Steps = multi.Read<StepInfo>().ToList();
                var sourceProgresses = multi.Read<SourceProgressInfo>().ToList();
                workFlow.WorkFlowActors = multi.Read<WorkFlowActorInfo>().ToList();
                var actorSteps = multi.Read<WorkFlowActorStepInfo>().ToList();
                var actorUsers = multi.Read<WorkFlowActorUserInfo>().ToList();

                foreach (var step in workFlow.Steps)
                {
                    step.SourceProgresses = sourceProgresses.Where(x => x.SourceId == step.Id).ToList();
                }

                foreach (var actor in workFlow.WorkFlowActors)
                {
                    actor.WorkFlowActorSteps = actorSteps.Where(x => x.WorkFlowActorId == actor.Id).ToList();
                    actor.WorkFlowActorUsers = actorUsers.Where(x => x.WorkFlowActorId == actor.Id).ToList();
                }
            }

            var startEventActionTypeId = (long)ActionTypeEnum.startEvent;
            var steps = workFlow.Steps.Where(x => x.ActionTypeId == startEventActionTypeId).ToList();

            if (steps.Count >= 1)
            {
                var checkIsEveryOne = new GetWorkflowInfoByIdResponseQueryResult();
                var checkActorAccess = new GetWorkflowInfoByIdResponseQueryResult();
                foreach (var actor in workFlow.WorkFlowActors)
                {
                    var checkUserInActor = workFlow.WorkFlowActors.Select(x => x.WorkFlowActorUsers.Select(c => c.UserId).Contains(_simaIdentity.UserId));

                    var actorUsers = actor.WorkFlowActorUsers.FirstOrDefault(x => x.UserId == _simaIdentity.UserId && x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
                    if (actorUsers != null)
                    {
                        var actorStepIds = actor.WorkFlowActorSteps.Select(x => x.StepId);
                        var step = workFlow.Steps.FirstOrDefault(s => actorStepIds.Contains(s.Id) && s.ActionTypeId == startEventActionTypeId);
                        if (step != null)
                        {
                            var progress = step.SourceProgresses.FirstOrDefault(x => x.SourceId == step.Id);
                            if (progress != null)
                            {

                                var currentStep = workFlow.Steps.FirstOrDefault(x => x.Id == progress.TargetId);
                                var currentProgress = currentStep.SourceProgresses.FirstOrDefault(x => x.SourceId == currentStep.Id);
                                nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == currentProgress.TargetId);
                                if (nextStep.ActionTypeId == (long)ActionTypeEnum.exclusiveGateway)
                                {
                                    var GatewayStep = workFlow.Steps.FirstOrDefault(x => x.Id == currentProgress.TargetId);
                                    var GateWayProgress = GatewayStep.SourceProgresses.Where(x => x.SourceId == GatewayStep.Id).ToList();
                                    foreach (var item in GateWayProgress)
                                    {
                                        nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == item.TargetId && x.ActionTypeId != (long)ActionTypeEnum.serviceTask);
                                        if (nextStep is not null) break;

                                    }
                                }

                                if (nextStep.ActionTypeId == (long)ActionTypeEnum.endEvent)
                                {
                                    result.Id = workFlow.Id;
                                    result.SourceStepId = step.Id;
                                    result.TargetStepId = nextStep.Id;
                                    result.ProjectId = workFlow.ProjectId;
                                    return result;
                                }

                                checkActorAccess.TargetStateId = nextStep.SourceProgresses[0].StateId;
                                checkActorAccess.SourceStateId = null;
                                checkActorAccess.TargetStepId = nextStep.SourceProgresses[0].SourceId;
                                checkActorAccess.SourceStepId = step.Id;
                                checkActorAccess.Id = workFlowId;
                                checkActorAccess.ProjectId = workFlow.ProjectId;
                                checkActorAccess.MainAggregateId = workFlow.MainAggregateId;
                                //checkActorAccess = result;
                            }
                        }
                    }
                    else if (actor.IsEveryOne == "1")
                    {
                        var actorStepIds = actor.WorkFlowActorSteps.Select(x => x.StepId);
                        var step = workFlow.Steps.FirstOrDefault(s => actorStepIds.Contains(s.Id) && s.ActionTypeId == startEventActionTypeId);
                        if (step != null)
                        {
                            var progress = step.SourceProgresses.FirstOrDefault(x => x.SourceId == step.Id);
                            if (progress != null)
                            {
                                var currentStep = workFlow.Steps.FirstOrDefault(x => x.Id == progress.TargetId);
                                var currentProgress = currentStep.SourceProgresses.FirstOrDefault(x => x.SourceId == currentStep.Id);
                                nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == currentProgress.TargetId);
                                if (nextStep.ActionTypeId == (long)ActionTypeEnum.exclusiveGateway)
                                {
                                    var GatewayStep = workFlow.Steps.FirstOrDefault(x => x.Id == currentProgress.TargetId);
                                    var GateWayProgress = GatewayStep.SourceProgresses.Where(x => x.SourceId == GatewayStep.Id).ToList();
                                    foreach (var item in GateWayProgress)
                                    {
                                        nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == item.TargetId && x.ActionTypeId != (long)ActionTypeEnum.serviceTask);
                                        if (nextStep is not null) break;

                                    }
                                }

                                checkIsEveryOne.TargetStateId = nextStep.SourceProgresses[0].StateId;
                                checkIsEveryOne.SourceStateId = null;
                                checkIsEveryOne.TargetStepId = nextStep.SourceProgresses[0].SourceId;
                                checkIsEveryOne.SourceStepId = step.Id;
                                checkIsEveryOne.Id = workFlowId;
                                checkIsEveryOne.ProjectId = workFlow.ProjectId;
                                checkIsEveryOne.MainAggregateId = workFlow.MainAggregateId;
                                //checkIsEveryOne = result;
                            }
                        }
                    }
                }

                if (checkActorAccess.Id != 0) return checkActorAccess;
                else if (checkIsEveryOne.Id != 0) return checkIsEveryOne;
                else throw new SimaResultException(CodeMessges._100063Code, Messages.CreateIssueWithChechActorException);

            }
            else
            {
                var step = workFlow.Steps.FirstOrDefault(s => s.ActionTypeId == startEventActionTypeId);
                if (step != null)
                {
                    var progress = step.SourceProgresses.FirstOrDefault(x => x.SourceId == step.Id);
                    if (progress != null)
                    {
                        nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == progress.TargetId);

                        result.TargetStateId = progress.StateId;
                        result.SourceStateId = null;
                        result.TargetStepId = progress.TargetId;
                        result.SourceStepId = step.Id;
                        result.Id = workFlowId;
                        result.ProjectId = workFlow.ProjectId;
                        result.MainAggregateId = workFlow.MainAggregateId;
                        return result;
                    }
                }
            }

            throw new SimaResultException(CodeMessges._400Code, Messages.IssueErrorException);
        }
    }
    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetWorkflowInfoByIdAsyncSanaz(long workFlowId)
    {
        var result = new GetWorkflowInfoByIdResponseQueryResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            // Load workflow with related entities
            var sql = @"
           SELECT * FROM Project.WorkFlow W WHERE W.Id = @WorkFlowId AND W.ActiveStatusId <> 3 ;
            SELECT * FROM Project.State ST WHERE ST.WorkFlowId = @WorkFlowId;
            SELECT * FROM Project.Step S WHERE S.WorkFlowId = @WorkFlowId;
            SELECT * FROM Project.Progress P WHERE P.SourceId IN (SELECT Id FROM Project.Step SS WHERE  SS.WorkFlowId = @WorkFlowId);
            SELECT * FROM Project.WorkFlowActor A WHERE A.WorkFlowId = @WorkFlowId;
            SELECT * FROM Project.WorkFlowActorStep WAS WHERE WAS.WorkFlowActorId IN (SELECT Id FROM Project.WorkFlowActor WHERE WorkFlowId = @WorkFlowId);
            SELECT * FROM Project.WorkFlowActorUser WAU WHERE WorkFlowActorId IN (SELECT Id FROM Project.WorkFlowActor WHERE WorkFlowId = @WorkFlowId);
        ";

            var workFlow = new WorkflowInfoQueryResult();
            using (var multi = await connection.QueryMultipleAsync(sql, new { WorkFlowId = workFlowId }))
            {
                workFlow = multi.Read<WorkflowInfoQueryResult>().SingleOrDefault();
                if (workFlow == null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);

                workFlow.States = multi.Read<StateInfo>().ToList();
                workFlow.Steps = multi.Read<StepInfo>().ToList();
                var sourceProgresses = multi.Read<SourceProgressInfo>().ToList();
                workFlow.WorkFlowActors = multi.Read<WorkFlowActorInfo>().ToList();
                var actorSteps = multi.Read<WorkFlowActorStepInfo>().ToList();
                var actorUsers = multi.Read<WorkFlowActorUserInfo>().ToList();

                foreach (var step in workFlow.Steps)
                {
                    step.SourceProgresses = sourceProgresses.Where(x => x.SourceId == step.Id).ToList();
                }

                foreach (var actor in workFlow.WorkFlowActors)
                {
                    actor.WorkFlowActorSteps = actorSteps.Where(x => x.WorkFlowActorId == actor.Id).ToList();
                    actor.WorkFlowActorUsers = actorUsers.Where(x => x.WorkFlowActorId == actor.Id).ToList();
                }
            }

            var startEventActionTypeId = (long)ActionTypeEnum.startEvent;
            var steps = workFlow.Steps.Where(x => x.ActionTypeId == startEventActionTypeId).ToList();

            if (steps.Count >= 1)
            {
                var checkIsEveryOne = new GetWorkflowInfoByIdResponseQueryResult();
                var checkActorAccess = new GetWorkflowInfoByIdResponseQueryResult();
                foreach (var actor in workFlow.WorkFlowActors)
                {
                    var checkUserInActor = workFlow.WorkFlowActors.Select(x => x.WorkFlowActorUsers.Select(c => c.UserId).Contains(_simaIdentity.UserId));

                    var actorUsers = actor.WorkFlowActorUsers.FirstOrDefault(x => x.UserId == _simaIdentity.UserId && x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
                    if (actorUsers != null)
                    {
                        var actorStepIds = actor.WorkFlowActorSteps.Select(x => x.StepId);
                        var step = workFlow.Steps.FirstOrDefault(s => actorStepIds.Contains(s.Id) && s.ActionTypeId == startEventActionTypeId);
                        if (step != null)
                        {
                            var progress = step.SourceProgresses.FirstOrDefault(x => x.SourceId == step.Id);
                            if (progress != null)
                            {
                                var currentStep = workFlow.Steps.FirstOrDefault(x => x.Id == progress.TargetId);
                                var currentProgress = currentStep.SourceProgresses.FirstOrDefault(x => x.SourceId == currentStep.Id);
                                var nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == currentProgress.TargetId);
                                if (nextStep.ActionTypeId == (long)ActionTypeEnum.exclusiveGateway)
                                {
                                    var x = nextStep.SourceProgresses[0];
                                    nextStep = workFlow.Steps.FirstOrDefault(i => i.Id == x.TargetId);
                                }

                                checkActorAccess.TargetStateId = nextStep.SourceProgresses[0].StateId;
                                checkActorAccess.SourceStateId = null;
                                checkActorAccess.TargetStepId = nextStep.SourceProgresses[0].SourceId;
                                checkActorAccess.SourceStepId = step.Id;
                                checkActorAccess.Id = workFlowId;
                                checkActorAccess.ProjectId = workFlow.ProjectId;
                                checkActorAccess.MainAggregateId = workFlow.MainAggregateId;
                                //checkActorAccess = result;
                            }
                        }
                    }
                    else if (actor.IsEveryOne == "1")
                    {
                        var actorStepIds = actor.WorkFlowActorSteps.Select(x => x.StepId);
                        var step = workFlow.Steps.FirstOrDefault(s => actorStepIds.Contains(s.Id) && s.ActionTypeId == startEventActionTypeId);
                        if (step != null)
                        {
                            var progress = step.SourceProgresses.FirstOrDefault(x => x.SourceId == step.Id);
                            if (progress != null)
                            {
                                var currentStep = workFlow.Steps.FirstOrDefault(x => x.Id == progress.TargetId);
                                var currentProgress = currentStep.SourceProgresses.FirstOrDefault(x => x.SourceId == currentStep.Id);
                                var nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == currentProgress.TargetId);
                                if (nextStep.ActionTypeId == (long)ActionTypeEnum.exclusiveGateway)
                                {
                                    var x = nextStep.SourceProgresses[0];
                                    nextStep = workFlow.Steps.FirstOrDefault(i => i.Id == x.TargetId);
                                }

                                checkIsEveryOne.TargetStateId = nextStep.SourceProgresses[0].StateId;
                                checkIsEveryOne.SourceStateId = null;
                                checkIsEveryOne.TargetStepId = nextStep.SourceProgresses[0].SourceId;
                                checkIsEveryOne.SourceStepId = step.Id;
                                checkIsEveryOne.Id = workFlowId;
                                checkIsEveryOne.ProjectId = workFlow.ProjectId;
                                checkIsEveryOne.MainAggregateId = workFlow.MainAggregateId;
                                //checkIsEveryOne = result;
                            }
                        }
                    }
                }

                if (checkActorAccess.Id != 0) return checkActorAccess;
                else if (checkIsEveryOne.Id != 0) return checkIsEveryOne;
                else throw new SimaResultException(CodeMessges._100063Code, Messages.CreateIssueWithChechActorException);

            }
            else
            {
                var step = workFlow.Steps.FirstOrDefault(s => s.ActionTypeId == startEventActionTypeId);
                if (step != null)
                {
                    var progress = step.SourceProgresses.FirstOrDefault(x => x.SourceId == step.Id);
                    if (progress != null)
                    {
                        var nextStep = workFlow.Steps.FirstOrDefault(x => x.Id == progress.TargetId);

                        result.TargetStateId = progress.StateId;
                        result.SourceStateId = null;
                        result.TargetStepId = progress.TargetId;
                        result.SourceStepId = step.Id;
                        result.Id = workFlowId;
                        result.ProjectId = workFlow.ProjectId;
                        result.MainAggregateId = workFlow.MainAggregateId;
                        return result;
                    }
                }
            }

            throw new SimaResultException(CodeMessges._400Code, Messages.IssueErrorException);
        }
    }
    public static string ContainsComparisonOperator(string text)
    {
        // Array of comparison operators to check for
        string[] operators = { "(!=)", "(==)", "(<=)", "(>=)", "(>)", "(<)" };

        // Use Any method for efficient check
        return operators.FirstOrDefault(op => text.Contains(op));
    }
    public async Task ExecuteSP(long ProgressId, string mainAggregateName, List<InputModel> SystemParams, List<InputParamQueryModel> InputParam, List<AddDocumentToSPQuery> docs)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var issueId = SystemParams.First(x => x.Key == "IssueId").Value;
            #region -- exe Sp --
            var SpQury = @"select 
                  p.Id ProgressId,
                  ps.StoreProcedureName StoreProcedureName,
                  ps.Id ProgressStoreProcedureId,
                  psp.Id ParamId,
                  psp.Name ParamName,
                  psp.FixedValue,
                  psp.IsSystemParam IsSystemParam,
                  psp.SystemParamName SystemParamName,
                  psp.DataTypeId,
                  ps.ExecutionOrdering,
                  psp.JsonFormat
                  from
                  Project.Progress p 
				  join Project.ProgressStoreProcedure ps on p.Id = ps.ProgressId  and ps.ActiveStatusId <>3
                  left join Project.ProgressStoreProcedureParam psp on psp.ProgressStoreProcedureId = ps.Id and psp.ActiveStatusId <>3
                  where p.Id= @ProgressId

				  union

                  select 
                  p.Id ProgressId,
                  ps.StoreProcedureName StoreProcedureName,
                  ps.Id ProgressStoreProcedureId,
                  psp.Id ParamId,
                  psp.Name ParamName,
                  psp.FixedValue,
                  psp.IsSystemParam IsSystemParam,
                  psp.SystemParamName SystemParamName,
                  psp.DataTypeId,
                  ps.ExecutionOrdering,
                  psp.JsonFormat
                  from
                 Project.Progress p
				 join Project.Step S on S.Id = P.SourceId 
				 join Project.Progress PT on PT.TargetId = S.Id and PT.ActiveStatusId <>3
				 join IssueManagement.Issue I On I.CurrenStepId = PT.SourceId and I.ActiveStatusId <>3
				 join  Project.ProgressStoreProcedure ps on PT.Id = ps.ProgressId  and ps.ActiveStatusId <>3
                 left join Project.ProgressStoreProcedureParam psp on psp.ProgressStoreProcedureId = ps.Id and psp.ActiveStatusId <>3
                 where p.Id= @ProgressId and I.Id =@issueId and S.ActiveStatusID <> 3
                  ";
            var spQuery = await connection.QueryAsync<StoreProcedureInfo>(SpQury, new { ProgressId, issueId });
            var spGroup = spQuery.OrderBy(x => x.ExecutionOrdering).GroupBy(x => x.StoreProcedureName);
            foreach (var item in spGroup)
            {
                string sp = string.Empty;
                //if (item.Key.Contains(mainAggregateName + "Document"))
                //{

                if (item.Key.Contains("UploadedFile"))
                {
                    if (docs is not null && docs.Count > 0)
                    {
                        var json = JsonConvert.SerializeObject(docs);
                        sp = $"{item.Key} @UserId ={SystemParams.First(x => x.Key == "UserId").Value},@DocumentIdList= N'{json}'";
                    }
                }
                else
                {
                    var paramas = new List<StoreProcedureParamInfo>();
                    paramas = item.Select(it => new StoreProcedureParamInfo
                    {
                        ParamName = it.ParamName,
                        IsSystemParam = it.IsSystemParam,
                        ParamId = it.ParamId,
                        SystemParamName = it.SystemParamName,
                        DataTypeId = it.DataTypeId,
                        FixedValue = it.FixedValue,
                        JsonFormat = it.JsonFormat
                    }).ToList();
                    // build-in params
                    foreach (var p in InputParam)
                    {
                        var param = paramas.Where(it => it.ParamId == p.Id).FirstOrDefault();
                        if (param != null) param.Value = p.ParamValue;
                    }

                    foreach (var p in paramas)
                    {
                        if (!string.IsNullOrEmpty(p.FixedValue) && string.IsNullOrEmpty(p.JsonFormat))
                        {
                            p.Value = p.FixedValue;
                        }

                        if (p.IsSystemParam == "1")
                        {
                            if (p.SystemParamName == "Id")
                            {
                                var id = IdHelper.GenerateUniqueId();
                                p.Value = id.ToString();
                            }
                            var param2 = SystemParams.FirstOrDefault(it => it.Key == p.SystemParamName);
                            if (param2 != null) p.Value = param2.Value;
                        }
                    }
                    sp = $"{item.Key} ";
                    foreach (var i in paramas)
                    {
                        if (i.DataTypeId == 2)
                            sp += $"@{i.ParamName} = N'{i.Value}' ,";
                        else if (i.DataTypeId == 6)
                        {
                            var date = i.Value.ToMiladiDate().ToString();
                            sp += $"@{i.ParamName} = N'{date}' ,";
                        }
                        else
                            sp += $"@{i.ParamName} = {i.Value} ,";
                    }
                    sp = sp.Remove(sp.Length - 1);
                }
                try
                {
                    //var result = await connection.ExecuteAsync(sp);
                    //if (result == -1)
                    //{
                    //    throw new SimaResultException(CodeMessges._100062Code, Messages.ExecStoreProcedureError);
                    //}
                    if (!string.IsNullOrEmpty(sp))
                        await connection.ExecuteAsync(sp);
                }
                catch (Exception)
                {
                    throw new SimaResultException(CodeMessges._100062Code, Messages.ExecStoreProcedureError);
                }
            }
            #endregion
        }
    }




    private async Task<GetWorkflowInfoByIdResponseQueryResult> EvaluateNextProgress(List<Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc.NextProgressInfo> progresses, string conditionValue, List<InputModel> inputs)
    {

        foreach (var item in progresses)
        {
            if (string.IsNullOrEmpty(item.ConditionExpression))
            {
                var query = new GetNextStepQuery { WorkflowId = item.WorkflowId, ProgressId = item.ProgressId, SystemParams = inputs, NextStepId = item.TargetId.Value, ConditionValue = conditionValue };
                return await GetNextStepById(item.WorkflowId, query, null);

            }
            var condition = item.ConditionExpression;
            var extension = item.Extension;

            var evalueatedExtension = await GetExtension(extension, inputs);
            var conditionWithExtension = EvaluateConditionExtension(condition, evalueatedExtension);
            var isView = condition.Trim().StartsWith("@");
            if (isView)
            {
                var allQueries = GetAllViewQueries(conditionWithExtension, inputs);
                var conditionResult = await EvaluateViewCondition(allQueries);
                if (conditionResult)
                {
                    var sourceStepId = item.TargetId.Value;
                    var query = new GetNextStepQuery { WorkflowId = item.WorkflowId, ProgressId = item.ProgressId, SystemParams = inputs, NextStepId = item.TargetId.Value, ConditionValue = conditionValue };

                    return await GetNextStepById(item.WorkflowId, query, null);
                    //if (nextProgress.ActionTypeId == 6)
                    //{
                    //    var query = new GetNextStepQuery { WorkflowId = item.WorkflowId, ProgressId = nextProgress.ProgressId, SystemParams = inputs, NextStepId = nextProgress.TargetId, ConditionValue = conditionValue };
                    //    return await GetNextStepById(item.WorkflowId, query);

                    //    //var forFindTargetQuery = @"select p.TargetId from Project.Progress p where p.SourceId=@Id";
                    //    //sourceStepId = await connection.QueryFirstOrDefaultAsync<long>(forFindTargetQuery, new { Id = sourceStepId });
                    //}

                    //return new GetWorkflowInfoByIdResponseQueryResult { SourceStepId = sourceStepId, SourceStateId = item.NextStateId };
                }
            }
            else
            {
                var formConditionResult = await GetAllFormQueries(conditionWithExtension, inputs);
                if (formConditionResult)
                {
                    return new GetWorkflowInfoByIdResponseQueryResult { SourceStepId = item.TargetId.Value, SourceStateId = item.NextStateId };
                }
            }

        }
        throw new SimaResultException(CodeMessges._100061Code, Messages.NoConditionWasCorrect);
    }
    private async Task<NextProgressDetails> GetNextProgress(long workflowId, long sourceStepId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var targetStepQuery = @"SELECT S.ActionTypeId, FStep.Id ProgressId, FStep.TargetId FROM Project.Step S
                                                CROSS APPLY [Project].[ReturnNextStepN]   (s.id,@WorkflowId) FStep WHERE S.Id = @TargetId AND S.ActiveStatusId <> 3 ;";
            return await connection.QueryFirstOrDefaultAsync<NextProgressDetails>(targetStepQuery, new { TargetId = sourceStepId, WorkflowId = workflowId });
        }
    }
    private string EvaluateConditionExtension(string condition, Dictionary<string, string> evalueatedExtension)
    {
        if (!condition.Contains("*"))
        {
            return condition;
        }
        var extensionValues = condition.Split('*', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in extensionValues)
        {
            var allVariables = item.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var variable in allVariables)
            {
                var findValue = evalueatedExtension.TryGetValue(variable, out var value);
                if (!findValue)
                {
                    continue;
                }
                condition = condition.Replace($"*{variable}", value);

            }
        }
        return condition;
    }
    private async Task<Dictionary<string, string>> GetExtension(string? extension, List<InputModel> inputs)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrEmpty(extension))
            return result;

        var allVariables = extension.Split('|', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in allVariables)
        {
            var variable = item.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (variable.Length > 2)
                continue;
            var keyValue = new KeyValuePair<string, string>();
            var variableName = variable[0].Trim().Substring(1);
            var variableValue = variable[1];
            if (variableValue.StartsWith('#'))
            {

                var query = inputs.FirstOrDefault(x => x.Key.ToLower() == variableName.ToLower());
                result.Add(query.Key, query.Value);
                continue;
            }
            else if (variableValue.StartsWith('@') || variableValue.StartsWith("\"@"))
            {
                var isString = variableValue.StartsWith("\"");
                variableValue = isString ? variableValue.Substring(2) : variableValue.Substring(1);
                var aqlQuery = GetQueryConditions(variableValue, inputs);
                var conditionSplitter = aqlQuery.Split('?');
                var tableColumn = conditionSplitter[0].Split(".");
                var sqlQuery = SelectGenerator(tableColumn);
                var conditionaql = conditionSplitter[1];
                var conditionSql = ConditionGenerator(conditionaql);
                var query = $"{sqlQuery} WHERE {conditionSql}";
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    if (!isString)
                    {
                        var longResult = await connection.QuerySingleOrDefaultAsync<long>(query);
                        result.Add(variableName, longResult.ToString());
                        continue;
                    }
                    var queryResult = await connection.QuerySingleOrDefaultAsync<string>(query);
                    result.Add(variableName, queryResult.ToString());
                }
            }
            else
            {
                result.Add(variable[0], variableValue);
            }
        }
        return result;
    }
    private async Task<bool> GetAllFormQueries(string condition, List<InputModel> formData)
    {
        var result = false;
        string[] allFormCheck = condition.Split('#', StringSplitOptions.None);
        foreach (var item in allFormCheck)
        {
            if (string.IsNullOrWhiteSpace(item))
                continue;
            var aqlQuery = GetFormConditions(item, formData);
            result = await EvaluateFormCondition(aqlQuery);
            if (!result)
            {
                return false;
            }
        }
        return result;
    }
    private List<QueryWithCheckerModel> GetFormConditions(string condition, List<InputModel> formData)
    {
        var result = new List<QueryWithCheckerModel>();
        var items = condition.Split('$');
        foreach (var item in items)
        {
            string pattern = @$"({string.Join("|", splitWords)})";
            var checker = ContainsComparisonOperator(condition);
            var splittedCondition = item.Split(splitWords, StringSplitOptions.RemoveEmptyEntries);
            if (string.IsNullOrWhiteSpace(item) || splittedCondition.Length < 1)
            {
                continue;
            }
            var query = formData.FirstOrDefault(x => x.Key.ToLower() == splittedCondition[0].Trim().ToLower());
            if (query is null)
                continue;
            var data = new QueryWithCheckerModel { Checker = checker, Query = query?.Value ?? string.Empty, ValueToCheck = splittedCondition[1] };
            result.Add(data);
        }
        return result;
    }
    private async Task<bool> EvaluateFormCondition(List<QueryWithCheckerModel> queries)
    {
        var result = false;
        foreach (var query in queries)
        {
            try
            {
                if (!query.IsLong)
                {

                    result = CheckStringCondition(query.Query, query.Checker, query.ValueToCheck);
                    if (!result)
                    {
                        break;
                    }
                }
                else
                {
                    var longData = long.Parse(query.Query);
                    result = CheckLongCondition(longData, query.Checker, Convert.ToInt64(query.ValueToCheck));
                    if (!result)
                    {
                        break;
                    }
                }
            }
            catch
            {
                break;
            }

        }
        return result;
    }
    private async Task<bool> EvaluateViewCondition(List<QueryWithCheckerModel> queries)
    {
        var result = false;
        var skip = false;
        foreach (var query in queries)
        {
            if (skip)
            {
                if (query.Combiner.Contains("||"))
                {
                    continue;
                }
                skip = false;
                continue;
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    if (!query.IsLong)
                    {

                        var stringChecker = await connection.QuerySingleOrDefaultAsync<string>(query.Query);
                        result = CheckStringCondition(stringChecker, query.Checker, query.ValueToCheck);
                        if (!result && query.Combiner.Contains("&&"))
                        {
                            break;
                        }


                    }
                    else
                    {
                        var longChecker = await connection.QueryFirstOrDefaultAsync<long>(query.Query);
                        result = CheckLongCondition(longChecker, query.Checker, Convert.ToInt64(query.ValueToCheck));
                        if (!result && query.Combiner.Contains("&&"))
                        {
                            break;
                        }
                    }
                    if (result && query.Combiner.Contains("||"))
                    {
                        skip = true;
                    }
                }
                catch
                {
                    break;
                }


            }

        }
        return result;
    }
    private bool CheckStringCondition(string check, string checker, string valueToCheck)
    {
        return checker switch
        {
            "==" => check.Trim().Equals(valueToCheck.Trim()),
            "!=" => !check.Trim().Equals(valueToCheck.Trim()),
            _ => false
        };
    }
    private bool CheckLongCondition(long check, string checker, long valueToCheck)
    {
        return checker switch
        {
            "(==)" => check == valueToCheck,
            "(<=)" => check <= valueToCheck,
            "(>=)" => check >= valueToCheck,
            "(!=)" => check != valueToCheck,
            "(>)" => check > valueToCheck,
            "(<)" => check < valueToCheck,
            _ => false
        };
    }
    private List<QueryWithCheckerModel> GetAllViewQueries(string condition, List<InputModel> inputModels)
    {
        //string pattern = @"(?=(" + string.Join("|", splitWords) + @"))";
        var aqlQuery = GetQueryConditions(condition, inputModels);
        var allQueriesWithExpression = ExtractStringsAndConditions(aqlQuery);

        //allQueries = Array.FindAll(allQueries, s => !string.IsNullOrWhiteSpace(s));
        var queries = new List<QueryWithCheckerModel>();
        foreach (var item in allQueriesWithExpression)
        {
            var query = GetQuery(item.Key);
            query.Combiner = item.Value;
            queries.Add(query);
        }
        return queries;
    }
    private static Dictionary<string, string> ExtractStringsAndConditions(string expression)
    {
        var results = new Dictionary<string, string>();
        string[] delimiters = { " || ", " && " };

        foreach (var delimiter in delimiters)
        {
            if (!expression.Contains(delimiter))
            {
                continue;
            }
            var parts = expression.Split(new[] { delimiter }, StringSplitOptions.None);

            string part = parts[0].Trim();
            string stringPart = part.Substring(1).Trim();
            results.TryAdd(stringPart, delimiter.Trim());
            var subLenght = part.Length + delimiter.Length;
            if (subLenght < expression.Length)
            {
                expression = expression.Substring(subLenght).Trim();
                var newDic = ExtractStringsAndConditions(expression);
                foreach (var item in newDic)
                {
                    results.TryAdd(item.Key, item.Value);
                }

            }
        }
        if (!results.Any())
        {
            string stringPart = expression.Substring(1).Trim();
            results.TryAdd(stringPart, string.Empty);
        }

        return results;
    }
    private string[] splitWords = new string[] { "(!=)", "(==)", "(<=)", "(>=)", "(>)", "(<)" };
    private QueryWithCheckerModel GetQuery(string aqlQuery)
    {
        string pattern = @$"({string.Join("|", splitWords)})";
        var checker = ContainsComparisonOperator(aqlQuery);
        var queries = aqlQuery.Split(splitWords, StringSplitOptions.RemoveEmptyEntries);
        var query = queries[0];
        var conditionSplitter = query.Split('?');
        var tableColumn = conditionSplitter[0].Split(".");
        var sqlQuery = SelectGenerator(tableColumn);
        var conditionaql = conditionSplitter[1];
        var conditionSql = ConditionGenerator(conditionaql);
        var result = $"{sqlQuery} WHERE {conditionSql}";
        return new QueryWithCheckerModel { Checker = checker, Query = result, ValueToCheck = queries[1] };
    }
    private string SelectGenerator(string[] tableAndColumn)
    {
        var column = tableAndColumn.LastOrDefault();
        var query = !string.IsNullOrEmpty(tableAndColumn[0]) ? $"SELECT {column} FROM {tableAndColumn[0]}" : $"SELECT {column} ";
        return query;
    }
    private string ConditionGenerator(string condition)
    {
        var result = condition.Replace("&", " AND ");

        return result;
    }
    private string CastVariableToQuery(string value, string extension)
    {
        var expression = value.Split(' ');
        var variable = expression[0];
        var extensions = extension.Split("|");
        var result = string.Empty;
        if (extensions.Length < 2)
        {
            return value;
        }
        foreach (var item in extensions)
        {
            var definedVariable = item.Split(":");

            if (definedVariable[0].Trim() != variable)
            {
                continue;
            }

            result = definedVariable[1];
            break;
        }
        return result;
    }
    private string GetQueryConditions(string condition, List<InputModel> inputData)
    {
        var result = new List<QueryWithCheckerModel>();
        var firstParamIndex = condition.IndexOf('$');
        if (firstParamIndex < 0)
        {
            return condition;
        }
        var conditionQuery = condition.Substring(firstParamIndex - 1);
        var items = conditionQuery.Trim().Split('$', StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in items)
        {
            var spaceIndex = item.IndexOf(' ');

            var value = spaceIndex > 0 ? item.Substring(0, spaceIndex) : item.Substring(0);

            var input = inputData.FirstOrDefault(x => x.Key == value);
            if (input is null)
                continue;
            condition = condition.Replace("$" + value, input.Value);
        }
        return condition;
    }

    public async Task<TResult> PostAsync<TResult, TRequest>(string actionUrl, TRequest request, Dictionary<string, string> headers = null) where TResult : class
    {
        var _httpClient = new HttpClient();
        if (headers != null)
            foreach (var item in headers)
                _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);



        var response = await _httpClient.PostAsync(actionUrl, CreateHttpContent<TRequest>(request));
        //response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadAsStringAsync();

        if (typeof(TResult) == typeof(string))
        {
            return data as TResult;
        }
        var model = JsonConvert.DeserializeObject<TResult>(data);

        return model;
    }

    private HttpContent CreateHttpContent<T>(T content)
    {
        var json = string.Empty;
        if (typeof(T) == typeof(string))
        {
            json = content.ToString();
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        else
        {
            json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");


        }
    }

    private static JsonSerializerSettings MicrosoftDateFormatSettings
    {
        get
        {
            return new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
        }
    }


}

