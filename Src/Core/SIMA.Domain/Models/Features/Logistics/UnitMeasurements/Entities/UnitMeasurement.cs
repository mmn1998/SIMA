using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;

public class UnitMeasurement : Entity, IAggregateRoot
{
    private UnitMeasurement() { }
    private UnitMeasurement(CreateUnitMeasurementArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async static Task<UnitMeasurement> Create(CreateUnitMeasurementArg arg, IUnitMeasurementDomainService service)
    {
        await CreateGuards(arg, service);
        return new UnitMeasurement(arg);
    }
    public async Task Modify(ModifyUnitMeasurementArg arg, IUnitMeasurementDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateUnitMeasurementArg arg, IUnitMeasurementDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyUnitMeasurementArg arg, IUnitMeasurementDomainService service)
    {

    }
    #endregion
    public UnitMeasurementId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? IsRequireItConfirmation { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Goods> _goods = new();
    public ICollection<Goods> Goods => _goods;
}