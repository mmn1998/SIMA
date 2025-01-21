using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Contracts;

public interface IWageRateRepository : IRepository<WageRate>
{
    Task<WageRate> GetById(WageRateId id);
}