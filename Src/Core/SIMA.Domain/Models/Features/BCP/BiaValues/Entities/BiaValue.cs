using SIMA.Domain.Models.Features.BCP.BiaValues.Args;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BiaValues.Entities;

public class BiaValue : Entity, IAggregateRoot
{
    private BiaValue()
    {

    }
    private BiaValue(CreateBiaValueArg arg)
    {
        Id = new(arg.Id);
        ConsequenceIntensionId = new(arg.ConsequenceIntensionId);
        ConsequenceId = new(arg.ConsequenceId);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Description = arg.Description;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BiaValue Create(CreateBiaValueArg arg)
    {
        CreateGuards(arg);
        return new BiaValue(arg);
    }
    public void Modify(ModifyBiaValueArg arg)
    {
        ModifyGuards(arg);
        ConsequenceIntensionId = new(arg.ConsequenceIntensionId);
        ConsequenceId = new(arg.ConsequenceId);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Description = arg.Description;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static void CreateGuards(CreateBiaValueArg arg)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Description.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private async Task ModifyGuards(ModifyBiaValueArg arg)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Description.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public BiaValueId Id { get; private set; }
    public string Name { get; private set; }
    public ConsequenceId ConsequenceId { get; private set; }
    public virtual Consequence Consequence { get; private set; }
    public ConsequenceIntensionId ConsequenceIntensionId { get; private set; }
    public virtual ConsequenceIntension ConsequenceIntension { get; private set; }
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