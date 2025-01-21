using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Common.Exceptions;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Diagrams;

public class RiskDiagramQueryRepository : IRiskDiagramQueryRepository
{
    private readonly string _connectionString;
    public RiskDiagramQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<GetrRskEvaluationQueryResult> GetRiskEvaluation(GetrRskEvaluationQuery query)
    {
        using var connection = new SqlConnection(_connectionString);
        string spName = $"Exec [SP_RiskManagement_RiskEvaluation] {query.RiskId}";
        await connection.OpenAsync();
        var result = await connection.QueryAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetrRskEvaluationQueryResult>>(result.ToList()[0]);
        return test[0];
    }
}