using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
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

    public async Task<Result<IEnumerable<GetStaffQueryResult>>> GetAll(GetAllStaffQuery? request = null)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
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
                        WHERE  S.[ActiveStatusID] <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
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
                        WHERE  S.[ActiveStatusID] <> 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetStaffQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                string queryCount = @"
                SELECT COUNT(*) Result
                FROM [Organization].[Staff] S
                INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
                INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
                INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
                INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
                join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
                WHERE  S.[ActiveStatusID] <> 3
                AND(@SearchValue is null OR P.FirstName like @SearchValue OR P.LastName like @SearchValue OR S.StaffNumber like @SearchValue OR C.Name like @SearchValue OR D.Name like @SearchValue)";
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
                WHERE  S.[ActiveStatusID] <> 3
                AND (@SearchValue is null OR P.FirstName like @SearchValue OR P.LastName like @SearchValue OR S.StaffNumber like @SearchValue OR C.Name like @SearchValue OR D.Name like @SearchValue)
                order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetStaffQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }

    public async Task<bool> IsStaffSatisfied(long profileId, long positionId)
    {
        return !await _context.Staff.AnyAsync(s => s.ProfileId == new ProfileId(profileId) && s.PositionId == new PositionId(positionId));
    }
}
