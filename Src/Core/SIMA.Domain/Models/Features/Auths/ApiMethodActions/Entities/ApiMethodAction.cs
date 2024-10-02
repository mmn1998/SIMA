using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Args;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Contracts;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;

public class ApiMethodAction : Entity, IAggregateRoot
{
    private ApiMethodAction() { }
    private ApiMethodAction(CreateApiMethodActionArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApiMethodAction> Create(CreateApiMethodActionArg arg, IApiMethodActionDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApiMethodAction(arg);
    }
    public async Task Modify(ModifyApiMethodActionArg arg, IApiMethodActionDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApiMethodActionArg arg, IApiMethodActionDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyApiMethodActionArg arg, IApiMethodActionDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ApiMethodActionId Id { get; private set; } = new(IdHelper.GenerateUniqueId());
    public string? Name { get; private set; } = string.Empty;
    public string? Code { get; private set; } = string.Empty;
    public long? ActiveStatusId { get; set; }
    public long? CreatedBy { get; set; }
    public long? ModifiedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public ICollection<ProgressStoreProcedureParam>? ProgressStoreProcedureParams { get; set; }
    public ICollection<Api>? Apis { get; set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}