using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Args;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Entities;

public class CobitScenarioCategory : Entity, IAggregateRoot
{
    private CobitScenarioCategory()
    {

    }
    private CobitScenarioCategory(CreateCobitScenarioCategoryArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<CobitScenarioCategory> Create(CreateCobitScenarioCategoryArg arg, ICobitScenarioCategoryDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new CobitScenarioCategory(arg);
    }
    public async Task Modify(ModifyCobitScenarioCategoryArg arg, ICobitScenarioCategoryDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public CobitScenarioCategoryId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateCobitScenarioCategoryArg arg, ICobitScenarioCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuard(ModifyCobitScenarioCategoryArg arg, ICobitScenarioCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<CobitScenario> _cobitScenarios = new();
    public ICollection<CobitScenario> CobitScenarios => _cobitScenarios;
}