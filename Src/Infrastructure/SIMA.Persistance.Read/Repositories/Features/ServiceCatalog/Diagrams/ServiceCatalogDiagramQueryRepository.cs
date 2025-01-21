using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Common.Exceptions;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Diagrams;

public class ServiceCatalogDiagramQueryRepository : IServiceCatalogDiagramQueryRepository
{
    private readonly string _connectionString;
    public ServiceCatalogDiagramQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<List<GetServiceNetworkDiagramQueryResult>> GetNetworkDiagrams(GetServiceNetworkDiagramQuery query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceNetworkDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceNetworkDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type = query.Type.HasValue ? query.Type.Value.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_ServiceNetworkDiagram] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetServiceNetworkDiagramQueryResultWrapper>>(result);
        return test[0].data;
    }

    public async Task<List<GetServiceTreeDiagramQueryResult>> GetTreeDiagrams(GetServiceTreeDiagramQuery query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type = query.Type.HasValue ? query.Type.Value.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetServiceTreeDiagramQueryResultWrapper>>(result);
        return test[0].data;
    }
}
