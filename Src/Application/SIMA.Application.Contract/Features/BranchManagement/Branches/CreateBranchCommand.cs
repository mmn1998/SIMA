using Sima.Framework.Core.Mediator;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.Branches;

public class CreateBranchCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? BranchTypeId { get; set; }

    public long? BranchChiefOfficerId { get; set; }
    public long DepartmentId { get; set; }

    public long? BranchDeputyId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int? LocationId { get; set; }
    [CustomePhoneNumber(PhoneTypeEnum.Phone)]
    public string? PhoneNumber { get; set; }
    [CustomePostalCode]

    public string? PostalCode { get; set; }

    public string? Address { get; set; }

    public string? IsMultiCurrencyBranch { get; set; }
}
