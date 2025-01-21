using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.Channels;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.Channels;

public class ChannelCommandHandler : ICommandHandler<CreateChannelCommand, Result<long>>, ICommandHandler<ModifyChannelCommand,
    Result<long>>, ICommandHandler<DeleteChannelCommand, Result<long>>
{
    private readonly IChannelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IChannelDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public ChannelCommandHandler(IChannelRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IChannelDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateChannelArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await Channel.Create(arg, _service);
            if (request.ChannelResponsibleList is not null)
            {
                var channelResponsibleArgs = _mapper.Map<List<CreateChannelResponsibleArg>>(request.ChannelResponsibleList);
                foreach (var item in channelResponsibleArgs)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.ChannelId = arg.Id;
                }
                entity.AddChannelResponsibles(channelResponsibleArgs);
            }
            if (request.ChannelAccessPointList is not null)
            {
                var channelAccessPointArgs = _mapper.Map<List<CreateChannelAccessPointArg>>(request.ChannelAccessPointList);
                foreach (var item in channelAccessPointArgs)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.ChannelId = arg.Id;
                }
                entity.AddChannelAccessPoints(channelAccessPointArgs);
            }
            if (request.UserTypes is not null)
            {
                var channelUserTypeArgs = _mapper.Map<List<CreateChannelUserTypeArg>>(request.UserTypes);
                foreach (var item in channelUserTypeArgs)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.ChannelId = arg.Id;
                }
                entity.AddChannelUserTypes(channelUserTypeArgs);
            }
            if (request.Products is not null)
            {
                var channelProductChannelArgs = _mapper.Map<List<CreateProductChannelArg>>(request.Products);
                foreach (var item in channelProductChannelArgs)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.ChannelId = arg.Id;
                }
                entity.AddProductChannels(channelProductChannelArgs);
            }
            if (request.Services is not null)
            {
                var channelProductChannelArgs = _mapper.Map<List<CreateServiceChannelArg>>(request.Services);
                foreach (var item in channelProductChannelArgs)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.ChannelId = arg.Id;
                }
                entity.AddServiceChannels(channelProductChannelArgs);
            }
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<Result<long>> Handle(ModifyChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyChannelArg>(request);
        if (request.ChannelResponsibleList is not null)
        {
            var channelResponsibleArgs = _mapper.Map<List<CreateChannelResponsibleArg>>(request.ChannelResponsibleList);
            foreach (var item in channelResponsibleArgs)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ChannelId = arg.Id;
            }
            entity.ModifyChannelResponsibles(channelResponsibleArgs);
        }
        if (request.ChannelAccessPointList is not null)
        {
            var channelAccessPointArgs = _mapper.Map<List<CreateChannelAccessPointArg>>(request.ChannelAccessPointList);
            foreach (var item in channelAccessPointArgs)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ChannelId = arg.Id;
            }
            entity.ModifyChannelAccessPoints(channelAccessPointArgs);
        }
        if (request.UserTypes is not null)
        {
            var channelUserTypeArgs = _mapper.Map<List<CreateChannelUserTypeArg>>(request.UserTypes);
            foreach (var item in channelUserTypeArgs)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ChannelId = arg.Id;
            }
            entity.ModifyChannelUserTypes(channelUserTypeArgs);
        }
        if (request.Products is not null)
        {
            var channelProductChannelArgs = _mapper.Map<List<CreateProductChannelArg>>(request.Products);
            foreach (var item in channelProductChannelArgs)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ChannelId = arg.Id;
            }
            entity.ModifyProductChannels(channelProductChannelArgs);
        }
        if (request.Services is not null)
        {
            var channelProductChannelArgs = _mapper.Map<List<CreateServiceChannelArg>>(request.Services);
            foreach (var item in channelProductChannelArgs)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ChannelId = arg.Id;
            }
            entity.ModifyServiceChannels(channelProductChannelArgs);
        }
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        entity.Delete(_simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}