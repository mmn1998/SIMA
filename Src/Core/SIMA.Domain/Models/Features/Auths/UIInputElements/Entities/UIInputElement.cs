using SIMA.Domain.Models.Features.Auths.UIInputElements.Args;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Contracts;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;

public class UIInputElement : Entity, IAggregateRoot 
{
    private UIInputElement()
    {
        
    }
    private UIInputElement(CreateUIInputElementArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        IsMultiSelect = arg.IsMultiSelect;
        IsSingleSelect = arg.IsSingleSelect;
        HasInputInEachRecord = arg.HasInputInEachRecord;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async static Task<UIInputElement> Create(CreateUIInputElementArg arg, IUIInputElementDomainService service)
    {
        await CreateGuards(arg, service);
        return new UIInputElement(arg);
    }
    public async Task Modify(ModifyUIInputElementArg arg, IUIInputElementDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        IsMultiSelect = arg.IsMultiSelect;
        IsSingleSelect = arg.IsSingleSelect;
        HasInputInEachRecord = arg.HasInputInEachRecord;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateUIInputElementArg arg, IUIInputElementDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyUIInputElementArg arg, IUIInputElementDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public UIInputElementId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? IsMultiSelect { get; private set; }
    public string? IsSingleSelect { get; private set; }
    public string? HasInputInEachRecord { get; private set; }
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
    private List<ProgressStoreProcedureParam> _progressStoreProcedureParams = new();
    public ICollection<ProgressStoreProcedureParam> ProgressStoreProcedureParams => _progressStoreProcedureParams;
}
