using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Brokers;

public class BrokerReadRepository : IBrokerReadRepository
{
    private readonly string _connectionString;
    public BrokerReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetBrokerQueryResult>>> GetAll(GetAllBrokerQuery request)
    {
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            

                string queryCount = @"SELECT Count(*) Result
                        FROM [Bank].[Broker] B
                        INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                        WHERE  B.ActiveStatusId != 3
                        and (@SearchValue is null OR B.[Name] like @SearchValue or B.[Code] like @SearchValue)
                        ";

                string query = $@"
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BrokerTypeId]
                              ,B.[PhoneNumber]
                              ,B.[Address]
                              ,B.[ActiveStatusId]
	                          ,FORMAT(B.[ExpireDate], 'yyyy/MM/dd', 'fa') as ExpireDatePersian
                              ,S.[Name] as ActiveStatus
                              ,b.[CreatedAt]
                       FROM [Bank].[Broker] B
                       INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                       WHERE  B.ActiveStatusId != 3
                       and (@SearchValue is null OR B.[Name] like @SearchValue or B.[Code] like @SearchValue)
                       order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                       OFFSET @Skip rows FETCH NEXT @PageSize rows only;  
                                             ";


            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetBrokerQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
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
