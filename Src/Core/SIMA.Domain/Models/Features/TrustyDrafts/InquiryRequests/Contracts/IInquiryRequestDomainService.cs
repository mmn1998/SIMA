using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;

public interface IInquiryRequestDomainService : IDomainService
{
    //Task<bool> IsCodeUnique(string code, InquiryRequestId? id = null);
    Task<long> GetBranchIdByUserId(long userId);
    Task<string> GetBranchCodeByUserId(long userId);
    Task<string?> GetLastRefrenceNumber();
    Task<string?> GetCustomerNumber(long customerId);
    Task<string?> GetCurrencySymbol(long currencyTypeId);
    void CheckBeneficiaryName(string beneficiaryName);
}