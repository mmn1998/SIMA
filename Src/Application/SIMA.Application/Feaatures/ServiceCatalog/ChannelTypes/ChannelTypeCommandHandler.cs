using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ChannelTypes;

public class ChannelTypeCommandHandler : ICommandHandler<CreateChannelTypeCommand, Result<long>>,
    ICommandHandler<ModifyChannelTypeCommand, Result<long>>, ICommandHandler<DeleteChannelTypeCommand, Result<long>>
{
    private readonly IChannelTypeRepository _repository;
    private readonly IChannelTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ChannelTypeCommandHandler(IChannelTypeRepository repository, IChannelTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateChannelTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateChannelTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ChannelType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyChannelTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ChannelTypeId(request.Id));
        var arg = _mapper.Map<ModifyChannelTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteChannelTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ChannelTypeId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}