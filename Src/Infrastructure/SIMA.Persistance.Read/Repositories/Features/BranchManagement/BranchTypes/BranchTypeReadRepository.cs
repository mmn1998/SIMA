using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.BranchTypes;

public class BranchTypeReadRepository : IBranchTypeReadRepository
{
    private readonly string _connectionString;
    public BranchTypeReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<List<GetBranchTypeQueryResult>> GetAll(BaseRequest request)
    {
        var result = new List<GetBranchTypeQueryResult>();
        string query = string.Empty;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                query = @"
                            SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                                  ,BT.[ActiveStatusID]
                            	  ,A.Name ActiveStatus
,bt.[CreatedAt]
                              FROM [Bank].[BranchType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
                              WHERE (PT.Name like @SearchValue OR BT.[Code] like @SerachValue OR A.[Name] like @SerachValue)
Order By bt.[CreatedAt] desc  
                            ";
            }
            else
            {
                query = @"
                            SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                                  ,BT.[ActiveStatusID]
                            	  ,A.Name ActiveStatus
,bt.[CreatedAt]
                              FROM [Bank].[BranchType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID 
Order By bt.[CreatedAt] desc  
                            ";
            }
            result = (await connection.QueryAsync<GetBranchTypeQueryResult>(query, new { SearchValue = "%" + request.SearchValue + "%" }))
                .Skip((request.Skip - 1) * request.Take)
                .Take(request.Take)
                .ToList();
        }
        return result;
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
