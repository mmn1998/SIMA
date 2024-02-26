using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Branches;

public class BranchQueryRepository : IBranchQueryRepository
{
    private readonly string _connectionString;
    public BranchQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetBranchQueryResult>>> GetAll(BaseRequest baseRequest)
    {
        try
        {
            var response = new List<GetBranchQueryResult>();
            string query = string.Empty;
            int totalCount = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                if (!string.IsNullOrEmpty(baseRequest.SearchValue))
                {
                    query = @"
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.[BranchChiefOfficerId]
                              ,B.ActiveStatusId
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                            , S.[Name] as ActiveStatus
,b.[CreatedAt]
                          FROM [Bank].[Branch] B
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                        WHERE (B.[Name] like @SearchValue OR B.[Code] like @SearchValue OR B.[PhoneNumber] like @SearchValue OR
                        B.[PostalCode] like @SearchValue OR B.[Longitude] like @SearchValue OR B.[Latitude] like @SearchValue OR B.[Address] like @SearchValue OR S.[Name] like @SearchValue)
Order By b.[CreatedAt] desc  ";
                    var result = await connection.QueryAsync<GetBranchQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                    totalCount = result.Count();
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
                }
                else if (baseRequest != null && string.IsNullOrEmpty(baseRequest.SearchValue))
                {
                    query = @"
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.[BranchChiefOfficerId]
                              ,B.ActiveStatusId
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                            , S.[Name] as ActiveStatus
,b.[CreatedAt]
                          FROM [Bank].[Branch] B
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
Order By b.[CreatedAt] desc  
                        ";
                    var result = await connection.QueryAsync<GetBranchQueryResult>(query);
                    totalCount = result.Count();
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
                }
                else
                {
                    query = @"
                        SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.ActiveStatusId
                              ,B.[BranchChiefOfficerId]
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                            , S.[Name] as ActiveStatus
,b.[CreatedAt]
                          FROM [Bank].[Branch] B
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
Order By b.[CreatedAt] desc  ";
                    response = (await connection.QueryAsync<GetBranchQueryResult>(query)).ToList();

                }
            }
            return Result.Ok(response, totalCount);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<GetBranchQueryResult> GetById(long id)
    {
        string query = string.Empty;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            query = @"
                SELECT DISTINCT B.[Id]
                              ,B.[Name]
                              ,B.[Code]
                              ,B.[BranchTypeId]
                              ,B.[BranchChiefOfficerId]
                              ,B.ActiveStatusId
                              ,B.[BranchDeputyId]
                              ,B.[Latitude]
                              ,B.[Longitude]
                              ,B.[LocationId]
                              ,B.[PhoneNumber]
                              ,B.[PostalCode]
                              ,B.[Address]
                              ,B.[IsMultiCurrencyBranch]
                            , S.[Name] as ActiveStatus
                          FROM [Bank].[Branch] B
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = B.ActiveStatusId
                  WHERE B.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetBranchQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;
            return result;
        }

    }
}
