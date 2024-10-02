using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace SIMA.Domain.Models.Features.Auths.Profiles.Entities;

public class PhoneBook
{
    private PhoneBook()
    {

    }
    private PhoneBook(CreatePhoneBookArg arg)
    {
        Id = new PhoneBookId(IdHelper.GenerateUniqueId());
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PhoneTypeId.HasValue) PhoneTypeId = new PhoneTypeId(arg.PhoneTypeId.Value);
        PhoneNumber = arg.PhoneNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<PhoneBook> Create(CreatePhoneBookArg arg)
    {
        CreateGuards(arg);
        return new PhoneBook(arg);
    }
    public void Modify(ModifyPhoneBookArg arg)
    {
        ModifyGuards(arg);
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PhoneTypeId.HasValue) PhoneTypeId = new PhoneTypeId(arg.PhoneTypeId.Value);
        PhoneNumber = arg.PhoneNumber;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreatePhoneBookArg arg)
    {
        var stringValue = arg.PhoneNumber?.ToString() ?? string.Empty;
        switch (arg.PhoneTypeId)
        {
            case (long)PhoneTypeEnum.Mobile:
                {
                    var mobileRegexPattern = new Regex(CodeMessges.MobileRegex);
                    if (!mobileRegexPattern.IsMatch(stringValue))
                    {
                        throw new SimaResultException(CodeMessges._400Code, Messages.MobileNumberRegexError);
                    }
                }
                break;
            case (long)PhoneTypeEnum.Work:
                {
                    var phoneRegexPattern = new Regex(CodeMessges.PhoneRegex);
                    if (!phoneRegexPattern.IsMatch(stringValue))
                    {
                        throw new SimaResultException(CodeMessges._400Code, Messages.PhoneNumberRegexError);
                    }
                }
                break;
            case (long)PhoneTypeEnum.Phone:
                {
                    var phoneRegexPattern = new Regex(CodeMessges.PhoneRegex);
                    if (!phoneRegexPattern.IsMatch(stringValue))
                    {
                        throw new SimaResultException(CodeMessges._400Code, Messages.PhoneNumberRegexError);
                    }
                }
                break;
            default:
                break;
        } 
    }
    private void ModifyGuards(ModifyPhoneBookArg arg)
    {
        var stringValue = arg.PhoneNumber?.ToString() ?? string.Empty;
        switch (arg.PhoneTypeId)
        {
            case (long)PhoneTypeEnum.Mobile:
                {
                    var mobileRegexPattern = new Regex(@"(0|\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}");
                    if (!mobileRegexPattern.IsMatch(stringValue))
                    {
                        throw new SimaResultException(CodeMessges._400Code, Messages.MobileNumberRegexError);
                    }
                }
                break;
            case (long)PhoneTypeEnum.Work:
                {
                    var phoneRegexPattern = new Regex(@"^(0|\+98)[1-9]{2}[0-9]{8}$");
                    if (!phoneRegexPattern.IsMatch(stringValue))
                    {
                        throw new SimaResultException(CodeMessges._400Code, Messages.PhoneNumberRegexError);
                    }
                }
                break;
            case (long)PhoneTypeEnum.Phone:
                {
                    var phoneRegexPattern = new Regex(@"^(0|\+98)[1-9]{2}[0-9]{8}$");
                    if (!phoneRegexPattern.IsMatch(stringValue))
                    {
                        throw new SimaResultException(CodeMessges._400Code, Messages.PhoneNumberRegexError);
                    }
                }
                break;
            default:
                break;
        } 
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public PhoneBookId Id { get; private set; }

    public ProfileId? ProfileId { get; private set; }

    public PhoneTypeId? PhoneTypeId { get; private set; }

    public string? PhoneNumber { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual PhoneType? PhoneType { get; private set; }

    public virtual Profile? Profile { get; private set; }
}
