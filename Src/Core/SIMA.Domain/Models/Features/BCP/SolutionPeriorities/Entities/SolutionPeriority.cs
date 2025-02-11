using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Args;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Entities;

public class SolutionPeriority : Entity, IAggregateRoot
{
    private SolutionPeriority()
    {

    }
    private SolutionPeriority(CreateSolutionPeriorityArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Priority = arg.Priority;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<SolutionPeriority> Create(CreateSolutionPeriorityArg arg, ISolutionPeriorityDomainService service)
    {
        await CreateGuards(arg, service);
        return new SolutionPeriority(arg);
    }
    public async Task Modify(ModifySolutionPeriorityArg arg, ISolutionPeriorityDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Priority = arg.Priority;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateSolutionPeriorityArg arg, ISolutionPeriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifySolutionPeriorityArg arg, ISolutionPeriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public SolutionPeriorityId Id { get; set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public float Priority { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}