using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ConsequenceLevels;

public class ConsequenceLevelQueryRepository : IConsequenceLevelQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ConsequenceLevelQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT CL.[Id]
              ,CL.[Name]
              ,CL.[Code]
			  ,CL.NumericValue
	          ,A.[Name] ActiveStatus
              ,CL.CreatedAt
          FROM [RiskManagement].[ConsequenceLevel] CL
          INNER JOIN [Basic].[ActiveStatus] A ON CL.ActiveStatusId = A.ID
WHERE CL.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetConsequenceLevelQueryResult>>> GetAll(GetAllConsequenceLevelsQuery request)
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
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetConsequenceLevelQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetConsequenceLevelQueryResult> GetById(GetConsequenceLevelQuery request)
    {
        var query = $@"
          {_mainQuery} AND CL.Id = @Id

select 
CLG.Id,
CLG.ConsequenceCategoryId,
CC.Name ConsequenceCategoryName,
CC.Description
FROM [RiskManagement].[ConsequenceLevel] CL
INNER JOIN RiskManagement.RiskConsequence CLG on CLG.ConsequenceLevelId = CL.Id AND CLG.ActiveStatusId<>3
INNER JOIn RiskManagement.ConsequenceCategory CC on CC.Id = CLG.ConsequenceCategoryId AND CC.ActiveStatusId<>3
WHERE CL.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var multi = await connection.QueryMultipleAsync(query, new { request.Id });
        var result = await multi.ReadFirstOrDefaultAsync<GetConsequenceLevelQueryResult>() ?? throw SimaResultException.NotFound;
        result.ConsequencLevelCategoryList = await multi.ReadAsync<GetConsequenceLevelCategoryQueryResult>();
        result.NullCheck();
        return result;
    }
}