using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Project;

public class ProjectQueryRepository : IProjectQueryRepository
{
    private readonly string _connectionString;

    public ProjectQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();

    }
    public async Task<GetProjectQueryResult> FindById(long id)
    {
        try
        {
            var response = new GetProjectQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                                            string query = $@"
                           SELECT DISTINCT 
                                  P.[Id]
                                    ,P.[DomainID]
                                    ,P.[Name]
                                    ,P.[Code]
                                    ,P.[ActiveStatusID]
                                    ,D.[Name] DomainName
                                   ,A.[Name] ActiveStatus
                                FROM [Project].[Project] P
                            INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                                        WHERE P.[ActiveStatusID] <> 3 and P.Id = @Id
                            
                            --ProjectMember
                            
                             SELECT DISTINCT 
                            	    PM.[IsAdminProject]
                            	    ,PM.[IsManager]
                            	    ,PM.[Id] ProjectMemberId
                            	    ,U.[Id] UserId
                            	    ,U.[Username]
                                    ,Pro.[FirstName] + ' ' + Pro.[LastName] as FullName
                                FROM [Project].[Project] P
                            INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                            left join [Project].[ProjectMember] PM on P.Id = PM.ProjectID
                            left join [Authentication].[Users] U on PM.UserID = u.Id
                            left join [Authentication].[Profile] Pro on Pro.Id = U.ProfileID
                            WHERE P.[ActiveStatusID] <> 3 and PM.[ActiveStatusID] <> 3 and P.Id = @Id
                            
                            
                            --ProjectGroup

                            SELECT  
                            	   G.[Name] GroupName
                            	   ,G.[Id] GroupId
                            	   ,PG.[Id] ProjectGroupId
                                FROM [Project].[Project] P
                            INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
                            left join [Project].[ProjectGroup] PG on PG.ProjectId = P.Id
                            left join [Authentication].[Groups] G on PG.GroupId = G.Id 
                            WHERE P.[ActiveStatusID] <> 3 and  PG.[ActiveStatusID] <> 3 and P.Id = @Id";
                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
                {
                    response = multi.ReadAsync<GetProjectQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException("10057",Messages.ProjectNotFoundError);
                    response.ProjectMembers = multi.ReadAsync<GetProjectMemberResult>().GetAwaiter().GetResult().ToList();
                    response.ProjectGroups = multi.ReadAsync<GetProjectGroupResult>().GetAwaiter().GetResult().ToList();
                }
                response.NullCheck();

            }
            return response;
        }
        catch   (Exception ex)
        {
            throw;
        }
        
    }

    public async Task<Result<IEnumerable<GetProjectQueryResult>>> GetAll(GetAllProjectsQuery request)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
           
                string queryCount = @"  WITH Query as(
						          SELECT DISTINCT 
      	    P.[Id]
          ,P.[DomainID]
          ,P.[Name]
          ,P.[Code]
          ,P.[ActiveStatusID]
      	   ,D.[Name] DomainName
      	   ,A.[Name] ActiveStatus
          ,p.[CreatedAt]
  FROM [Project].[Project] P
  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
  WHERE  P.ActiveStatusId != 3
     AND  (@DomainId is null OR P.DomainID = @DomainId) 
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
							 SELECT DISTINCT 
      	    P.[Id]
          ,P.[DomainID]
          ,P.[Name]
          ,P.[Code]
          ,P.[ActiveStatusID]
      	   ,D.[Name] DomainName
      	   ,A.[Name] ActiveStatus
          ,p.[CreatedAt]
  FROM [Project].[Project] P
  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
  WHERE  P.ActiveStatusId != 3
     AND  (@DomainId is null OR P.DomainID = @DomainId) 
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetProjectQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
    }

    public async Task<List<GetProjectQueryResult>> GetByDomainId(long domainId)
    {
        var response = new List<GetProjectQueryResult>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT 
	       P.[Id]
          ,P.[DomainID]
          ,P.[Name]
          ,P.[Code]
          ,P.[ActiveStatusID]
	      ,D.[Name] DomainName
	      ,A.[Name] ActiveStatus
      FROM [Project].[Project] P
  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
              WHERE P.[ActiveStatusID] <> 3 and P.[DomainId] = @DomainId";
            var result = await connection.QueryAsync<GetProjectQueryResult>(query, new { DomainId = domainId });
            if (result is null) throw new SimaResultException("10056",Messages.ProjectNotFoundError);
            response = result.ToList();
        }
        return response;
    }
}
