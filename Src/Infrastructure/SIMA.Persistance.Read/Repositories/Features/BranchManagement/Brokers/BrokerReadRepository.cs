using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Brokers;

public class BrokerReadRepository : IBrokerReadRepository
{
    private readonly string _connectionString;
    public BrokerReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<List<GetBrokerQueryResult>> GetAll(BaseRequest request)
    {
        var result = new List<GetBrokerQueryResult>();
        string query = string.Empty;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                query = @"
                            SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BrokerTypeId]
                              ,B.[PhoneNumber]
                              ,B.[Address]
                              ,B.[ActiveStatusId]
	                          ,FORMAT(B.[ExpireDate], 'yyyy/MM/dd', 'fa') as ExpireDatePersian
                            , S.[Name] as ActiveStatus
,b.[CreatedAt]
                          FROM [Bank].[Broker] B
                            INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                              WHERE (Name like @SearchValue OR [Code] like @SerachValue OR [PhoneNumber] like @SerachValue OR [Address] like @SerachValue OR [ExpireDate] like @SerachValue)
Order By b.[CreatedAt] desc  
                            ";
                result = (await connection.QueryAsync<GetBrokerQueryResult>(query, new { SearchValue = "%" + request.SearchValue + "@" }))
                .Skip((request.Skip - 1) * request.Take)
                .Take(request.Take)
                .ToList();
            }
            else
            {
                query = @"
                            SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BrokerTypeId]
                              ,B.[PhoneNumber]
                              ,B.[Address]
                              ,B.[ActiveStatusId]
	                          ,FORMAT(B.[ExpireDate], 'yyyy/MM/dd', 'fa') as ExpireDatePersian
                            , S.[Name] as ActiveStatus
,b.[CreatedAt]
                          FROM [Bank].[Broker] B
                            INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
Order By b.[CreatedAt] desc  
                            ";
                result = (await connection.QueryAsync<GetBrokerQueryResult>(query))
                .Skip((request.Skip - 1) * request.Take)
                .Take(request.Take)
                .ToList();
            }
        }
        return result;
    }

    public async Task<GetBrokerQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BrokerTypeId]
                              ,B.[PhoneNumber]
                              ,B.[Address]
                              ,B.[ActiveStatusId]
	                          ,FORMAT(B.[ExpireDate], 'yyyy/MM/dd', 'fa') as ExpireDatePersian
                            , S.[Name] as ActiveStatus
                          FROM [Bank].[Broker] B
                            INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                            WHERE [Id] = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetBrokerQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
}
