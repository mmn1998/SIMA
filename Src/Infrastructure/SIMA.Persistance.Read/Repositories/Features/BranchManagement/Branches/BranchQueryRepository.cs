using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Branches;

public class BranchQueryRepository : IBranchQueryRepository
{
    private readonly string _connectionString;
    public BranchQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetBranchQueryResult>>> GetAll(GetAllBranchQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
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
                        ,S.[Name] as ActiveStatus
				  ,b.[CreatedAt]
FROM [Bank].[Branch] B
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
                        ,S.[Name] as ActiveStatus
				  ,b.[CreatedAt]
FROM [Bank].[Branch] B
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
                var response = await multi.ReadAsync<GetBranchQueryResult>();
                return Result.Ok(response, request, count);
            }

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



