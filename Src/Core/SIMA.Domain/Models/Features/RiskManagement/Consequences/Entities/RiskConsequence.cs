using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Consequences.Args;
using SIMA.Domain.Models.Features.RiskManagement.Consequences.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;

public class RiskConsequence : Entity, IAggregateRoot
{
    private RiskConsequence()
    {

    }
    private RiskConsequence(CreateRiskConsequenceArg arg)
    {
        Id = new(arg.Id);
        ConsequenceLevelId = new(arg.ConsequenceLevelId);
        ConsequenceCategoryId = new(arg.ConsequenceCategoryId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static RiskConsequence Create(CreateRiskConsequenceArg arg)
    {
        CreateGuadrs(arg);
        return new RiskConsequence(arg);
    }
    public void Modify(ModifyRiskConsequenceArg arg)
    {
        ModifyGuards(arg);
        ConsequenceLevelId = new(arg.ConsequenceLevelId);
        ConsequenceCategoryId = new(arg.ConsequenceCategoryId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public RiskConsequenceId Id { get; private set; }
    public ConsequenceCategoryId ConsequenceCategoryId { get; private set; }
    public virtual ConsequenceCategory ConsequenceCategory { get; private set; }
    public ConsequenceLevelId ConsequenceLevelId { get; private set; }
    public virtual ConsequenceLevel ConsequenceLevel { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static void CreateGuadrs(CreateRiskConsequenceArg arg)
    {
        arg.NullCheck();
    }
    private void ModifyGuards(ModifyRiskConsequenceArg arg)
    {
        arg.NullCheck();
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}