using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;

public interface IBranchDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long Id);
    Task<bool> IsNearExistingLocations(double newLatitude, double newLongitude);
    Task<bool> IsStaffHasAnyRoleInOtherBrfanches(StaffId staffId, BranchId? branchId = null);
    Task<bool> IsStaffFromSelectedLocation(StaffId staffId, LocationId locationId);
}
