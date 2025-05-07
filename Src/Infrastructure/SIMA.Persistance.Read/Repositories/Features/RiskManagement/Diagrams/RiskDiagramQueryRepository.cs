using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Common.Exceptions;
using System.Data.SqlClient;
using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;

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

    public async Task<GetMatrixAChartQueryResult> GetMatrixAChart(GetMatrixAChartQuery query)
    {
        using var connection = new SqlConnection(_connectionString);
        string spName = $"Exec [SP_RiskManagement_MatrixAChart] {query.RiskId}";
        await connection.OpenAsync();
        var result = await connection.QueryAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetMatrixAChartQueryResult>>(result.ToList()[0]);
        return test[0];
    }

    public async Task<GetSeverityQueryResult> GetSeverity(GetSeverityQuery query)
    {
        using var connection = new SqlConnection(_connectionString);
        string spName = $"Exec [SP_RiskManagement_severityChart] {query.RiskId}";
        await connection.OpenAsync();
        var result = await connection.QueryAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetSeverityQueryResult>>(result.ToList()[0]);
        return test[0];
    }

    public async Task<GetInherentOccurrenceProbabilityQueryResult> GetInherentOccurrenceProbability(GetInherentOccurrenceProbabilityQuery query)
    {
                using var connection = new SqlConnection(_connectionString);
        string spName = $"Exec [SP_RiskManagement_InherentOccurrenceProbabilityChart] {query.RiskId}";
        await connection.OpenAsync();
        var result = await connection.QueryAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetInherentOccurrenceProbabilityQueryResult>>(result.ToList()[0]);
        return test[0];
    }

    public async Task<GetRiskLevelCobitQueryResult> GetRiskLevelCobit(GetRiskLevelCobitQuery query)
    {
        using var connection = new SqlConnection(_connectionString);
        string spName = $"Exec [SP_RiskManagement_RiskLevelCobitchart] {query.RiskId}";
        await connection.OpenAsync();
        var result = await connection.QueryAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetRiskLevelCobitQueryResult>>(result.ToList()[0]);
        return test[0];
    }

    public async Task<GetCurrentOccurrenceProbabilityQueryResult> GetCurrentOccurrenceProbability(GetCurrentOccurrenceProbabilityQuery query)
    {
                using var connection = new SqlConnection(_connectionString);
        string spName = $"Exec [SP_RiskManagement_CurrentOccurrenceProbability] {query.RiskId}";
        await connection.OpenAsync();
        var result = await connection.QueryAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetCurrentOccurrenceProbabilityQueryResult>>(result.ToList()[0]);
        return test[0];
    }
}