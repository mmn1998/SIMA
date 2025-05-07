using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.BrokerTypes;

public class BrokerTypeCommandHandler : ICommandHandler<CreateBrokerTypeCommand, Result<long>>, ICommandHandler<ModifyBrokerTypeCommand, Result<long>>, ICommandHandler<DeleteBrokerTypeCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBrokerTypeRepository _repository;
    private readonly IBrokerTypeDomainService _domainService;
    private readonly ISimaIdentity _simaIdentity;

    public BrokerTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IBrokerTypeRepository repository, IBrokerTypeDomainService domainService,
        ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _domainService = domainService;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateBrokerTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBrokerTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await BrokerType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyBrokerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyBrokerTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteBrokerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
