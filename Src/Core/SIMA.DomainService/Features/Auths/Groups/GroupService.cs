using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.Goods;
using SIMA.Domain.Models.Features.Auths.Groups.Interfaces;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;
using System.Data.SqlClient;

namespace SIMA.DomainService.Features.Auths.Groups;

public class GroupService : IGroupService
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    public GroupService(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Groups.AnyAsync(b => b.Code == code && b.Id != new GroupId(id));
        else
            return !await _context.Groups.AnyAsync(b => b.Code == code);
    }
    
    public async Task<List<long>> GetGroupPermissonByFormId(long formId , long groupId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                            select 
                            GP.PermissionId 
                            from Authentication.Form F
                            join Authentication.FormPermission FP on F.Id = FP.FormId
                            join Authentication.GroupPermission GP on GP.PermissionId = FP.PermissionId
                            where F.Id = @formId and GP.GroupId = @groupId  and FP.ActiveStatusId <> 3 and GP.ActiveStatusId <> 3
							";
            using (var multi = await connection.QueryMultipleAsync(mainQuery, new { formId , groupId }))
            {
                var response = await multi.ReadAsync<long>();
                return response.ToList();
            }

        }
    }
}
