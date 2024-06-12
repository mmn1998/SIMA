using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
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
            
                string queryCount = @" WITH Query as(
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
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
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
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetBrokerQueryResult>();
                return Result.Ok(response, request, count);
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
