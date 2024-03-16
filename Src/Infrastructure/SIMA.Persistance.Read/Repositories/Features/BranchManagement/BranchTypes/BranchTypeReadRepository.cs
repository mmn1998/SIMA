using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.BranchTypes;

public class BranchTypeReadRepository : IBranchTypeReadRepository
{
    private readonly string _connectionString;
    public BranchTypeReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetBranchTypeQueryResult>>> GetAll(GetAllBranchTypesQuery request)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {
            string queryCount = @"
                                SELECT Count(*) Result
                             FROM [Bank].[BranchType] BT
                             INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
                             WHERE  BT.ActiveStatusId != 3
                                 and (@SearchValue is null OR BT.[Name] like @SearchValue or BT.[Code] like @SearchValue)";

            await connection.OpenAsync();

            string query = $@"
                         SELECT DISTINCT BT.[ID]
                              ,BT.[Name]
                              ,BT.[Code]
                              ,BT.[ActiveStatusID]
                           	  ,A.Name ActiveStatus
                              ,bt.[CreatedAt]
                          FROM [Bank].[BranchType] BT
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
                var response = await multi.ReadAsync<GetBranchTypeQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }
    public async Task<GetBranchTypeQueryResult> GetById(long id)
    {
        var result = new GetBranchTypeQueryResult();
        string query = @"
                    SELECT DISTINCT BT.[ID]
                          ,BT.[Name]
                          ,BT.[Code]
                          ,BT.[ActiveStatusID]
                    	  ,A.Name ActiveStatus
                      FROM [Bank].[BranchType] BT
                      INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
                      WHERE BT.ID = @Id
                    ";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            result = await connection.QueryFirstAsync<GetBranchTypeQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;
        }
        return result;
    }
}
