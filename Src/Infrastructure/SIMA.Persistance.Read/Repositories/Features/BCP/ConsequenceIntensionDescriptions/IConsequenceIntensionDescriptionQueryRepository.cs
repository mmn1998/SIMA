using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceIntensionDescriptions;

public interface IConsequenceIntensionDescriptionQueryRepository : IQueryRepository
{
    Task<GetConsequenceIntensionDescriptionQueryResult> GetById(GetConsequenceIntensionDescriptionQuery request);
    Task<Result<IEnumerable<GetConsequenceIntensionDescriptionQueryResult>>> GetAll(GetAllConsequenceIntensionDescriptionsQuery request);
}