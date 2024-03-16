using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Args;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Interfaces;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;

public class DocumentType : Entity
{
    private DocumentType() { }
    private DocumentType(CreateDocumentTypeArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DocumentType> Create(CreateDocumentTypeArg arg, IDocumentTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new DocumentType(arg);
    }
    public async Task Modify(ModifyDocumentTypeArg arg, IDocumentTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    public static async Task CreateGuards(CreateDocumentTypeArg arg, IDocumentTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    public async Task ModifyGuards(ModifyDocumentTypeArg arg, IDocumentTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
    public DocumentTypeId Id { get; private set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public virtual ICollection<Document> Documents { get; set; }
    public virtual ICollection<WorkflowDocumentType> WorkflowDocumentTypes { get; set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
