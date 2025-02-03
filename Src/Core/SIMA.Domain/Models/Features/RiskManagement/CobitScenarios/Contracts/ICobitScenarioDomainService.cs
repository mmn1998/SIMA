using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;

public interface ICobitScenarioDomainService : IDomainService
{
    void ValidateAscii(string input);
}
