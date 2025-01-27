using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SIMA.Application.Contract.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
public sealed class CustomePostalCodeAttribute : DataTypeAttribute
{
    public CustomePostalCodeAttribute() : base(DataType.PostalCode)
    {

    }
    public override bool IsValid(object? value)
    {
        bool result = false;
        if (value is not null)
        {
            var stringValue = value.ToString() ?? string.Empty;
            var regexPattern = new Regex(CodeMessges.PostalCodeRegex);
            if (!regexPattern.IsMatch(stringValue))
            {
                throw new SimaResultException(CodeMessges._400Code, Messages.PostalCodeRegexError);
            }
            else result = true;
        }
        return result;
    }
}