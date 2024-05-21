using SIMA.Domain.Models.Features.Auths.Forms.Args;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class FormField
{
    private FormField() { }
    private FormField(CreateFormFieldArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        FormId = new(arg.FormId);
        Name = arg.Name;
        Code = arg.Code;
        Type = arg.Type;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static FormField Create(CreateFormFieldArg arg)
    {
        return new FormField(arg);
    }
    public FormFieldId Id { get; private set; }
    public FormId FormId { get; private set; }
    public virtual Form Form { get; private set; }
    public string Name { get; private set; }
    public string? Code { get; private set; }
    public string? Type { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
