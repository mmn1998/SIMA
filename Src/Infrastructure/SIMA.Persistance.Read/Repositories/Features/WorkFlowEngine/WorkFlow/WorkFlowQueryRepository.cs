using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Resources;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;

public class WorkFlowQueryRepository : IWorkFlowQueryRepository
{
    private readonly string _connectionString;
    private readonly ISimaIdentity _simaIdentity;
    public WorkFlowQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _simaIdentity = simaIdentity;

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
            if (result is null) throw new SimaResultException("10057", Messages.WorkflowNotFoundError);
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
            if (result is null) throw new SimaResultException("10058", Messages.StateNotFoundError);
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
            if (result is null) throw new SimaResultException("10058", Messages.StateNotFoundError);
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
            if (result is null) throw new SimaResultException("10057", Messages.WorkflowNotFoundError);
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

    public async Task<GetWorkflowInfoByIdResponseQueryResult> GetNextStepById(long workflowId, GetNextStepQuery query)
    {
        var model = new Dictionary<long, NextProgressInfo>();
        var getNextStepQuery = @"select distinct wp.StateId CurrentProgressStateId, wp.ProgressId CurrentProgressId, s.Id StepId, s.WorkFlowID WorkflowId, s.ActionTypeId, p.TargetId, p.ConditionExpression, p.StateId NextStateId, p.Extension from project.Step s
                                  inner join project.WorkFlow w on w.Id = s.WorkFlowID
                                  left join Project.Progress p on p.SourceId = s.Id
                                  left join (SELECT iwp.Id ProgressId, iwp.StateId, iwp.WorkFlowId from Project.Progress iwp where iwp.Id = @ProgressId) wp on wp.WorkFlowId = w.Id
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
                        item = new NextProgressInfo { ConditionExpression = current.ConditionExpression, Extension = current.Extension, NextStateId = current.NextStateId, TargetId = current.TargetId };
                        model.Add(current.TargetId, item);
                    }
                }

                result.CurrentProgressId = current.CurrentProgressId;
                result.CurrentProgressStateId = current.CurrentProgressStateId;
                result.StepId = current.StepId;
                result.WorkflowId = current.WorkflowId;
                result.ActionTypeId = current.ActionTypeId;
            }
            result.NextProgressInfo = model.Values.Any() ? model.Values.ToList() : null;
            if (result.ActionTypeId == 6)
            {
                return await EvaluateNextProgress(result.NextProgressInfo, query.ConditionValue, query.Form);
            }
            return new GetWorkflowInfoByIdResponseQueryResult { SourceStepId = result.StepId, SourceStateId = result.CurrentProgressStateId };

        }
    }
    private async Task<GetWorkflowInfoByIdResponseQueryResult> EvaluateNextProgress(List<NextProgressInfo> progresses, string ConditionValue, List<FormModel> formData)
    {

        foreach (var item in progresses)
        {
            var condition = item.ConditionExpression;
            var extension = item.Extension ?? string.Empty;
            var isView = condition.Trim().StartsWith("@");
            if (isView)
            {
                var allQueries = GetAllViewQueries(condition, extension);
                var conditionResult = await EvaluateViewCondition(allQueries);
                if (conditionResult)
                {
                    return new GetWorkflowInfoByIdResponseQueryResult { SourceStepId = item.TargetId.Value, SourceStateId = item.NextStateId };
                }
            }
            else
            {
                var formConditionResult = await GetAllFormQueries(condition, formData);
                if (formConditionResult)
                {
                    return new GetWorkflowInfoByIdResponseQueryResult { SourceStepId = item.TargetId.Value, SourceStateId = item.NextStateId };
                }
            }

        }
        throw new Exception("No Condition Was Correct");
    }

    private async Task<bool> GetAllFormQueries(string condition, List<FormModel> formData)
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
    private List<QueryWithCheckerModel> GetFormConditions(string condition, List<FormModel> formData)
    {
        var result = new List<QueryWithCheckerModel>();
        var items = condition.Split('$');
        foreach (var item in items)
        {
            string pattern = @$"({string.Join("|", splitWords)})";
            var checker = Regex.Match(item, pattern);
            var splittedCondition = item.Split(splitWords, StringSplitOptions.None);
            if (string.IsNullOrWhiteSpace(item) || splittedCondition.Length < 1)
            {
                continue;
            }
            var query = formData.FirstOrDefault(x => x.Key.ToLower() == splittedCondition[0].ToLower());
            var data = new QueryWithCheckerModel { Checker = checker.Value, Query = query?.Value ?? string.Empty, ValueToCheck = splittedCondition[1] };
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
        foreach (var query in queries)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    if (!query.IsLong)
                    {

                        var stringChecker = await connection.QuerySingleOrDefaultAsync<string>(query.Query);
                        result = CheckStringCondition(stringChecker, query.Checker, query.ValueToCheck);
                        if (!result)
                        {
                            break;
                        }


                    }
                    else
                    {
                        var longChecker = await connection.QuerySingleOrDefaultAsync<long>(query.Query);
                        result = CheckLongCondition(longChecker, query.Checker, Convert.ToInt64(query.ValueToCheck));
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
            "==" => check == valueToCheck,
            "<=" => check <= valueToCheck,
            ">=" => check >= valueToCheck,
            "!=" => check != valueToCheck,
            ">" => check > valueToCheck,
            "<" => check < valueToCheck,
            _ => false
        };
    }
    private List<QueryWithCheckerModel> GetAllViewQueries(string condition, string extension)
    {
        //string pattern = @"(?=(" + string.Join("|", splitWords) + @"))";
        var aqlQuery = GetQueryConditions(condition, extension);

        string[] allQueries = aqlQuery.Split('@', StringSplitOptions.None);

        allQueries = Array.FindAll(allQueries, s => !string.IsNullOrWhiteSpace(s));
        var queries = new List<QueryWithCheckerModel>();
        foreach (var item in allQueries)
        {
            var query = GetQuery(item);
            queries.Add(query);
        }
        return queries;
    }
    private string[] splitWords = new string[] { "!=", "==", "<=", ">=", ">", "<" };

    private QueryWithCheckerModel GetQuery(string aqlQuery)
    {
        string pattern = @$"({string.Join("|", splitWords)})";
        var checker = Regex.Match(aqlQuery, pattern);
        var queries = aqlQuery.Split(splitWords, StringSplitOptions.None);
        var query = queries[0];
        var conditionSplitter = query.Split('?');
        var tableColumn = conditionSplitter[0].Split(".");
        var sqlQuery = SelectGenerator(tableColumn);
        var conditionaql = conditionSplitter[1];
        var conditionSql = ConditionGenerator(conditionaql);
        var result = $"{sqlQuery} WHERE {conditionSql}";
        return new QueryWithCheckerModel { Checker = checker.Value, Query = result, ValueToCheck = queries[1] };
    }

    private string SelectGenerator(string[] tableAndColumn)
    {
        var column = tableAndColumn.LastOrDefault();
        var query = $"SELECT {column} FROM {tableAndColumn[0]}";
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
    private string GetQueryConditions(string condition, string extension)
    {
        var result = condition;
        var items = condition.Split('$');
        foreach (var item in items)
        {
            var castedVariable = CastVariableToQuery(item, extension);
            result.Replace(item, castedVariable);
        }
        return result;
    }

}
