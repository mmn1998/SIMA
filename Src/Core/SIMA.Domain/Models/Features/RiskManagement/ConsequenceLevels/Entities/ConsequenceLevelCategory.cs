using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Args;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Entities;

public class ConsequenceLevelCategory : Entity
{
    private ConsequenceLevelCategory()
    {

    }
    private ConsequenceLevelCategory(CreateConsequenceLevelCategoryArg arg)
    {
        Id = new(arg.Id);
        ConsequenceLevelId = new(arg.ConsequenceLevelId);
        ConsequenceCategoryId = new(arg.ConsequenceCategoryId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static ConsequenceLevelCategory Create(CreateConsequenceLevelCategoryArg arg)
    {
        return new ConsequenceLevelCategory(arg);
    }
    public ConsequenceLevelCategoryId Id { get; private set; }
    public ConsequenceCategoryId ConsequenceCategoryId { get; private set; }
    public virtual ConsequenceCategory ConsequenceCategory { get; private set; }
    public ConsequenceLevelId ConsequenceLevelId { get; private set; }
    public virtual ConsequenceLevel ConsequenceLevel { get; private set; }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}