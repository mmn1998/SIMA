using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Args;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Interfaces;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;

public class DocumentExtension : Entity
{
    private DocumentExtension() { }
    private DocumentExtension(CreateDocumentExtensionArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DocumentExtension> Create(CreateDocumentExtensionArg arg, IDocumentExtensionDomainService service)
    {
        await CreateGuards(arg, service);
        return new DocumentExtension(arg);
    }

    public async Task Modify(ModifyDocumentExtensionArg arg, IDocumentExtensionDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    public static async Task CreateGuards(CreateDocumentExtensionArg arg, IDocumentExtensionDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    public async Task ModifyGuards(ModifyDocumentExtensionArg arg, IDocumentExtensionDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public DocumentExtensionId Id { get; private set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public virtual ICollection<Document> Documents { get; set; }
    public virtual ICollection<WorkflowDocumentExtension> WorkflowDocumentExtensions { get; set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
