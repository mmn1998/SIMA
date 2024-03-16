using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.Branches;

public class BranchDomainService : IBranchDomainService
{
    private readonly SIMADBContext _context;
    private readonly double _bufferDistanceInMeters;

    public BranchDomainService(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _bufferDistanceInMeters = configuration.GetValue<double>("GISSettings:BufferDistanceInMeters");
    }
    public async Task<bool> IsCodeUnique(string code, long Id)
    {
        if (Id > 0)
            return await _context.Branches.AnyAsync(b => b.Code == code && b.Id != new BranchId(Id));
        else
            return await _context.Branches.AnyAsync(b => b.Code == code);
    }
    public async Task<bool> IsNearExistingLocations(double newLatitude, double newLongitude)
    {
        bool result = false;
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 3857); // For OpenStreet and Google Maps
        var newPoint = geometryFactory.CreatePoint(new Coordinate(newLongitude, newLatitude));
        double bufferDistanceInDegrees = _bufferDistanceInMeters / (6371000 * Math.PI / 180);

        var dbResults = await _context.Branches.ToListAsync();
        foreach (var item in dbResults)
        {
            if (!result) break;
            result = newPoint.Distance(geometryFactory.CreatePoint(new Coordinate(item.Longitude ?? 0, item.Latitude ?? 0))) <= bufferDistanceInDegrees;
        }
        return result;
    }

    public async Task<bool> IsStaffFromSelectedLocation(StaffId staffId, LocationId locationId)
    {
        bool result = false;
        var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Id == staffId);
        if (staff is not null)
        {
            if (staff.ProfileId is not null)
            {
                var location = await _context.Locations
                    .Include(x => x.AddressBooks)
                    .FirstOrDefaultAsync(l => l.Id == locationId);
                if (location is not null && location.AddressBooks is not null)
                {
                    foreach (var item in location.AddressBooks)
                    {
                        if (item.ProfileId == staff.ProfileId)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
        }
        return result;
    }

    public async Task<bool> IsStaffHasAnyRoleInOtherBrfanches(StaffId staffId, BranchId? branchId = null)
    {
        bool result = true;
        if (branchId != null) result = await _context.Branches.AnyAsync(b => b.BranchChiefOfficerId == staffId || b.BranchDeputyId == staffId);
        else result = await _context.Branches.AnyAsync(b => b.Id == branchId && (b.BranchChiefOfficerId == staffId || b.BranchDeputyId == staffId));
        return result;
    }
}
