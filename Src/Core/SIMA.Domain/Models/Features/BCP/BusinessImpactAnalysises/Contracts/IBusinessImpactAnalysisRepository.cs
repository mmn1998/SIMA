using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;

public interface IBusinessImpactAnalysisRepository : IRepository<BusinessImpactAnalysis>
{
    Task<BusinessImpactAnalysis> GetById(BusinessImpactAnalysisId id);
    Task<BusinessImpactAnalysis?> GetLast();
}
