using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;

public class DraftOrigin : Entity, IAggregateRoot
{
    private DraftOrigin()
    {
        
    }
    private DraftOrigin(CreateDraftOriginArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<DraftOrigin> Create(CreateDraftOriginArg arg, IDraftOriginDomainService service)
    {
        await CreateGuards(arg, service);
        return new DraftOrigin(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateDraftOriginArg arg, IDraftOriginDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyDraftOriginArg arg, IDraftOriginDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public async Task Modify(ModifyDraftOriginArg arg, IDraftOriginDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public DraftOriginId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;

    private List<InquiryRequest> _inquiryRequests = new();
    public ICollection<InquiryRequest> InquiryRequests => _inquiryRequests;

    private List<WageRate> _wageRates = new();
    public ICollection<WageRate> WageRates => _wageRates;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}