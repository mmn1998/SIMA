using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Permissions;
using SIMA.Domain.Models.Features.Auths.Permissions.Args;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    private readonly SIMADBContext _context;

    public PermissionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> AddAllPermissionsFromAUserToAnotherUser(long ownUserId, long targetUserId)
    {
        var targetUser = await _context.Users.Include(u => u.UserPermissions).FirstOrDefaultAsync(u => u.Id == new UserId(targetUserId));
        if (targetUser is null) throw new SimaResultException(CodeMessges._100051Code, Messages.UserNotFoundError);
        if (targetUser.ActiveStatusId == 2 || targetUser.ActiveStatusId == 4) throw new SimaResultException(CodeMessges._100009Code, Messages.UserIsDeactiveError);
        if (targetUser.ActiveStatusId == 3) throw new SimaResultException(CodeMessges._100008Code, Messages.UserIsDeletedError);
        var allPermissions = await _context.UserPermissions.Where(up => up.UserId == new UserId(ownUserId)).ToListAsync();
        var TargetUserPermissions = await _context.UserPermissions.Where(up => up.UserId == new UserId(targetUserId)).ToListAsync();
        foreach (var item in TargetUserPermissions)
        {
            allPermissions = allPermissions.Where(i => i.PermissionId != item.PermissionId).ToList();
        }
        var createUserPermissionArgs = allPermissions.Select(i => new CreateUserPermissionArg
        {
            UserId = targetUserId,
            PermissionId = i.Id.Value
        }).ToList();
        await targetUser.AddUserPermission(createUserPermissionArgs, targetUserId);
        return true;
    }

    public async Task<bool> AddAllPermissionsToAUser(long userId)
    {
        var user = await _context.Users.Include(u => u.UserPermissions).FirstOrDefaultAsync(u => u.Id == new UserId(userId));
        if (user is null) throw new SimaResultException(CodeMessges._100051Code, Messages.UserNotFoundError);
        if (user.ActiveStatusId == 2 || user.ActiveStatusId == 4) throw new SimaResultException(CodeMessges._100007Code, Messages.UserIsDeactiveError);
        if (user.ActiveStatusId == 3) throw new SimaResultException(CodeMessges._100008Code, Messages.UserIsDeletedError);
        var allPermissions = await _context.Permissions/*.Where(p => p.Id != new PermissionId(32767))*/.ToListAsync();
        var thisUserPermissions = await _context.UserPermissions.Where(up => up.UserId == new UserId(userId)).ToListAsync();
        foreach (var item in thisUserPermissions)
        {
            allPermissions = allPermissions.Where(i => i.Id != item.PermissionId).ToList();
        }
        var createUserPermissionArgs = allPermissions.Select(i => new CreateUserPermissionArg
        {
            UserId = userId,
            PermissionId = i.Id.Value,
            ActiveStatusId = 1
        }).ToList();

        await user.AddUserPermission(createUserPermissionArgs, userId);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CreatePermissionArg>> AddPermissionNotExist()
    {
        var enumList = Enum.GetValues(typeof(PermissionForAddEnum)).Cast<PermissionForAddEnum>();
        var AddPermissionList = new List<CreatePermissionArg>();
        foreach (var enumValue in enumList)
        {
            
            var enumField = typeof(PermissionForAddEnum).GetField(enumValue.ToString());
            var eunmCode = Convert.ToInt32(enumValue);
            var displayAttribute = enumField.GetCustomAttribute<DisplayAttribute>();

            var name = enumValue.ToString();
            var groupName = displayAttribute?.GroupName ?? string.Empty;
            var description = displayAttribute?.Description ?? string.Empty;

            var exists = await _context.Permissions.Where(s => s.Code == eunmCode.ToString()).FirstOrDefaultAsync();
            if (exists is null)
            {
                CreatePermissionArg arg = new CreatePermissionArg();

                arg.Name = description;
                arg.Code = eunmCode.ToString();
                arg.EnglishKey = name;
                arg.DomainId = 17;
                arg.CreatedAt = DateTime.Now;
                arg.CreatedBy = 9999;
                arg.ActiveStatusId = 1;
                AddPermissionList.Add(arg);
            }
            
        }
        return AddPermissionList;
    }
    public async Task<Permission> GetById(long id)
    {
        var entity = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == new PermissionId(id));
        entity.NullCheck();
        return entity;
    }

}
