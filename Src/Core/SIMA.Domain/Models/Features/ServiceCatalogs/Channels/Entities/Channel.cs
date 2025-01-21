using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.Auths.UserTypes.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

public class Channel : Entity, IAggregateRoot
{
    private Channel()
    {
    }

    private Channel(CreateChannelArg arg)
    {
        Id = new ChannelId(arg.Id);
        ServiceStatusId = arg.ServiceStatusId.HasValue ? new ServiceStatusId(arg.ServiceStatusId.Value) : null;
        Name = arg.Name;
        Code = arg.Code;
        Scope = arg.Scope;
        Description = arg.Description;
        InServiceDate = arg.InServiceDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<Channel> Create(CreateChannelArg arg, IChannelDomainService service)
    {
        await CreateGuards(arg, service);
        return new Channel(arg);
    }
    public async Task Modify(ModifyChannelArg arg, IChannelDomainService service)
    {
        await ModifyGuards(arg, service);
        ServiceStatusId = arg.ServiceStatusId.HasValue ? new ServiceStatusId(arg.ServiceStatusId.Value) : null;
        Name = arg.Name;
        Code = arg.Code;
        Scope = arg.Scope;
        Description = arg.Description;
        InServiceDate = arg.InServiceDate;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateChannelArg arg, IChannelDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyChannelArg arg, IChannelDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRelaatedEntities
        DeleteChannelAccessPoints(userId);
        DeleteChannelResponsibles(userId);
        DeleteChannelUserTypes(userId);
        DeleteProductChannels(userId);
        DeleteServiceChannels(userId);
        #endregion
    }

    #region DeleteMethods
    public void DeleteProductChannels(long userId)
    {
        foreach (var item in _productChannels)
        {
            item.Delete(userId);
        }
    }
    public void DeleteChannelResponsibles(long userId)
    {
        foreach (var item in _channelResponsibles)
        {
            item.Delete(userId);
        }
    }
    public void DeleteChannelUserTypes(long userId)
    {
        foreach (var item in _channelUserTypes)
        {
            item.Delete(userId);
        }
    }
    public void DeleteChannelAccessPoints(long userId)
    {
        foreach (var item in _channelAccessPoints)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceChannels(long userId)
    {
        foreach (var item in _serviceChannels)
        {
            item.Delete(userId);
        }
    }
    #endregion

    #region AddMethods
    public void AddProductChannels(List<CreateProductChannelArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ProductChannel.Create(arg);
            _productChannels.Add(entity);
        }
    }
    public void AddServiceChannels(List<CreateServiceChannelArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceChannel.Create(arg);
            _serviceChannels.Add(entity);
        }
    }
    public void AddChannelResponsibles(List<CreateChannelResponsibleArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ChannelResponsible.Create(arg);
            _channelResponsibles.Add(entity);
        }
    }
    public void AddChannelUserTypes(List<CreateChannelUserTypeArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ChannelUserType.Create(arg);
            _channelUserTypes.Add(entity);
        }
    }
    public void AddChannelAccessPoints(List<CreateChannelAccessPointArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ChannelAccessPoint.Create(arg);
            _channelAccessPoints.Add(entity);
        }
    }
    #endregion

    #region ModifyMethods

    public void ModifyProductChannels(List<CreateProductChannelArg> args)
    {
        var activeEntities = _productChannels.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ProductId == x.ProductId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ProductId.Value == x.ProductId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _productChannels.FirstOrDefault(x => x.ProductId.Value == arg.ProductId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ProductChannel.Create(arg);
                _productChannels.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyChannelResponsibles(List<CreateChannelResponsibleArg> args)
    {
        var activeEntities = _channelResponsibles.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ResponsibleId == x.ResponsibleId.Value && c.ResponsibleTypeId == x.ResponsibleTypeId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ResponsibleId.Value == x.ResponsibleId && c.ResponsibleTypeId.Value == x.ResponsibleTypeId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _channelResponsibles.FirstOrDefault(x => x.ResponsibleId.Value == arg.ResponsibleId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ChannelResponsible.Create(arg);
                _channelResponsibles.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyChannelUserTypes(List<CreateChannelUserTypeArg> args)
    {
        var activeEntities = _channelUserTypes.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.UserTypeId == x.UserTypeId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.UserTypeId.Value == x.UserTypeId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _channelUserTypes.FirstOrDefault(x => x.UserTypeId.Value == arg.UserTypeId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ChannelUserType.Create(arg);
                _channelUserTypes.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyChannelAccessPoints(List<CreateChannelAccessPointArg> args)
    {
        var activeEntities = _channelAccessPoints.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.IpAddressFrom == x.IpAddressFrom && x.PortFrom == c.PortFrom));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.IpAddressFrom == x.IpAddressFrom && x.PortFrom == c.PortFrom));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _channelAccessPoints.FirstOrDefault(x => x.IpAddressFrom == arg.IpAddressFrom && x.PortFrom == arg.PortFrom && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ChannelAccessPoint.Create(arg);
                _channelAccessPoints.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceChannels(List<CreateServiceChannelArg> args)
    {
        var activeEntities = _serviceChannels.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceId == x.ServiceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceId.Value == x.ServiceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceChannels.FirstOrDefault(x => x.ServiceId.Value == arg.ServiceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceChannel.Create(arg);
                _serviceChannels.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion

    public ChannelId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? Scope { get; private set; }
    public string? Description { get; private set; }
    public ServiceStatusId? ServiceStatusId { get; private set; }
    public virtual ServiceStatus? ServiceStatus { get; private set; }
    public DateOnly? InServiceDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<ChannelResponsible> _channelResponsibles = new();
    public ICollection<ChannelResponsible> ChannelResponsibles => _channelResponsibles;
    private List<ChannelUserType> _channelUserTypes = new();
    public ICollection<ChannelUserType> ChannelUserTypes => _channelUserTypes;
    private List<ChannelAccessPoint> _channelAccessPoints = new();
    public ICollection<ChannelAccessPoint> ChannelAccessPoints => _channelAccessPoints;
    private List<ProductChannel> _productChannels = new();
    public ICollection<ProductChannel> ProductChannels => _productChannels;
    private List<ServiceChannel> _serviceChannels = new();
    public ICollection<ServiceChannel> ServiceChannels => _serviceChannels;


}
