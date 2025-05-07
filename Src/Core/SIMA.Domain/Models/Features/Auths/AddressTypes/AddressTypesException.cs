using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.Auths.AddressTypes
{
    public class AddressTypesException
    {
        public static readonly SimaResultException AddressTypeNameIsNotNullable = new("400", Messages.AddressTypeNameIsNotNullable);
        public static readonly SimaResultException AddressTypeCodeIsNotNullable = new("400", Messages.AddressTypeCodeIsNotNullable);
    }
}
