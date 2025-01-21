using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.Responses.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;

public class InquiryRequestCurrency : Entity
{
    private InquiryRequestCurrency()
    {

    }
    private InquiryRequestCurrency(CreateInquiryRequestCurrencyArg arg)
    {
        Id = new(arg.Id);
        CurrencyTypeId = new(arg.CurrencyTypeId);
        InquiryRequestId = new(arg.InquiryRequestId);
        ActiveStatusId = arg.ActiveStatusId;
        Amount = arg.Amount;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static InquiryRequestCurrency Create(CreateInquiryRequestCurrencyArg arg)
    {
        return new InquiryRequestCurrency(arg);
    }
    public InquiryRequestCurrencyId Id { get; private set; }
    public InquiryRequestId InquiryRequestId { get; private set; }
    public virtual InquiryRequest InquiryRequest { get; private set; }
    public CurrencyTypeId CurrencyTypeId { get; private set; }
    public virtual CurrencyType CurrencyType { get; private set; }
    public decimal Amount { get; private set; }
    public long ActiveStatusId { get; private set; }
    public long? CreatedBy { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }

    private List<InquiryResponse> _inquiryResponses = new();
    public ICollection<InquiryResponse> InquiryResponses => _inquiryResponses;
}
