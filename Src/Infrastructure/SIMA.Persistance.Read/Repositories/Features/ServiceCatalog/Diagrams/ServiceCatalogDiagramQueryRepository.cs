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
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_ServiceNetworkDiagram] {type},{id}";
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

    public async Task<List<GetProductListResult>> GetProductDiagrams(GetProductList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetProdoctList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetProductListResultWrapper>>(result);
        return test[0].Data;
    }

    public async Task<List<GetChannelListResult>> GetChannelDiagrams(GetChannelList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetChannelList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetChannelListResultWrapper>>(result);
        return test[0].data;
    }

    public async Task<List<GetAssetListResult>> GetAssetDiagrams(GetAssetList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetAssetList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetAssetListResultWrapper>>(result);
        return test[0].Data;
    }

    public async Task<List<GetAssignedStaffListResult>> GetAssignedStaffList(GetAssignedStaffList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetAssignedStaffList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetAssignedStaffListResultWrapper>>(result);
        return test[0].Data;
    }

    public async Task<List<GetApiListResult>> GetApiList(GetApiList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetApiList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetApiListResultWrapper>>(result);
        return test[0].Data;
    }

    public async Task<List<GetProcedureListResult>> GetProcedureList(GetProcedureList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetProcedureList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetProcedureListResultWrapper>>(result);
        return test[0].Data;
    }

    public async Task<List<GetRiskListResult>> GetRiskLis(GetRiskList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetRiskList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetRiskListResultWrapper>>(result);
        return test[0].Data;
    }
}
