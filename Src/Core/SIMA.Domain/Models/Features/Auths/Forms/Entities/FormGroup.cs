using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class FormGroup
{
    private FormGroup() { }


    public FormGroupId Id { get; private set; }
    public FormId FormId { get; private set; }
    public Form Form { get; private set; }
    public GroupId GroupId { get; private set; }
    public Group Group { get; private set; }
    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
}
