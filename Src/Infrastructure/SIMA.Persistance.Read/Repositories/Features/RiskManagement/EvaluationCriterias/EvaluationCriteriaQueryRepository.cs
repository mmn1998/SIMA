using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.EvaluationCriterias;

public class EvaluationCriteriaQueryRepository : IEvaluationCriteriaQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public EvaluationCriteriaQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
select 
ec.Id,
EC.Code,
EC.RiskDegreeId,
RD.Name RiskDegreeName,
ec.RiskImpactId,
RI.Name RiskImpactName,
EC.RiskPossibilityId,
RP.Name RiskPossibilityName,
a.Name ActiveStatus,
EC.CreatedAt
from
RiskManagement.EvaluationCriteria EC
join Basic.ActiveStatus A on A.ID = EC.ActiveStatusId and ec.ActiveStatusId<>3
join RiskManagement.RiskPossibility RP on RP.Id = EC.RiskPossibilityId and RP.ActiveStatusId<>3
join RiskManagement.RiskImpact RI on RI.Id = EC.RiskImpactId and RI.ActiveStatusId<>3
join RiskManagement.RiskDegree RD on RD.Id = EC.RiskDegreeId and RD.ActiveStatusId<>3
";
    }
    public async Task<Result<IEnumerable<GetEvaluationCriteriaQueryResult>>> GetAll(GetAllEvaluationCriteriasQuery request)
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
        var response = await multi.ReadAsync<GetEvaluationCriteriaQueryResult>();
        return Result.Ok(response, request, count);


    }

    public async Task<GetEvaluationCriteriaQueryResult> GetById(long id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = $@"{_mainQuery} where ec.Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<GetEvaluationCriteriaQueryResult>(query, new { Id = id }) ?? throw SimaResultException.NotFound;
    }
}
