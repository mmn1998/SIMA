using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Arg;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Contracts;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;

public class TimeMeasurement : Entity
{
    private TimeMeasurement()
    {

    }
    private TimeMeasurement(CreateTimeMeasurementArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        UnitBasement = arg.UnitBasement;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<TimeMeasurement> Create(CreateTimeMeasurementArg arg, ITimeMeasurementDomainService service)
    {
        await CreateGuards(arg, service);
        return new TimeMeasurement(arg);
    }
    public async Task Modify(ModifyTimeMeasurementArg arg, ITimeMeasurementDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        UnitBasement = arg.UnitBasement;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateTimeMeasurementArg arg, ITimeMeasurementDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyTimeMeasurementArg arg, ITimeMeasurementDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public TimeMeasurementId Id { get; private set; }
    public long UnitBasement { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
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
    private List<RecoveryPointObjective> _recoveryPointObjectives = new();
    public ICollection<RecoveryPointObjective> RecoveryPointObjectives => _recoveryPointObjectives;
    private List<ConfigurationItemBackupSchedule> _configurationItemBackupSchedules = new();
    public ICollection<ConfigurationItemBackupSchedule> ConfigurationItemBackupSchedules => _configurationItemBackupSchedules;
}
