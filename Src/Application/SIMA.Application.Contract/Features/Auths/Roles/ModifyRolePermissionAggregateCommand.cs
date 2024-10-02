using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Auths.Roles
{
    public class ModifyRolePermissionAggregateCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? EnglishKey { get; set; }
        public string? Code { get; set; }
        public List<CreateRolePermissionCommand>? RolePermissions { get; set; }
        public List<CreateFormRoleCommand>? FormRoles { get; set; }
    }
}
