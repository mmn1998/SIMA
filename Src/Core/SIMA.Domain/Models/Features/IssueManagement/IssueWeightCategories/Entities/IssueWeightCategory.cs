using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;

public class IssueWeightCategory : Entity
{
    private IssueWeightCategory()
    {
    }

    public IssueWeightCategory(CreateIssueWeightCategoryArg arg)
    {
        Id = new IssueWeightCategoryId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        MinRange = arg.MinRange;
        MaxRange = arg.MaxRange;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssueWeightCategory> Create(CreateIssueWeightCategoryArg arg, IIssueWeightCategoryDomainService service)
    {
        await CreateGuards(arg, service);
        return new IssueWeightCategory(arg);
    }
    public async void Modify(ModifyIssueWeightCategoryArg arg, IIssueWeightCategoryDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        MinRange = arg.MinRange;
        MaxRange = arg.MaxRange;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateIssueWeightCategoryArg arg, IIssueWeightCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        arg.MinRange.NullCheck();
        arg.MaxRange.NullCheck();
        if (!service.IsRangeVilid(arg.MinRange, arg.MaxRange)) throw SimaResultException.RangeNotValidError;
        if (await service.IsRangeExist(arg.MinRange, arg.MaxRange, 0)) throw SimaResultException.RangeAlreadyAllocatedError;

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyIssueWeightCategoryArg arg, IIssueWeightCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        arg.MinRange.NullCheck();
        arg.MaxRange.NullCheck();
        if (!service.IsRangeVilid(arg.MinRange, arg.MaxRange)) throw SimaResultException.RangeNotValidError;
        if (await service.IsRangeExist(arg.MinRange, arg.MaxRange, arg.Id)) throw SimaResultException.RangeAlreadyAllocatedError;

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
    public IssueWeightCategoryId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public int MinRange { get; private set; }
    public int MaxRange { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public ICollection<Issue> Issues { get; private set; }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
