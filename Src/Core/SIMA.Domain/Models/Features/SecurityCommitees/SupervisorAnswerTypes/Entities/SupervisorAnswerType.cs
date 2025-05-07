using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.Entities;

public class SupervisorAnswerType : Entity
{
    private SupervisorAnswerType()
    {
    }
    private SupervisorAnswerType(CreateSupervisorAnswerTypeArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<SupervisorAnswerType> Create(CreateSupervisorAnswerTypeArg arg, ISupervisorAnswerTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new SupervisorAnswerType(arg);
    }
    public async Task Modify(ModifySupervisorAnswerTypeArg arg, ISupervisorAnswerTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards

    private static async Task CreateGuards(CreateSupervisorAnswerTypeArg arg, ISupervisorAnswerTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifySupervisorAnswerTypeArg arg, ISupervisorAnswerTypeDomainService service)
    {

    }
    #endregion
    public SupervisorAnswerTypeId Id { get; private set; }
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<ApprovalSupervisorAnswer> _approvalSupervisorAnswers => new();
    public ICollection<ApprovalSupervisorAnswer> ApprovalSupervisorAnswers => _approvalSupervisorAnswers;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
