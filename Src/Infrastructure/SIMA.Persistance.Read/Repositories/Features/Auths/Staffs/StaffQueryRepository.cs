using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Staffs;

public class StaffQueryRepository : IStaffQueryRepository
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    public StaffQueryRepository(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetStaffQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                SELECT DISTINCT S.ID as Id
                ,S.StaffNumber
                ,(P.FirstName + ' ' + P.LastName) as FulllName
                ,D.Name as DepartmentName
                ,C.Name as CompanyName
                ,S.[ActiveStatusID]
                ,A.[Name] as ActiveStatus
,s.[CreatedAt]
                FROM [Organization].[Staff] S
                INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
                INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
                INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
                INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
                join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
                WHERE S.[ActiveStatusID] <> 3 AND S.Id = @Id
Order By s.[CreatedAt] desc  ";
            var result = await connection.QueryFirstOrDefaultAsync<GetStaffQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }

    }

    public async Task<Result<List<GetStaffQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {
        var response = new List<GetStaffQueryResult>();
        int totalCount = 0;
        if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                SELECT DISTINCT S.ID as Id
                ,S.StaffNumber
                ,(P.FirstName + ' ' + P.LastName) as FulllName
                ,D.Name as DepartmentName
                ,C.Name as CompanyName
                ,S.[ActiveStatusID]
                ,A.[Name] as ActiveStatus
,s.[CreatedAt]
                FROM [Organization].[Staff] S
                INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
                INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
                INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
                INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
                join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
              WHERE (P.FirstName like @SearchValue OR P.LastName like @SearchValue OR S.StaffNumber like @SearchValue OR C.Name like @SearchValue OR D.Name like @SearchValue) AND S.[ActiveStatusID] <> 3
Order By s.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetStaffQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        else if (baseRequest != null && string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                 SELECT DISTINCT S.ID as Id
                ,S.StaffNumber
                ,(P.FirstName + ' ' + P.LastName) as FulllName
                ,D.Name as DepartmentName
                ,C.Name as CompanyName
                ,S.[ActiveStatusID]
                ,A.[Name] as ActiveStatus
,s.[CreatedAt]
                FROM [Organization].[Staff] S
                INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
                INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
                INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
                INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
                join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
                WHERE S.[ActiveStatusID] <> 3
Order By s.[CreatedAt] desc  ";
                var result = await connection.QueryAsync<GetStaffQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        return Result.Ok(response, totalCount);
    }

    public async Task<bool> IsStaffSatisfied(long profileId, long positionId)
    {
        return !await _context.Staff.AnyAsync(s => s.ProfileId == new ProfileId(profileId) && s.PositionId == new PositionId(positionId));
    }
}
