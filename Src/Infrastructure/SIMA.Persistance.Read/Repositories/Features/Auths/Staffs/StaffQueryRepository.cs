using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
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
      		,(P.FirstName + ' ' + P.LastName) as FullName
      		,D.Name as DepartmentName
            ,D.Id as DepartmentId
      		,C.Name as CompanyName
            ,C.Id as CompanyId
             ,Po.Name PositionName
             ,Po.Id PositionId
      		,S.[ActiveStatusID]
      		,A.[Name] as ActiveStatus
            ,P.Id ProfileId
      		,s.[CreatedAt]
            ,S.ManagerId
            ,(PP.FirstName + ' ' + PP.LastName) as ManagerFullName
            ,B.Id BranchId
            ,B.Name BranchName
            ,PL.Id PositionLevelId
            ,PL.Name PositionLevelName
FROM [Organization].[Staff] S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
Inner Join [Organization].[Position] Po on Po.Id = S.PositionId
LEFT JOIN Bank.Branch B ON Po.BranchId = B.Id and B.ActiveStatusId<>3
left join Organization.PositionLevel PL on pl.Id = po.PositionLevelId and PL.ActiveStatusId<>3
join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
LEFT JOIN [Organization].[Staff] SP On SP.Id = S.ManagerId and SP.ActiveStatusId<>3
LEFT JOIN [Authentication].[Profile] PP on PP.ID = SP.ProfileID
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
            string mainQuery = @"
SELECT DISTINCT S.ID as Id
      		,S.StaffNumber
      		,(P.FirstName + ' ' + P.LastName) as FullName
      		,D.Name as DepartmentName
            ,D.Id as DepartmentId
      		,C.Name as CompanyName
            ,C.Id as CompanyId
             ,Po.Name PositionName
             ,Po.Id PositionId
      		,S.[ActiveStatusID]
      		,A.[Name] as ActiveStatus
            ,P.Id ProfileId
      		,s.[CreatedAt]
            ,S.ManagerId
            ,(PP.FirstName + ' ' + PP.LastName) as ManagerFullName
            ,B.Id BranchId
            ,B.Name BranchName
            ,PL.Id PositionLevelId
            ,PL.Name PositionLevelName
FROM [Organization].[Staff] S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
Inner Join [Organization].[Position] Po on Po.Id = S.PositionId
LEFT JOIN Bank.Branch B ON Po.BranchId = B.Id and B.ActiveStatusId<>3
left join Organization.PositionLevel PL on pl.Id = po.PositionLevelId and PL.ActiveStatusId<>3
join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
LEFT JOIN [Organization].[Staff] SP On SP.Id = S.ManagerId and SP.ActiveStatusId<>3
LEFT JOIN [Authentication].[Profile] PP on PP.ID = SP.ProfileID
WHERE S.[ActiveStatusID] <> 3
";
                string queryCount = $@" WITH Query as({mainQuery})
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
                string query = $@" WITH Query as({mainQuery})
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("DepartmentId", request.DepartmentId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetStaffQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
    }

    public async Task<IEnumerable<GetStaffQueryResult>> GetAllByDepartmentId(long? departmentId)
    {
        var query = @"
SELECT DISTINCT S.ID as Id
      		,S.StaffNumber
      		,(P.FirstName + ' ' + P.LastName) as FullName
      		,D.Name as DepartmentName
            ,D.Id as DepartmentId
      		,C.Name as CompanyName
            ,C.Id as CompanyId
             ,Po.Name PositionName
             ,Po.Id PositionId
      		,S.[ActiveStatusID]
      		,A.[Name] as ActiveStatus
            ,P.Id ProfileId
      		,s.[CreatedAt]
            ,S.ManagerId
            ,(PP.FirstName + ' ' + PP.LastName) as ManagerFullName
            ,B.Id BranchId
            ,B.Name BranchName
            ,PL.Id PositionLevelId
            ,PL.Name PositionLevelName
FROM [Organization].[Staff] S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
INNER JOIN [Authentication].[Users] U on U.ProfileID = P.ID
INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
INNER JOIN [Organization].[Department] D on D.CompanyID = C.ID
Inner Join [Organization].[Position] Po on Po.Id = S.PositionId
LEFT JOIN Bank.Branch B ON Po.BranchId = B.Id and B.ActiveStatusId<>3
left join Organization.PositionLevel PL on pl.Id = po.PositionLevelId and PL.ActiveStatusId<>3
join [Basic].[ActiveStatus] A on A.Id = S.ActiveStatusID
LEFT JOIN [Organization].[Staff] SP On SP.Id = S.ManagerId and SP.ActiveStatusId<>3
LEFT JOIN [Authentication].[Profile] PP on PP.ID = SP.ProfileID
WHERE S.[ActiveStatusID] <> 3 and D.Id = @departmentId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryAsync<GetStaffQueryResult>(query, new {departmentId});
        return result;
    }

    public async Task<bool> IsStaffSatisfied(long profileId, long positionId)
    {
        return !await _context.Staff.AnyAsync(s => s.ProfileId == new ProfileId(profileId) && s.PositionId == new PositionId(positionId));
    }

    public async Task<long> GetStaffIdByUserId(long userId)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                       select
                            S.Id 
                            from Organization.Staff S
                            join Authentication.Profile P on s.ProfileId = P.Id 
                            join Authentication.Users U on U.ProfileID = P.Id 
                       where U.Id = @userId  
                        ";
            var result = await connection.QueryFirstOrDefaultAsync<long>(query, new { userId });
            result.NullCheck();
            return result;
        }
    }

    public async Task<GetStaffByStaffNumberQueryResult> FindByStaffNumber(string code)
    {
        var response = new GetStaffByStaffNumberQueryResult();
        var query = @"
SELECT 
	S.Id
	,P.FirstName
	,P.LastName
	,S.StaffNumber
	,S.CreatedAt
	,A.Name ActiveStatus
FROM Organization.Staff S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
INNER JOIN Basic.ActiveStatus A on A.Id = S.ActiveStatusId
WHERE S.StaffNumber = @StaffNumber and S.ActiveStatusId<>3

-- branch
SELECT 
DC.Id,
DC.Name,
DC.Code
FROM Organization.Staff S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID
INNER JOIN Organization.Position Po on Po.ID = S.PositionId
INNER JOIN Bank.Branch DC on DC.Id = Po.BranchId  and DC.ActiveStatusId<>3
WHERE S.StaffNumber = @StaffNumber and S.ActiveStatusId<>3

-- department
SELECT 
DC.Id,
DC.Name,
DC.Code
FROM Organization.Staff S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID and P.ActiveStatusId<>3
INNER JOIN Organization.Position Po on Po.ID = S.PositionId	and Po.ActiveStatusId<>3
INNER JOIN Organization.Department DC on DC.Id = Po.DepartmentId  and DC.ActiveStatusId<>3
WHERE S.StaffNumber = @StaffNumber and S.ActiveStatusId<>3

-- compnay
SELECT 
C.Id,
C.Name,
C.Code
FROM Organization.Staff S
INNER JOIN [Authentication].[Profile] P on P.ID = S.ProfileID and P.ActiveStatusId<>3
INNER JOIN Organization.Position Po on Po.ID = S.PositionId and Po.ActiveStatusId<>3
INNER JOIN Organization.Department DC on DC.Id = Po.DepartmentId  and DC.ActiveStatusId<>3
INNER JOIN Organization.Company C on C.Id = DC.CompanyId and C.ActiveStatusId<>3
WHERE S.StaffNumber = @StaffNumber and S.ActiveStatusId<>3
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var multi = await connection.QueryMultipleAsync(query, new { Code = code });
        response = await multi.ReadFirstOrDefaultAsync<GetStaffByStaffNumberQueryResult>() ?? throw SimaResultException.NotFound;
        response.BranchInfo = await multi.ReadFirstOrDefaultAsync<BranchInfo>();
        response.DepartmentInfo = await multi.ReadFirstOrDefaultAsync<DepartmentInfo>();
        response.CompanyInfo = await multi.ReadFirstOrDefaultAsync<CompanyInfo>();
        return response;
    }
}
