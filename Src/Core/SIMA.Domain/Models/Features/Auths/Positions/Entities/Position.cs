using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.Args;
using SIMA.Domain.Models.Features.Auths.Positions.Interfaces;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Positions.Entities;

public class Position : Entity
{
    private Position() { }
    private Position(CreatePositionArg arg)
    {
        Id = new PositionId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        if (arg.DepartmentId.HasValue) DepartmentId = new DepartmentId(arg.DepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Position> Create(CreatePositionArg arg, IPositionService service)
    {
        await CreateGuards(arg, service);
        return new Position(arg);
    }
    public async void Modify(ModifyPositionArg arg, IPositionService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        if (arg.DepartmentId.HasValue) DepartmentId = new DepartmentId(arg.DepartmentId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    #region Gaurds
    private static async Task CreateGuards(CreatePositionArg arg, IPositionService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyPositionArg arg, IPositionService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
    public PositionId Id { get; private set; }

    public DepartmentId? DepartmentId { get; private set; }

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
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}