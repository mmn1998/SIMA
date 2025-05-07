using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

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
    public async Task Modify(ModifyIssueWeightCategoryArg arg, IIssueWeightCategoryDomainService service)
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
        if (!service.IsRangeVilid(arg.MinRange, arg.MaxRange)) throw new SimaResultException(CodeMessges._100047Code, Messages.RangeNotValidError);
        if (await service.IsRangeExist(arg.MinRange, arg.MaxRange, 0)) throw new SimaResultException(CodeMessges._100048Code, Messages.RangeAlreadyAllocatedError);

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyIssueWeightCategoryArg arg, IIssueWeightCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        arg.MinRange.NullCheck();
        arg.MaxRange.NullCheck();
        if (!service.IsRangeVilid(arg.MinRange, arg.MaxRange)) throw new SimaResultException(CodeMessges._100047Code, Messages.RangeNotValidError);
        if (await service.IsRangeExist(arg.MinRange, arg.MaxRange, arg.Id)) throw new SimaResultException(CodeMessges._100048Code
            , Messages.RangeAlreadyAllocatedError);

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
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
    private List<IssueChangeHistory> _issueChangeHistories = new();
    public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;
    private List<Issue> _issues = new();
    public ICollection<Issue> Issues => _issues;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Deactive(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
}
