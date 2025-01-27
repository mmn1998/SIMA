using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SIMA.Application.Contract.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
public sealed class CustomePhoneNumberAttribute : DataTypeAttribute
{
    private readonly PhoneTypeEnum _phoneType;

    public CustomePhoneNumberAttribute(PhoneTypeEnum phoneType) : base(DataType.PhoneNumber)
    {
        _phoneType = phoneType;
    }
    public override bool IsValid(object? value)
    {
        bool result = false;
        if (value is not null)
        {
            var stringValue = value.ToString() ?? string.Empty;

            switch (_phoneType)
            {
                case PhoneTypeEnum.Phone:
                    {
                        var phoneRegexPattern = new Regex(CodeMessges.PhoneRegex);
                        if (!phoneRegexPattern.IsMatch(stringValue))
                        {
                            throw new SimaResultException(CodeMessges._400Code, Messages.PhoneNumberRegexError);
                        }
                        else result = true;
                    }
                    break;
                case PhoneTypeEnum.Work:
                    {
                        var phoneRegexPattern = new Regex(CodeMessges.PhoneRegex);
                        if (!phoneRegexPattern.IsMatch(stringValue))
                        {
                            throw new SimaResultException(CodeMessges._400Code, Messages.PhoneNumberRegexError);
                        }
                        else result = true;
                    }
                    break;
                case PhoneTypeEnum.Mobile:
                    {
                        var mobileRegexPattern = new Regex(CodeMessges.MobileRegex);
                        if (!mobileRegexPattern.IsMatch(stringValue))
                        {
                            throw new SimaResultException(CodeMessges._400Code, Messages.MobileNumberRegexError);
                        }
                        else result = true;
                    }
                    break;
                default:
                    result = false;
                    break;
            }

        }
        return result;
    }
}