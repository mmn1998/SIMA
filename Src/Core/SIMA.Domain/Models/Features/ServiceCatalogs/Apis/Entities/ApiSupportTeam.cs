using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiSupportTeam : Entity
{
    private ApiSupportTeam() { }
    private ApiSupportTeam(CreateApiSupportTeamArg arg)
    {
        Id = new(arg.Id);
        ApiId = new(arg.ApiId);
        StaffId = new(arg.StaffId);
        if (arg.BranchId.HasValue) BranchId = new(arg.BranchId.Value);
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ApiSupportTeam Create(CreateApiSupportTeamArg arg)
    {
        return new ApiSupportTeam(arg);
    }
    public ApiSupportTeamId Id { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api { get; private set; }
    public StaffId StaffId { get; private set; }
    public virtual Staff Staff { get; private set; }
    public DepartmentId? DepartmentId { get; private set; }
    public virtual Department? Department { get; private set; }
    public BranchId? BranchId { get; private set; }
    public virtual Branch? Branch { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
