using SIMA.Domain.Models.Features.Auths.Domains.Args;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Domains.Entities;

public class Domain
{
    private Domain() { }
    private Domain(CreateDomainArg arg)
    {
        Id = new DomainId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Domain> Create(CreateDomainArg arg)
    {
        return new Domain(arg);
    }
    public DomainId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    private List<MainAggregate> _mainEntities = new();
    public ICollection<MainAggregate> MainEntities => _mainEntities;

    private List<Permission> _permissions = new();
    public ICollection<Permission> Permissions => _permissions;

    private List<UserDomainAccess> _userDomainAccesses = new();
    public ICollection<UserDomainAccess> UserDomainAccesses => _userDomainAccesses;
    private List<Project> _projects = new();
    public ICollection<Project> Projects => _projects;

    private List<Form> _forms = new();
    public ICollection<Form> Forms => _forms;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
