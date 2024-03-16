using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;

public class IssueType : Entity
{
    private IssueType()
    {
    }

    public IssueType(CreateIssueTypeArg arg)
    {
        Id = new IssueTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        IconPath = arg.IconPath;
        ColorHex = arg.ColorHex;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssueType> Create(CreateIssueTypeArg arg, IIssueTypeDomainService service)
    {
        await CreateGuard(arg, service);
        return new IssueType(arg);
    }
    public async Task Modify(ModifyIssueTypeArg arg, IIssueTypeDomainService service)
    {
        await ModifyGuard(arg, service);

        Name = arg.Name;
        Code = arg.Code;
        IconPath = arg.IconPath;
        ColorHex = arg.ColorHex;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string IconPath { get; private set; }
    public string ColorHex { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public ICollection<Issue> Issues { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }


    #region Guards
    private static async Task CreateGuard(CreateIssueTypeArg arg, IIssueTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!string.IsNullOrEmpty(arg.ColorHex))
            if (!service.IsHexCodeValid(arg.ColorHex)) throw SimaResultException.ColorHexCodeIsInorrectError;

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }

    private async Task ModifyGuard(ModifyIssueTypeArg arg, IIssueTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!string.IsNullOrEmpty(arg.ColorHex))
            if (!service.IsHexCodeValid(arg.ColorHex)) throw SimaResultException.ColorHexCodeIsInorrectError;

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion

}
