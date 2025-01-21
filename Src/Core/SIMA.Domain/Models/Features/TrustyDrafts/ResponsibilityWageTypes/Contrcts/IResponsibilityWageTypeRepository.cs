using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Contrcts;

public interface IResponsibilityWageTypeRepository : IRepository<ResponsibilityWageType>
{
    Task<ResponsibilityWageType> GetById(ResponsibilityWageTypeId id);

    Task<ResponsibilityWageType> GetByCode(string code);
}