using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStategy : Entity, IAggregateRoot
{
    private BusinessContinuityStategy() { }
    private BusinessContinuityStategy(CreateBusinessContinuityStategyArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Title = arg.Title;
        Code = arg.Code;
        Description = arg.Description;
        CordinatorId = arg.CordinatorId;
        IsStableStrategy = arg.IsStableStrategy;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BusinessContinuityStategy> Create(CreateBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessContinuityStategy(arg);
    }
    public async Task Modify(ModifyBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {
        await ModifyGuards(arg, service);
        Title = arg.Title;
        Code = arg.Code;
        Description = arg.Description;
        CordinatorId = arg.CordinatorId;
        IsStableStrategy = arg.IsStableStrategy;
        ActiveStatusId = arg.ActiveStatusId;
        ExpireDate = arg.ExpireDate;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyBusinessContinuityStategyArg arg, IBusinessContinuityStategyDomainService service)
    {

    }
    #endregion
    public BusinessContinuityStategyId Id { get; private set; }
    /// TODO : CordinatorId
    public long CordinatorId { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public string? Code { get; private set; }
    public string? IsStableStrategy { get; private set; }
    public DateTime? ExpireDate { get; private set; }
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
