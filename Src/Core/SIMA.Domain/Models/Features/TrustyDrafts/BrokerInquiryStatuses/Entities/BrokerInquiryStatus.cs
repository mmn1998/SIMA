using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Contarcts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Entities;

public class BrokerInquiryStatus : Entity, IAggregateRoot
{
    private BrokerInquiryStatus()
    {

    }
    private BrokerInquiryStatus(CreateBrokerInquiryStatusArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BrokerInquiryStatus> Create(CreateBrokerInquiryStatusArg arg, IBrokerInquiryStatusDomainService service)
    {
        await CreateGuards(arg, service);
        return new BrokerInquiryStatus(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateBrokerInquiryStatusArg arg, IBrokerInquiryStatusDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyBrokerInquiryStatusArg arg, IBrokerInquiryStatusDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public async Task Modify(ModifyBrokerInquiryStatusArg arg, IBrokerInquiryStatusDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BrokerInquiryStatusId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<InquiryResponse> _inquiryResponses = new();
    public ICollection<InquiryResponse> InquiryResponses => _inquiryResponses;

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}