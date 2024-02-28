using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Exceptions;
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
              WHERE P.[ActiveStatusID] <> 3 and P.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetProjectQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.ProjectNotFoundError;
            response = result;
        }
        return response;
    }

    public async Task<List<GetProjectQueryResult>> GetAll()
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
,p.[CreatedAt]
      FROM [Project].[Project] P
  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
  INNER JOIN [Basic].[ActiveStatus] A on A.ID = P.ActiveStatusID
              WHERE P.[ActiveStatusID] <> 3 
Order By p.[CreatedAt] desc  
";
            var result = await connection.QueryAsync<GetProjectQueryResult>(query);
            if (result is null) throw SimaResultException.ProjectNotFoundError;
            response = result.ToList();
        }
        return response;
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
            if (result is null) throw SimaResultException.ProjectNotFoundError;
            response = result.ToList();
        }
        return response;
    }
}
