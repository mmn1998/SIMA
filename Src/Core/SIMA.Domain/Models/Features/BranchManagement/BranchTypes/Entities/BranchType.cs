using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;

public class BranchType : Entity
{
    private BranchType()
    {

    }
    private BranchType(CreateBranchTypeArg arg)
    {
        Id = new BranchTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<BranchType> Create(CreateBranchTypeArg arg, IBranchTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new BranchType(arg);
    }
    public async Task Modify(ModifyBranchTypeArg arg, IBranchTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifyAt = arg.ModifyAt;
        ModifyBy = arg.ModifyBy;
    }
    public async Task Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifyAt { get; private set; }
    public long? ModifyBy { get; private set; }
    private List<Branch> _branches = new();
    public virtual ICollection<Branch> Branches => _branches.AsReadOnly();
    public BranchTypeId Id { get; private set; }

    #region Guards
    private static async Task CreateGuards(CreateBranchTypeArg arg, IBranchTypeDomainService branchTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await branchTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyBranchTypeArg arg, IBranchTypeDomainService branchTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await branchTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }

    #endregion
}
