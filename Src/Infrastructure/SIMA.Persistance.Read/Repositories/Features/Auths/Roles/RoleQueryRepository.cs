using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Application.Query.Contract.Features.Auths.Roles;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Roles;

public class RoleQueryRepository : IRoleQueryRepository
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    public RoleQueryRepository(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetRoleQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT R.[ID] as Id
                  ,R.[Name]
                  ,R.[Code]
                  ,R.[ActiveStatusId]
                  ,A.[Name] as ActiveStatus
              FROM [Authentication].[Role] R
              join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
              WHERE R.[ActiveStatusID] <> 3 AND R.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetRoleQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
    public async Task<Result<IEnumerable<GetRoleQueryResult>>> GetAll(GetAllRoleQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
						  SELECT DISTINCT R.[ID] as Id
     ,R.[Name]
     ,R.[Code]
     ,R.[ActiveStatusId]
     ,A.[Name] as ActiveStatus
     ,r.[CreatedAt]
FROM [Authentication].[Role] R
join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
WHERE [ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@" WITH Query as(
							SELECT DISTINCT R.[ID] as Id
     ,R.[Name]
     ,R.[Code]
     ,R.[ActiveStatusId]
     ,A.[Name] as ActiveStatus
     ,r.[CreatedAt]
FROM [Authentication].[Role] R
join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
WHERE [ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetRoleQueryResult>();
                return Result.Ok(response, request, count);
            }
        }

    }
    public async Task<Result<List<GetRolePermissionQueryResult>>> GetRolePermission(long roleId , long formId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                            Select 
                             R.Id RoleId
                            ,F.Id FormId 
                            ,F.Name FormName 
                            ,F.Title FormTitle
                            ,P.Name PermissionName 
                            ,P.Id PermissionId
                            from Authentication.Role R
                            join Authentication.RolePermission RP on RP.RoleId = R.Id and RP.ActiveStatusId <>3
                            join Authentication.FormPermission FP on FP.PermissionId= RP.PermissionId and FP.ActiveStatusId <>3
                            join Authentication.Permission P on RP.PermissionId = P.Id and P.ActiveStatusId <>3
                            join Authentication.Form F on FP.FormId = F.Id and F.ActiveStatusId <>3
                            where FP.FormId = @formId and RP.RoleId = @roleId 
                                ";
            var result = await connection.QueryAsync<GetRolePermissionQueryResult>(query, new { roleId , formId });
            return Result.Ok(result.ToList());
        }
    }
    public async Task<bool> IsRoleSatisfied(string code, string englishKey)
    {
        return !await _context.Roles.AnyAsync(r => r.Code == code && r.EnglishKey == englishKey);
    }
    public async Task<GetRoleAggregateResult> GetRoleAggegate(long roleId)
    {
        var response = new GetRoleAggregateResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = $@"
                          SELECT DISTINCT R.[ID]
                          ,R.[Name]
                          ,R.[Code]
                          ,R.[ActiveStatusID]
                          ,R.[EnglishKey]
                          ,A.[Name] as ActiveStatus
                          FROM [Authentication].[Role] R
                          join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
                          WHERE R.ID = @RoleId and R.[ActiveStatusID] <> 3
                          GROUP BY R.ID,R.Name,R.Code,R.ActiveStatusID,A.Name,R.EnglishKey;

                         Select 
	                           F.Id  FormId
	                          ,F.Name FormName 
							  ,f.Title FormTitle
	                          ,D.Id DomainId
	                          ,D.Name DomainName 
	                          from Authentication.Role R
	                          join Authentication.FormRole FR on FR.RoleId = R.Id
	                          join Authentication.Form F on F.Id = FR.FormId 
	                          join Authentication.DomainForms DF on F.Id  = DF.FormId
	                          join Authentication.Domain D on DF.DomainId = D.Id
	                     where R.ID = @RoleId and FR.ActiveStatusId <> 3


                        Select
							 U.Id UserId
							,U.Username 
							,R.Id RoleId
							,R.Name RoleName
							,P.Id ProfileId
							,P.FirstName + ' '+ P.LastName FullName
							,P.NationalID 
							from Authentication.Role R
							join Authentication.UserRole UR on UR.RoleId = R.Id and UR.ActiveStatusId <> 3
							join Authentication.Users U on U.Id = UR.UserId and U.ActiveStatusId <> 3
							join Authentication.Profile P on U.ProfileId =P.Id
						where R.Id = @RoleId 


                            Select 
                             R.Id RoleId
                            ,FP.FormId FormId 
                            ,F.Title FormTitle
                            ,P.Name PermissionName 
                            ,P.Id PermissionId
                            from Authentication.Role R
                            join Authentication.RolePermission RP on RP.RoleId = R.Id and RP.ActiveStatusId <>3
                            join Authentication.FormPermission FP on FP.PermissionId= RP.PermissionId and FP.ActiveStatusId <>3
                            join Authentication.Form F on F.Id = FP.FormId and F.ActiveStatusId <> 3
                            join Authentication.Permission P on RP.PermissionId = P.Id and P.ActiveStatusId <>3
                            where RP.RoleId = @roleId ";


            using (var multi = await connection.QueryMultipleAsync(query, new { RoleId = roleId }))
            {
                response = multi.ReadAsync<GetRoleAggregateResult>().GetAwaiter().GetResult().Single();
                var formRoles = multi.ReadAsync<GetFormRoleQuery>().GetAwaiter().GetResult().ToList();
                response.RoleUsers = multi.ReadAsync<GetRoleUserQuery>().GetAwaiter().GetResult().ToList();
                var rolePermissions = multi.ReadAsync<GetRolePermissionQueryResult>().GetAwaiter().GetResult().ToList();

                var formPermission = formRoles.Select(form => new GetRoleFormPermissions
                {
                    Form = form,
                    Permissions = rolePermissions.Where(p => p.FormId == form.FormId).ToList()
                }).ToList();

                response.FormPermissions = formPermission;

            }
        }
        return response;
    }
}
