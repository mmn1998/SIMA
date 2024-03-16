using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.BrokerTypes;

public class BrokerTypeReadRepository : IBrokerTypeReadRepository
{
    private readonly string _connectionString;
    public BrokerTypeReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetBrokerTypeQueryResult>>> GetAll(GetAllBrokerTypesQuery request)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {

            string queryCount = @"
                            SELECT Count(*) Result
                            FROM [Bank].[BrokerType] BT
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
                            WHERE  BT.ActiveStatusId != 3
                            and (@SearchValue is null OR BT.[Name] like @SearchValue or BT.[Code] like @SearchValue)
                            ";

            await connection.OpenAsync();
             
            
               string query = $@"
                            SELECT DISTINCT BT.[ID]
                                 ,BT.[Name]
                                 ,BT.[Code]
                              	 ,A.Name ActiveStatus
                              	 ,BT.ActiveStatusId 
                                 ,bt.[CreatedAt]
                             FROM [Bank].[BrokerType] BT
                             INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
                             WHERE  BT.ActiveStatusId != 3
                             and (@SearchValue is null OR BT.[Name] like @SearchValue or BT.[Code] like @SearchValue)
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
                var response = await multi.ReadAsync<GetBrokerTypeQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
    public async Task<GetBrokerTypeQueryResult> GetById(long id)
    {
        var result = new GetBrokerTypeQueryResult();
        string query = @"
                    SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                            	  ,A.Name ActiveStatus
                            	  ,BT.ActiveStatusId 
                              FROM [Bank].[BrokerType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID WHERE BT.Id = @Id
                    ";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            result = await connection.QueryFirstAsync<GetBrokerTypeQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;
        }
        return result;
    }
}
