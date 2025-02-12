using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Entities;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Args;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.SolutionPriorities.Entities;

public class SolutionPriority : Entity, IAggregateRoot
{
    private SolutionPriority()
    {

    }
    private SolutionPriority(CreateSolutionPriorityArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Priority = arg.Priority;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<SolutionPriority> Create(CreateSolutionPriorityArg arg, ISolutionPriorityDomainService service)
    {
        await CreateGuards(arg, service);
        return new SolutionPriority(arg);
    }
    public async Task Modify(ModifySolutionPriorityArg arg, ISolutionPriorityDomainService service)
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
    private static async Task CreateGuards(CreateSolutionPriorityArg arg, ISolutionPriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifySolutionPriorityArg arg, ISolutionPriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public SolutionPriorityId Id { get; set; }
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
    private List<BusinessContinuityStratgySolution> _businessContinuityStratgySolutions = new();
    public ICollection<BusinessContinuityStratgySolution> BusinessContinuityStratgySolutions => _businessContinuityStratgySolutions;
}