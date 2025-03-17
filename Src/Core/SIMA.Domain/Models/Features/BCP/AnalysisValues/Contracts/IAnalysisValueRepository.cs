using SIMA.Domain.Models.Features.BCP.AnalysisValues.Entities;
using SIMA.Domain.Models.Features.BCP.AnalysisValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceValues.Contracts;

public interface IAnalysisValueRepository : IRepository<AnalysisValue>
{
    Task<AnalysisValue> GetById(AnalysisValueId id);
}