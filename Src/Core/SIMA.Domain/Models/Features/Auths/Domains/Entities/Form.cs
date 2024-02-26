using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.Domains.Entities;

public class Form
{
    private Form() { }
   
    public FormId Id { get; private set; }
    public DomainId DomainId { get; private set; }
    public virtual Domain Domain { get; private set; }

    public string? Name { get; private set; }

    public string? Title { get; private set; }
    public string? Code { get; private set; }
    public string? IsSystemForm{ get; private set; }
    public string? JsonContent{ get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    private List<FormRole> _formRoles = new();

    public ICollection<FormRole> FormRoles => _formRoles;   
    
    private List<FormUser> _formUsers = new();

    public ICollection<FormUser> FormUsers => _formUsers;

    private List<FormGroup> _formGroups = new();

    public ICollection<FormGroup> FormGroups => _formGroups;
}
