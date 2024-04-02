using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerType.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerType.Entities;

internal class SupervisorAnswerType
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
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
