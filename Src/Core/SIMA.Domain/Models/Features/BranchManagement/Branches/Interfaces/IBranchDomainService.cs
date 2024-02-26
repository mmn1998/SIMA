using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;

public interface IBranchDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long Id);
    Task<bool> IsNearExistingLocations(double newLatitude, double newLongitude);
}
