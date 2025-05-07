using SIMA.Framework.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.ConfigurationAttributes
{
    public interface IConfigurationAttributeService : IDomainService
    {
        Task<bool> CheckEnglishKey(string key);
    }
}
