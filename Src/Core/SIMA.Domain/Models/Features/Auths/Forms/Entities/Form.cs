using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Forms.Args;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities;

public class Form : Entity
{
    private Form() { }
    private Form(CreateFormArg arg)
    {
        Id = new FormId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Title = arg.Title;
        Code = arg.Code;
        DomainId = new(arg.DomainId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        IsSystemForm = arg.IsSystemForm;
        JsonContent = arg.JsonContent;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<Form> Create(CreateFormArg arg)
    {
        await CreateGuards(arg);
        return new Form(arg);
    }
    public async Task Modify(ModifyFormArg arg)
    {
        await ModifyGuards(arg);
        JsonContent = arg.JsonContent;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateFormArg arg)
    {

    }
    private async Task ModifyGuards(ModifyFormArg arg)
    {

    }
    #endregion
    public FormId Id { get; private set; }
    public DomainId DomainId { get; private set; }
    public virtual Domains.Entities.Domain Domain { get; private set; }

    public string? Name { get; private set; }

    public string? Title { get; private set; }
    public string? Code { get; private set; }
    public string? IsSystemForm { get; private set; }
    public string? JsonContent { get; private set; }

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
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    private List<Step> _steps = new();
    public List<Step> Steps => _steps;

    public void AddFromField(CreateFormFieldArg arg)
    {
        var entity = FormField.Create(arg);
        _formFields.Add(entity);
    }
    private List<FormField> _formFields = new();
    public List<FormField> FormFields => _formFields;
    private List<FormPermission> _formPermissions = new();
    public List<FormPermission> FormPermissions => _formPermissions;
}
