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
        try
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
        catch (Exception ex)
        {

            throw;
        }
      
    }

    public async Task<List<GetProductListResult>> GetProductDiagrams(GetProductList query)
    {
        try
        {
             using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetProdoctList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        string json = "{\r\n            \"\"data\"\": [\r\n                {\r\n                    \"\"id\"\": 1794466611,\r\n                    \"\"name\"\": \"\"test2\"\",\r\n                    \"\"code\"\": \"\"138200584263\"\",\r\n                    \"\"scope\"\": \"\"1234\"\",\r\n                    \"\"description\"\": \"\"test\"\",\r\n                    \"\"ProviderCompanyId\"\": 1623452081,\r\n                    \"\"providerCompanyName\"\": \"\"به پرداخت ملت\"\",\r\n                    \"\"serviceStatusId\"\": 4658969395,\r\n                    \"\"inServiceDate\"\": \"\"2024-11-17\"\",\r\n                    \"\"serviceStatusName\"\": \"\"بازنشسته\"\",\r\n                    \"\"ActiveStatus\"\": \"\"غیر فعال دائم\"\",\r\n                    \"\"createdAt\"\": \"\"2024-11-16T18:33:37.283\"\"\r\n                }\r\n            ]\r\n        }";
        var test = JsonConvert.DeserializeObject<GetProductListResultWrapper>(result);
        return test.Data;
        }
        catch (Exception ex)
        {

            throw;
        }
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
       
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

    public async Task<List<GetConfigurationItemListResult>> GetConfigurationItemList(GetConfigurationItemList query)
    {
        //using var connection = new SqlConnection(_connectionString);
        //string spName = "Exec [dbo].[SP_ServiceCatalog_ServiceTreeDiagram]";
        //await connection.OpenAsync();
        //return await connection.QueryAsync<GetServiceTreeDiagramQueryResult>(spName, param: new { id = query.Id, type = query.Type }, commandType: CommandType.StoredProcedure);
        using var connection = new SqlConnection(_connectionString);
        var id = query.Id.HasValue ? query.Id.Value.ToString() : "NULL";
        var type =!string.IsNullOrEmpty(query.Type) ? query.Type.ToString() : "NULL";
        string spName = $"Exec [dbo].[SP_ServiceCatalog_GetConfigurationItemList] {id},{type}";
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<string>(spName) ?? throw SimaResultException.NotFound;
        var test = JsonConvert.DeserializeObject<List<GetConfigurationItemListWrapperResult>>(result);
        return test[0].Data;
    }
}
