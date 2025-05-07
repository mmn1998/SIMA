using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Args;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Origins.Entities;
using SIMA.Domain.Models.Features.BCP.Origins.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;

public class ConsequenceValue : Entity, IAggregateRoot
{
    private ConsequenceValue()
    {

    }
    private ConsequenceValue(CreateConsequenceValueArg arg)
    {
        Id = new(arg.Id);
        OriginId = new(arg.OriginId);
        ConsequenceId = new(arg.ConsequenceId);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        ValueNumber = arg.ValueNumber;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConsequenceValue Create(CreateConsequenceValueArg arg)
    {
        CreateGuards(arg);
        return new ConsequenceValue(arg);
    }
    public void Modify(ModifyConsequenceValueArg arg)
    {
        ModifyGuards(arg);
        OriginId = new(arg.OriginId);
        ConsequenceId = new(arg.ConsequenceId);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        ValueNumber = arg.ValueNumber;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static void CreateGuards(CreateConsequenceValueArg arg)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private async Task ModifyGuards(ModifyConsequenceValueArg arg)
    {
        arg.NullCheck();
        arg.Name.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public ConsequenceValueId Id { get; private set; }
    public string Name { get; private set; }
    public ConsequenceId ConsequenceId { get; private set; }
    public virtual Consequence Consequence { get; private set; }
    public OriginId OriginId { get; private set; }
    public virtual Origin Origin { get; private set; }
    public float ValueNumber { get; private set; }
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