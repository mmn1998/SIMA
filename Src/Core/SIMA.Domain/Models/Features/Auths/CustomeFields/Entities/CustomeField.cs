using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFields.Args;
using SIMA.Domain.Models.Features.Auths.CustomeFields.Contracts;
using SIMA.Domain.Models.Features.Auths.CustomeFields.ValueObjects;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.CustomeFields.Entities;

public class CustomeField : Entity, IAggregateRoot
{
    private CustomeField()
    {

    }
    private CustomeField(CreateCustomeFieldArg arg)
    {
        Id = new CustomeFieldId(arg.Id);
        Name = arg.Name;
        IsMandatory = arg.IsMandatory;
        EnglishKey = arg.EnglishKey;
        ActiveStatusId = arg.ActiveStatusId;
        MainAggregateId = new(arg.MainAggregateId);
        CustomeFieldTypeId = new(arg.CustomeFieldTypeId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;

    }
    public static async Task<CustomeField> Create(CreateCustomeFieldArg arg, ICustomeFieldDomainService service)
    {
        await CreateGuards(arg, service);
        return new CustomeField(arg);
    }

    public async Task Modify(ModifyCustomeFieldArg arg, ICustomeFieldDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        IsMandatory = arg.IsMandatory;
        EnglishKey = arg.EnglishKey;
        MainAggregateId = new(arg.MainAggregateId);
        CustomeFieldTypeId = new(arg.CustomeFieldTypeId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateCustomeFieldArg arg, ICustomeFieldDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private async Task ModifyGuards(ModifyCustomeFieldArg arg, ICustomeFieldDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public CustomeFieldId Id { get; private set; }
    public MainAggregateId MainAggregateId { get; private set; }
    public virtual MainAggregate MainAggregate { get; private set; }
    public CustomeFieldTypeId CustomeFieldTypeId { get; private set; }
    public virtual CustomeFieldType CustomeFieldType { get; private set; }
    public string? Name { get; private set; }
    public string? EnglishKey { get; private set; }
    public string? IsMandatory { get; private set; }
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
}