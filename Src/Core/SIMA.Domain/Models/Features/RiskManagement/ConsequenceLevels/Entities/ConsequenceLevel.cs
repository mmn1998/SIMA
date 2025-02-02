using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Args;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Entities;

public class ConsequenceLevel : Entity, IAggregateRoot
{
    private ConsequenceLevel()
    {

    }
    private ConsequenceLevel(CreateConsequenceLevelArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ValueTitle = arg.ValueTitle;
        NumericValue = arg.NumericValue;
        Description = arg.Description;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<ConsequenceLevel> Create(CreateConsequenceLevelArg arg, IConsequenceLevelDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new ConsequenceLevel(arg);
    }
    public async Task Modify(ModifyConsequenceLevelArg arg, IConsequenceLevelDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ValueTitle = arg.ValueTitle;
        Description = arg.Description;
        NumericValue = arg.NumericValue;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public ConsequenceLevelId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public float NumericValue { get; private set; }
    public string? ValueTitle { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateConsequenceLevelArg arg, IConsequenceLevelDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ValueTitle.NullCheck();
        //arg.Description.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    private async Task ModifyGuard(ModifyConsequenceLevelArg arg, IConsequenceLevelDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ValueTitle.NullCheck();
        //arg.Description.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        DeleteCategories(userId);
    }
    public void ModifyCategories(List<CreateConsequenceLevelCategoryArg> args)
    {
        var activeEntities = _riskConsequences.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ConsequenceCategoryId == x.ConsequenceCategoryId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ConsequenceCategoryId.Value == x.ConsequenceCategoryId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _riskConsequences.FirstOrDefault(x => x.ConsequenceCategoryId.Value == arg.ConsequenceCategoryId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConsequenceLevelCategory.Create(arg);
                _riskConsequences.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void AddCategories(List<CreateConsequenceLevelCategoryArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConsequenceLevelCategory.Create(arg);
            _riskConsequences.Add(entity);
        }
    }
    public void DeleteCategories(long userId)
    {
        foreach (var entity in _riskConsequences)
        {
            entity.Delete(userId);
        }
    }
    private List<Severity> _severities = new();
    public ICollection<Severity> Severities => _severities;
    private List<ConsequenceLevelCategory> _riskConsequences = new();
    public ICollection<ConsequenceLevelCategory> RiskConsequences => _riskConsequences;
}
