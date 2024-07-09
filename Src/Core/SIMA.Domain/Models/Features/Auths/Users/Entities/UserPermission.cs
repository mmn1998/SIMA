using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities
{
    public class UserPermission
    {
        private UserPermission()
        {
        }

        private UserPermission(CreateUserPermissionArg arg)
        {
            Id = new UserPermissionId(IdHelper.GenerateUniqueId());
            if (arg.UserId.HasValue) UserId = new UserId(arg.UserId.Value);
            PermissionId = new PermissionId(arg.PermissionId);
            ActiveStatusId = arg.ActiveStatusId;
            ActiveFrom = arg.ActiveFrom;
            ActiveTo = arg.ActiveTo;
            CreatedAt = DateTime.Now;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<UserPermission> Create(CreateUserPermissionArg arg)
        {
            return new UserPermission(arg);
        }
        public UserPermissionId Id { get; private set; }

        public UserId? UserId { get; private set; }

        public PermissionId PermissionId { get; private set; }

        public long ActiveStatusId { get; private set; }

        public DateOnly? ActiveFrom { get; private set; }

        public DateOnly? ActiveTo { get; private set; }
        public DateTime? CreatedAt { get; set; }

        public long? CreatedBy { get; set; }
        public byte[]? ModifiedAt { get; set; }

        public long? ModifiedBy { get; set; }
        public async Task Modify(ModifyUserPermissionArg arg)
        {
            UserId = new UserId(arg.UserId.Value);
            PermissionId = new PermissionId(arg.PermissionId);
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }

        public virtual Permission? Permission { get; private set; }

        public virtual User? User { get; private set; }
        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }
    }
}
