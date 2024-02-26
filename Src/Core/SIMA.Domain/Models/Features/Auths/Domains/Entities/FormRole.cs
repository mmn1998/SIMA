using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.Domains.Entities;

public class FormRole
{
    private FormRole() { }


    public FormRoleId Id { get; private set; }
    public FormId FormId { get; private set; }
    public Form  Form { get; private set; }
    public RoleId RoleId { get; private set; }
    public Role Role { get; private set; }

    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
}
