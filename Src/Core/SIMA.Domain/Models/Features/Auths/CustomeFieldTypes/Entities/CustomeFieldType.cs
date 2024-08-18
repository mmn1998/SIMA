using SIMA.Domain.Models.Features.Auths.CustomeFields.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Args;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Entities;

public class CustomeFieldType : Entity, IAggregateRoot
{
    private CustomeFieldType()
    {

    }
    private CustomeFieldType(CreateCustomeFieldTypeArg arg)
    {
        Id = new CustomeFieldTypeId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        IsList = arg.IsList;
        IsMultiSelect = arg.IsMultiSelect;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;

    }
    public static async Task<CustomeFieldType> Create(CreateCustomeFieldTypeArg arg, ICustomeFieldTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new CustomeFieldType(arg);
    }

    public async Task Modify(ModifyCustomeFieldTypeArg arg, ICustomeFieldTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        IsList = arg.IsList;
        IsMultiSelect = arg.IsMultiSelect;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateCustomeFieldTypeArg arg, ICustomeFieldTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyCustomeFieldTypeArg arg, ICustomeFieldTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public CustomeFieldTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? IsList { get; private set; }
    public string? IsMultiSelect { get; private set; }
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
    private List<CustomeField> _customeFields = new();
    public ICollection<CustomeField> CustomeFields => _customeFields;
}
