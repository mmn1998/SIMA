using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Args;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Roles.Entities
{
    public class RolePermission
    {
        private RolePermission() { }


        public RolePermission(CreateRolePermissionArg arg)
        {
            Id = new RolePermissionId(IdHelper.GenerateUniqueId());
            RoleId = new RoleId(arg.RoleId);
            PermissionId = new PermissionId(arg.PermissionId);
            ActiveStatusId = arg.ActiveStatusId;
            ActiveFrom = arg.ActiveFrom;
            ActiveTo = arg.ActiveTo;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RolePermission> Create(CreateRolePermissionArg arg)
        {

            return new RolePermission(arg);
        }
        public async Task Modify(ModifyRolePermissionArg arg)
        {
            RoleId = new RoleId(arg.RoleId);
            PermissionId = new PermissionId(arg.PermissionId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }
        public RolePermissionId Id { get; private set; }

        public RoleId RoleId { get; private set; }

        public PermissionId PermissionId { get; private set; }

        public long ActiveStatusId { get; private set; }

        public DateOnly? ActiveFrom { get; private set; }

        public DateOnly? ActiveTo { get; private set; }

        public DateTime? CreatedAt { get; private set; }

        public long? CreatedBy { get; private set; }

        public byte[]? ModifiedAt { get; private set; }

        public long? ModifiedBy { get; private set; }

        public virtual Permission? Permission { get; private set; }

        public virtual Role Role { get; private set; } = null!;
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
    }
}
