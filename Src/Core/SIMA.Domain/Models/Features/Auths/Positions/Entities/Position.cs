using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Entities;
using SIMA.Domain.Models.Features.Auths.PositionLevels.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.Args;
using SIMA.Domain.Models.Features.Auths.Positions.Interfaces;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Positions.Entities;

public class Position : Entity
{
    private Position() { }
    private Position(CreatePositionArg arg)
    {
        Id = new PositionId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        if (arg.BranchId.HasValue) BranchId = new(arg.BranchId.Value);
        PersonLimitation = arg.PersonLimitation;
        PositionLevelId = new(arg.PositionLevelId);
        PositionTypeId = new(arg.PositionTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Position> Create(CreatePositionArg arg, IPositionService service)
    {
        await CreateGuards(arg, service);
        return new Position(arg);
    }
    public async Task Modify(ModifyPositionArg arg, IPositionService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
        if (arg.BranchId.HasValue) BranchId = new(arg.BranchId.Value);
        PositionLevelId = new(arg.PositionLevelId);
        PositionTypeId = new(arg.PositionTypeId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
        PersonLimitation = arg.PersonLimitation;

    }

    #region Gaurds
    private static async Task CreateGuards(CreatePositionArg arg, IPositionService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyPositionArg arg, IPositionService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public PositionId Id { get; private set; }

    public DepartmentId? DepartmentId { get; private set; }
    public BranchId? BranchId { get; private set; }
    public virtual Branch? Branch { get; private set; }
    public PositionTypeId? PositionTypeId { get; private set; }
    public virtual PositionType? PositionType { get; private set; }
    public PositionLevelId? PositionLevelId { get; private set; }
    public virtual PositionLevel? PositionLevel { get; private set; }
    public int PersonLimitation { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Department? Department { get; private set; }

    private List<Staff> _staff = new();
    public ICollection<Staff> Staff => _staff;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}