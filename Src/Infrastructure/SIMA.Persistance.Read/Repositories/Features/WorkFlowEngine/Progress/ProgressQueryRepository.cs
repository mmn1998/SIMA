using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Progress;

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
                          ,P.Name
                          ,S.[Name] StateName
                          ,P.[Name]
                          ,P.[ActiveStatusID]
                          ,A.[Name] ActiveStatus
                          ,p.[CreatedAt]
                          ,p.[ConditionExpression]
                       		 ,P.[workFlowId]
                       		 ,W.[Name] as WorkFlowName
                       		 ,D.Id DomainID
                          ,D.Name DomainName
                       		 ,Pro.Id ProjectId
                          ,Pro.Name ProjectName
						  ,Trg.Name TargetName
						  ,Trg.Id TargetId
						  ,Src.Name SourceName
						  ,Src.Id SourceId
                  FROM [Project].[Progress] P
                  INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                  left JOIN [Project].[State] S on P.StateId = s.Id
				  inner join Project.Step Src on Src.Id = P.SourceId
				  inner join Project.Step Trg on Trg.Id = p.TargetId
                  join [PROJECT].[WorkFlow] W on P.WorkFlowID = W.Id
                  join Project.Project Pro on Pro.Id=W.ProjectID
                  join Authentication.Domain D on D.Id=Pro.DomainID
                  WHERE  P.ActiveStatusId != 3 and W.ActiveStatusId != 3 and P.Id = @Id

            --Sp
			select distinct
            psp.Id
			,psp.StoreProcedureName
			,psp.ExecutionOrdering
			
			from Project.Progress pp
			inner join Project.ProgressStoreProcedure psp on psp.ProgressId=pp.id and psp.ActiveStatusId <> 3
			where pp.Id = @Id


			-- param
			select distinct
              pspp.Name
              ,psp.Id ProcedureId
              ,pspp.Id
              ,pspp.DataTypeId
              ,pspp.IsRequired
              ,pspp.IsSystemParam
              ,pspp.SystemParamName
              ,pspp.DisplayName
              ,pspp.JsonFormat
              ,pspp.ApiNameForDataBounding
              ,pspp.UiInputElementId
              ,pspp.BoundFormat
              ,pspp.StoredProcedureForDataBounding
              ,PSPP.ApiMethodActionId
              ,AMA.Name ApiMethodActionName
              from Project.Progress pp
              inner join Project.ProgressStoreProcedure psp on psp.ProgressId=pp.id and psp.ActiveStatusId <> 3
              inner join Project.ProgressStoreProcedureParam pspp on pspp.ProgressStoreProcedureId = psp.Id and pspp.ActiveStatusId <> 3
              left join  Basic.ApiMethodAction AMA on AMA.Id = PSPP.ApiMethodActionId
			where pp.Id = @Id";
            
            using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
            {
                response = await multi.ReadFirstOrDefaultAsync<GetProgressQueryResult>() ?? throw SimaResultException.NotFound;
                response.StoreProcedures = await multi.ReadAsync<GetProgressStoreProcedureQueryResult>();
                var result = await multi.ReadAsync<GetProgressStoreProcedureParamQueryResult>();
                foreach (var item in response.StoreProcedures)
                {
                    item.Params = result.Where(x => x.ProcedureId == item.Id);
                }
            }

        }
        return response;
    }

    public async Task<Result<IEnumerable<GetProgressQueryResult>>> GetAll(GetAllProgressQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
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
       ,
(CASE     WHEN  src.ActionTypeId=6 THEN
''+isnull([Project].[ReturnSRCN](P.[Id]),'')
 
ELSE ''+src.[Name] END ) AS  SourceName
           ,src.[Id] SourceId
           ,trg.[Name] TargetName
           ,trg.[Id] TargetId
   FROM [Project].[Progress] P
   INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
   left JOIN [Project].[State] S on P.StateId = s.Id        
   join [PROJECT].[WorkFlow] W on P.WorkFlowID = W.Id
   join Project.Project Pro on Pro.Id=W.ProjectID
   join Authentication.Domain D on D.Id=Pro.DomainID
   left join Project.Step src on src.Id = P.SourceId
   left join Project.Step trg on trg.Id = P.TargetId
   WHERE  P.ActiveStatusId != 3 and W.ActiveStatusId != 3
   AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR Pro.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

            string query = $@" WITH Query as(
														SELECT DISTINCT
 P.[Id]
 ,trg.ActionTypeId actiontypeid
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
       ,
(CASE     WHEN  src.ActionTypeId=6 THEN
''+isnull([Project].[ReturnSRCN](P.[Id]),'')
 
ELSE ''+src.[Name] END ) AS  SourceName


           ,src.[Id] SourceId
           ,(CASE     WHEN  trg.ActionTypeId=6 THEN
'Gateway'
 
ELSE ''+trg.[Name] END ) AS  TargetName
           ,trg.[Id] TargetId
   FROM [Project].[Progress] P
   INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
   left JOIN [Project].[State] S on P.StateId = s.Id        
   join [PROJECT].[WorkFlow] W on P.WorkFlowID = W.Id
   join Project.Project Pro on Pro.Id=W.ProjectID
   join Authentication.Domain D on D.Id=Pro.DomainID
   left join Project.Step src on src.Id = P.SourceId
   left join Project.Step trg on trg.Id = P.TargetId
   WHERE  P.ActiveStatusId != 3 and W.ActiveStatusId != 3   
   AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR Pro.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("WorkFlowId", request.WorkFlowId);
            dynaimcParameters.Item2.Add("ProjectId", request.ProjectId);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetProgressQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
}
