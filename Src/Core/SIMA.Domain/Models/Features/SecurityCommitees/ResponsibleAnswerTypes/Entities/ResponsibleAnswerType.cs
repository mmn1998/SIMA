using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Entities;

public class ResponsibleAnswerType
{
    private ResponsibleAnswerType()
    {
    }
    private ResponsibleAnswerType(CreateResponsibleAnswerTypeArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ResponsibleAnswerType> Create(CreateResponsibleAnswerTypeArg arg, IResponsibleAnswerTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ResponsibleAnswerType(arg);
    }
    public async Task Modify(ModifyResponsibleAnswerTypeArg arg, IResponsibleAnswerTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards

    private static async Task CreateGuards(CreateResponsibleAnswerTypeArg arg, IResponsibleAnswerTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyResponsibleAnswerTypeArg arg, IResponsibleAnswerTypeDomainService service)
    {

    }
    #endregion
    public ResponsibleAnswerTypeId Id { get; private set; }
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
