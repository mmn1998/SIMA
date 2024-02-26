using SIMA.Auth.Domain.Resources;
using SIMA.Framework.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.AddressTypes
{
    public class AddressTypesException
    {
        public static readonly SimaResultException AddressTypeNameIsNotNullable = new("400", Messages.AddressTypeNameIsNotNullable);
        public static readonly SimaResultException AddressTypeCodeIsNotNullable = new("400", Messages.AddressTypeCodeIsNotNullable);
    }
}
