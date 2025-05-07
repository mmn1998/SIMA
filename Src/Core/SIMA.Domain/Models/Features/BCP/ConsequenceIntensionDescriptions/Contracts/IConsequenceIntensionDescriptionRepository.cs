using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Contracts;

public interface IConsequenceIntensionDescriptionRepository : IRepository<ConsequenceIntensionDescription>
{
    Task<ConsequenceIntensionDescription> GetById(ConsequenceIntensionDescriptionId id);
}