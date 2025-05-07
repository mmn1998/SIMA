using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

public class DataProcedureSupportTeam : Entity
{
    private DataProcedureSupportTeam() { }
    private DataProcedureSupportTeam(CreateDataProcedureSupportTeamArg arg)
    {
        Id = new(arg.Id);
        DataProcedureId = new(arg.DataProcedureId);
        StaffId = new(arg.StaffId);
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        if (arg.BranchId.HasValue) BranchId = new(arg.BranchId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static DataProcedureSupportTeam Create(CreateDataProcedureSupportTeamArg arg)
    {
        return new DataProcedureSupportTeam(arg);
    }
    public DataProcedureSupportTeamId Id { get; private set; }
    public DataProcedureId DataProcedureId { get; private set; }
    public virtual DataProcedure DataProcedure { get; private set; }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}