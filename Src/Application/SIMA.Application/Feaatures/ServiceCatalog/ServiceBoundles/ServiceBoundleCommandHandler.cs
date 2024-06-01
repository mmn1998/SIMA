using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceBoundles;

public class ServiceBoundleCommandHandler : ICommandHandler<CreateServiceBoundleCommand, Result<long>>,
    ICommandHandler<ModifyServiceBoundleCommand, Result<long>>, ICommandHandler<DeleteServiceBoundleCommand, Result<long>>
{
    private readonly IServiceBoundleRepository _repository;
    private readonly IServiceBoundleDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ServiceBoundleCommandHandler(IServiceBoundleRepository repository, IServiceBoundleDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateServiceBoundleCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServiceBoundleArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ServiceBoundle.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyServiceBoundleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceBoundleId(request.Id));
        var arg = _mapper.Map<ModifyServiceBoundleArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteServiceBoundleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceBoundleId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
