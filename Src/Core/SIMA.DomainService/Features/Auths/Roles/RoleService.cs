using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Domain.Models.Features.Auths.Roles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Persistance.Persistence;
using System.Data.SqlClient;

namespace SIMA.DomainService.Features.Auths.Roles;

public class RoleService : IRoleService
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    
    public RoleService(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();

    }

    public async Task<bool> IsRoleSatisfied(string code, string englishKey, long id)
    {
        bool result = false;
        if (id > 0)
            result = await _context.Roles.AnyAsync(b => b.Code == code && b.EnglishKey == englishKey && b.Id != new RoleId(id));
        else
            result = await _context.Roles.AnyAsync(b => b.Code == code && b.EnglishKey == englishKey);
        return result;
    }

    public async Task<List<long>> GetRolePermissonByFormId(long formId , long roleId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                            select 
                            RP.PermissionId 
                            from Authentication.Form F
                            join Authentication.FormPermission FP on F.Id = FP.FormId
                            join Authentication.RolePermission RP on RP.PermissionId = FP.PermissionId
                            where F.Id = @formId and RP.RoleId = @roleId and FP.ActiveStatusId <> 3 and RP.ActiveStatusId <> 3
							";
            using (var multi = await connection.QueryMultipleAsync(mainQuery, new { formId , roleId }))
            {
                var response = await multi.ReadAsync<long>();
                return response.ToList();
            }

        }
    }
}
