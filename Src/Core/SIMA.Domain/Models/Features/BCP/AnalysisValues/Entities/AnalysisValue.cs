using SIMA.Domain.Models.Features.BCP.AnalysisValues.Args;
using SIMA.Domain.Models.Features.BCP.AnalysisValues.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.AnalysisValues.Entities;

public class AnalysisValue : Entity, IAggregateRoot
{
    private AnalysisValue()
    {

    }
    private AnalysisValue(CreateAnalysisValueArg arg)
    {
        Id = new(arg.Id);
        ConsequenceIntensionId = new ConsequenceIntensionId(arg.ConsequenceIntensionId);
        ConsequenceId = new ConsequenceId(arg.ConsequenceId);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Description = arg.Description;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AnalysisValue Create(CreateAnalysisValueArg arg)
    {
        CreateGuards(arg);
        return new AnalysisValue(arg);
    }
    public void Modify(ModifyAnalysisValueArg arg)
    {
        ModifyGuards(arg);
        ConsequenceIntensionId = new(arg.ConsequenceIntensionId);
        ConsequenceId = new(arg.ConsequenceId);
        Name = arg.Name;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static void CreateGuards(CreateAnalysisValueArg arg)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private void ModifyGuards(ModifyAnalysisValueArg arg)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public AnalysisValueId Id { get; private set; }
    public string Name { get; private set; }
    public ConsequenceIntensionId ConsequenceIntensionId { get; private set; }
    public virtual ConsequenceIntension ConsequenceIntension { get; private set; }
    public ConsequenceId ConsequenceId { get; private set; }
    public virtual Consequence Consequence { get; private set; }
    public string Description { get; private set; }
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